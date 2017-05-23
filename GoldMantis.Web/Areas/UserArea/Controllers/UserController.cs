using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GoldMantis.Common;
using GoldMantis.Common.CustomEnum;
using GoldMantis.Entity;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Web.Core;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.User;
using GoldMantis.WorkFlow.WCFClient;
using Newtonsoft.Json;
using UploadfileConfiguration = GoldMantis.Web.Areas.UploadFileManage.Controllers.UploadfileConfiguration;

namespace GoldMantis.Web.Areas.UserArea.Controllers
{
    public class UserController : BaseController
    {
        ModelDemoCreate model = new ModelDemoCreate();

        protected IDemoService demoService;
        protected ISCFlowMapingService _SCFlowMapingService;
        protected static IList<SAMenu> menus;
        protected IPermissionService _PermissionService;
        private List<int> array;
        public ActionResult GetTree(int? id)
        {
            ArrayList list=new ArrayList();
            dynamic obj = new ExpandoObject();

            if (menus == null)
            {
                IList<SAMenu> menusDB = _PermissionService.GetSAMenuList();
                menus = menusDB;
                
            }

            

            array = new List<int>();

            
            if (!id.HasValue)
            {
                foreach (var item in menus.Where(x => x.ParentID == 0 && !array.Contains(x.ID)).ToList())
                {
                    obj = new ExpandoObject();
                    obj.icon = "fa fa-folder icon-lg icon-state-warning";
                    obj.id = item.ID.ToString();
                    obj.text = item.MenuName;
                    obj.children = true;
                    obj.type = "root";

                 

                    list.Add(obj);
                }

            }
            else
            {
                if (menus.Where(x => x.ParentID == id).ToList().Count > 0)
                {
                    IList<SAMenu> childItem = menus.Where(x => x.ParentID == id && !array.Contains(x.ID)).ToList();
                    foreach (var item in childItem)
                    {
                        

                        array.Add(item.ID);

                        if (menus.Where(x => x.ParentID == item.ID && !array.Contains(x.ID)).ToList().Count > 0)
                        {
                            obj = new ExpandoObject();
                            obj.id = item.ID;
                            obj.text = item.MenuName;
                            obj.children = true;
                            obj.icon = "fa fa-folder icon-lg icon-state-warning";

                      

                            list.Add(obj);
                        }
                        else
                        {
                            obj = new ExpandoObject();
                            obj.id = item.ID;
                            obj.text = item.MenuName;
                            obj.children = false;
                            obj.icon = "fa fa-file icon-lg icon-state-warning";

                         

                            list.Add(obj);
                        }
                    }


                }


            }

            return Content(JsonConvert.SerializeObject(list), "application/json");

        }

        public UserController()
        {

        }

        public ActionResult Index(ModelDemoIndex model)
        {
            model = model ?? new ModelDemoIndex();
            model = demoService.GetModelDemoIndex(model);

            //int sec1, sec2;
            //System.Diagnostics.Process.Start(@"E:\redis-2.4.5-win32-win64\64bit\redis-server.exe");
            //using (var redisClient = RedisManager.GetClient())
            //{

            //    DateTime d1=DateTime.Now;

            //    IList<SAUser> menusDB = _SAUserService.List();

            //    sec1=(DateTime.Now - d1).Milliseconds;

            //    IRedisTypedClient<SAUser> menu = redisClient.As<SAUser>();
            //    menu.StoreAll(menusDB);


            //}


            //using (var redisClient = RedisManager.GetClient())
            //{
            //    DateTime d1 = DateTime.Now;

            //    IRedisTypedClient<SAUser> menu = redisClient.As<SAUser>();
            //    var s= menu.GetAll();

            //    sec2 = (DateTime.Now - d1).Milliseconds;
            //}


            return View(model);
        }

        public ActionResult Index500(ModelDemoIndex model)
        {
            model = model ?? new ModelDemoIndex();
            int ex = Convert.ToInt32("asdasd");
            return View(model);
        }

        public virtual ActionResult Create(Int32? id, EnumPageMode? pageState)
        {
            ViewBag.PageState = GetPageState(id, pageState);

            try
            {
                if (id != null)
                {
                    //修改或查看用户信息
                    model.Demo = new Demo { ID = id.Value };
                }
                model = demoService.GetModelDemoCreate(model);

                if (pageState == EnumPageMode.View)
                {
                    //将附件设置成“只读”，默认为可编辑
                    model.FileUploadHelperObject.IsReadOnly = true;
                }

                if (model.Demo != null)
                {
                    model.KeyID = model.Demo.ID;
                    model.CheckStatus = model.Demo.CheckStatus;
                    if (model.CheckStatus.HasValue && model.CheckStatus.Value > 0)
                    {
                        model.FormCheckStatus = EnumCheckStatus.Post;
                    }
                }

                IList<SCDepartment> menus = _SCFlowMapingService.GetSCDepartmentList(); //0无任何意义
                string x = TreeSCDepartment.Department(menus);

                //IList<SAMenu> menus = _PermissionService.GetUserMenuInfo(SessionManager.UserInfo.UserID, 0); //0无任何意义
                //string x = TreeSAMenu.Menu(menus);
                ViewBag.Text = x;

                ////图文打印加班登记
                //model.MenuID = "2685";
                //model.MenuURL = "OA/OAOvertimeEdit.aspx";
                //model.WFTokenName = "员工加班";
                //model.DeptID = SessionManager.UserInfo.HrDeptID;
                //model.KeyID = model.Demo != null ? model.Demo.ID : 0;

                //InitWFController(model);

                return View(model);
            }
            catch
            {
                return Content("读取信息失败");
            }
        }

        // POST: UserArea/User/Create
        [HttpPost]
        public ActionResult Create(ModelDemoCreate model)
        {
            try
            {
                #region 附件列表上传操作

                ////获取上传的文件
                IList<UploadedFile> attachments =
                    SessionManager.UploadedFiles.Where(x => x.FileKey == model.FileUploadHelperObject.FileKey).ToList();


                //实例化
                if (model.FileUploadHelperObject.AttachmentList == null)
                {
                    model.FileUploadHelperObject.AttachmentList =new List<Attachment>();
                }

                foreach (var item in attachments)
                {
                    model.FileUploadHelperObject.AttachmentList.Add(item.RemoteFileMessage);
                }

                #endregion

                model = demoService.PostModelDemoCreate(model);
 
                if (model.Comand == "1")
                {
                    model.KeyID = model.Demo.ID;
                    ViewBag.PageState = EnumPageMode.Edit;
                    return View(model);
                }
                return Content("<script>window.top.menu.closeTabCallBack("+model.Demo.ID+");</script>");
            }
            catch
            {
                ModelDemoCreate modelGet = demoService.GetModelDemoCreate(model);
                modelGet.Demo = model.Demo;
                Error = "保存信息失败";
                return View(modelGet);
            }
        }

        // POST: UserArea/User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            JsonResult jsresult = new JsonResult();
            try
            {
                demoService.DeleteByKey(id);
                jsresult.Data = new { result = "操作成功!" };
            }
            catch
            {
                jsresult.Data = new { result = "操作失败!" };
            }
            return jsresult;
        }

        [HttpPost]
        public ActionResult DeleteBatch(int[] ids)
        {
            JsonResult jsresult = new JsonResult();
            try
            {
                demoService.DeleteByKeys(ids);
                jsresult.Data = new { result = "操作成功!" };
            }
            catch
            {
                jsresult.Data = new { result = "操作失败!" };
            }
            return jsresult;
        }

        public ActionResult Choose(ModelDemoIndex model)
        {
            model = model ?? new ModelDemoIndex();
            model = demoService.GetModelDemoIndex(model);
            return View(model);
        }

        public ActionResult ChooseWorkFlow(int MenuID, int DeptID)
        {

            return View();
        }

        public ActionResult ChooseSCFlowMapingIndex(ModelSCFlowMapingIndex model, int menuID, int deptID)
        {
            model = model ?? new ModelSCFlowMapingIndex();
            model.DeptID = deptID;
            model.MenuID = menuID;

           // model = _SCFlowMapingService.GetModelChooseWorkFlowIndex(model);
            return View(model);
        }



        public class UserModel
        {
            public int ActorType
            {
                get;
                set;

            }

            public string ActorName
            {
                get;
                set;

            }

            public int SelectType
            {
                get;
                set;
            }

            public int IsMutal
            {
                get;
                set;
            }

            public int IsAssign
            {
                get;
                set;
            }

            public string UserIds
            {
                get;
                set;
            }
        }

        //public override bool OnWorkflowCompleted(out string message, WorkflowEventArgs e, int keyID)
        //{
        //    int checkstatus = e.CheckStatus.GetHashCode();
        //    message = string.Empty;

        //    bool result = demoService.Checked(keyID, checkstatus);

        //    if (checkstatus ==EnumCheckStatus.Ok.GetHashCode()) //审批通过自动审核
        //    {
        //        result = result && demoService.Post(keyID, out  message);
        //    }
        //    return result;
        //}

        //public override bool Save(out string message)
        //{
        //    return base.Save(out message);
        //}

        public ActionResult GetWaitHandle()
        {
            IList<MyTokensModel> tokens = null;
            using (TokenServiceClient wf = new TokenServiceClient())
            {
                tokens = wf.GetToDoTokens(21, SessionManager.UserInfo.UserID.ToString(), string.Empty, string.Empty,
                    string.Empty, string.Empty, string.Empty);
                IList<SCFlowMaping> maps = _SCFlowMapingService.GetSCFlowMapingList();

                foreach (MyTokensModel token in tokens)
                {
                    if (token.WFPublishId.ToString() == "a6f1c4c6-568f-48cf-940b-ad841fff915d")
                    {
                        if (token.ExtendInfo.IndexOf("Manning:1") > -1)
                            token.WorkflowName = token.WorkflowName + "(编外)";
                    }

                    if (string.IsNullOrEmpty(token.FormUrl))
                    {
                        var item = maps.FirstOrDefault(x => x.WFPID == token.WFPublishId);
                        if (item != null)
                        {
                            token.FormUrl = item.Url;
                        }

                        if (string.IsNullOrEmpty(token.FormUrl)
                            && maps.FirstOrDefault(x => x.WFPID == token.ParentWFPublishId) != null
                            && token.ParentWFPublishId != Guid.Empty)
                        {
                            token.FormUrl = maps.FirstOrDefault(x => x.WFPID == token.ParentWFPublishId).Url;
                        }

                    }
                }
            }
            return View();

        }

    }
}
