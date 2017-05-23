using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.Common.CustomEnum;
using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Web.Areas.UploadFileManage.Controllers;
using GoldMantis.Web.Areas.UserArea.Controllers;
using GoldMantis.Web.Core;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.User;
using GoldMantis.WorkFlow.WCFClient;
using Newtonsoft.Json;
using System.IO;

namespace GoldMantis.Web.Areas.WorkFlow.Controllers
{
    public class WorkFlowController : WFController
    {
        public String ShowDigram(Guid wfpid)
        {
            string imgUrl = string.Empty;
            FlowEntity flowEntity = null;
            using (TokenServiceClient wf = new TokenServiceClient())
            {
                flowEntity = wf.GetFlowByWfId(wfpid);

            }
            if (flowEntity != null)
            {
                if (flowEntity.IsCommon)
                {
                    //重点
                    //自定义工作流只显示文字，图片需要隐藏
                    //ifFlow.InnerHtml = flowEntity.Desc;
                    //plImg.Visible = false;

                    imgUrl = flowEntity.Desc;
                }
                else
                {
                    try
                    {
                        string imgFilePath = string.Format("{0}TEMP\\{1}.png", AppDomain.CurrentDomain.BaseDirectory, wfpid);
                        DateTime modifyDate =System.IO.File.GetLastWriteTime(imgFilePath);
                        imgUrl = string.Format("/Temp/{0}.png", wfpid);
                        using (DiagramServiceClient client = new DiagramServiceClient())
                        {
                            DiagramImage img = client.RetrieveImage(modifyDate, wfpid, string.Empty);
                            if (img != null && img.ImageByte != null)
                            {
                                System.IO.File.WriteAllBytes(imgFilePath, img.ImageByte);
                                System.IO.File.SetLastWriteTime(imgFilePath, img.ModifyDate);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw new Exception(ex.Message);
                    }
                }

            }
            return imgUrl;
        }

        public ActionResult GetAdvice(string menuID, string keyID, Guid workFlowID)
        {
            ModelWorkFlowAdvice model = GetWorkFlowAdvice("", keyID, workFlowID, menuID);
            if (model.DataSource == null)
            {
                model.DataSource = new List<CheckedTokensModel>();
            }
            return View(model);
        }

        public ModelWorkFlowAdvice GetWorkFlowAdvice(string c, string businessID, Guid workFlowID, string menuID)
        {
            ModelWorkFlowAdvice model = new ModelWorkFlowAdvice();

            bool GetByBusiness = c == "1";
            //AB3AC23C-D570-48E4-8E26-B48566FE77C5

            Guid WorkFlowTokenID = new Guid();

            string endTime = "";
            IList<CheckedTokensModel> tokens = null;
            if (!GetByBusiness)
            {
                tokens = GetCheckOpinions(businessID, workFlowID, menuID, out endTime);
                if (WorkFlowTokenID != Guid.Empty)
                {
                    tokens = GetCheckOpinions(WorkFlowTokenID);
                }
            }
            else
            {
                //tokens = Session["CheckTokens"] as IList<CheckedTokensModel>;
            }

            if (tokens != null && tokens.Count > 0)
            {
                model.UserName = tokens[0].CreateUserName;
                model.WorkflowName = tokens[0].WorkflowName;

                model.StartTime = Convert.ToDateTime(tokens[0].StartTime);
                if (tokens[0].EndTime != null)
                {
                    model.EndTime = tokens[0].EndTime.Value;
                }
                model.DataSource = tokens;
            }

            return model;
        }

        /// <summary>
        /// 获取审批意见
        /// </summary>
        /// <param name="businessID"></param>
        /// <param name="workflowId"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static IList<CheckedTokensModel> GetCheckOpinions(string businessID, Guid workflowId, string menuId, out string endTime)
        {
            endTime = "";
            using (TokenServiceClient wfRuntime = new TokenServiceClient())
            {
                IList<CheckedTokensModel> tokens = wfRuntime.GetTokensByBusinessIdAndMenuId(businessID, workflowId, menuId);
                if (tokens.Count > 0)
                    endTime = tokens[0].EndTime.ToString();
                return tokens;
            }
        }

        /// <summary>
        /// 获取审批意见
        /// </summary>
        /// <param name="workflowInstID"></param>
        /// <returns></returns>
        public static IList<CheckedTokensModel> GetCheckOpinions(Guid workflowInstID)
        {
            using (TokenServiceClient wfRuntime = new TokenServiceClient())
            {
                IList<CheckedTokensModel> tokens = wfRuntime.GetCheckTokens(workflowInstID);
                return tokens;
            }
        }

        public ActionResult ChooseSCFlowMapingIndex(ModelSCFlowMapingIndex model, int menuID, int deptID)
        {
            model = model ?? new ModelSCFlowMapingIndex();
            model.DeptID = deptID;
            model.MenuID = menuID;
            model = _WorkFlowService.GetModelChooseWorkFlowIndex(model);
            return View(model);
        }

        public ActionResult MutualSelectActor(ModelSAUserIndex model, String pdata)
        {
            model = model ?? new ModelSAUserIndex();
            model.Pdata = pdata;
            List<UserController.UserModel> ids = JsonConvert.DeserializeObject<List<UserController.UserModel>>(pdata);
            if (ids.Count > 0)
            {
                UserController.UserModel firstObj = ids.FirstOrDefault();
                int[] idsArray = null;
                if (!String.IsNullOrEmpty(firstObj.UserIds))
                {
                    idsArray = Array.ConvertAll(firstObj.UserIds.Split(','), int.Parse);
                }
                model.IDs = idsArray;
                //important 此处需要从权限数据库获取用户信息
                model = _WorkFlowService.GetSAUsersByIDs(model);

            }

            return View(model);
        }

        /// <summary>
        /// 退回选择
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pdata"></param>
        /// <returns></returns>
        public ActionResult MutualSelectNode(ModelNodeIndex model, String pdata)
        {
            model = model ?? new ModelNodeIndex();
            model.Pdata = pdata;
            List<NodeModel> gridDataSources = JsonConvert.DeserializeObject<List<NodeModel>>(pdata);
            model.GridDataSources = gridDataSources;
            model = _WorkFlowService.MutualSelectNodeIndex(model);
            return View(model);
        }

        public ActionResult MutualSelectNextIndex(ModelSelectNextIndex model, String pdata)
        {
            model = model ?? new ModelSelectNextIndex();
            model.Pdata = pdata;
            List<ModelSelectNext> gridDataSources = JsonConvert.DeserializeObject<List<ModelSelectNext>>(pdata);
            model.GridDataSources = gridDataSources;
            model = _WorkFlowService.MutualSelectNextIndex(model);
            return View(model);
        }

        public ActionResult CustomFlow(ModelCustomFlowIndex model, int userID, int menuID, int deptID)
        {
            model = model ?? new ModelCustomFlowIndex() { DeptID = deptID, MenuID = menuID, UserID = userID };
            model = _WorkFlowService.GetModelCustomFlowIndex(model);

            return View(model);
        }


        public virtual ActionResult Create(int UserID,int MenuID, EnumPageState? pageState)
        {
            ViewBag.PageState = pageState;

            var model = new ModelCommonFlowEntityCreate() { UserID = UserID, MenuID = MenuID };
            try
            {
                
                return View(model);
            }
            catch
            {
                return Content("读取信息失败");
            }
        }

        public virtual ActionResult View(Guid? PublishID, int UserID, int MenuID, EnumPageState? pageState)
        {
            ViewBag.PageState = pageState;

            var model = new ModelCommonFlowEntityCreate();
            try
            {
                if (PublishID != null)
                {
                    model.CommonFlowEntity = new CommonFlowEntity();
                    model.PublishID = PublishID.Value;
                    model.MenuID = MenuID;
                    model.UserID = UserID;
                }
                model = _WorkFlowService.GetModelCommonFlowEntityCreate(model);

                return View(model);
            }
            catch
            {
                return Content("读取信息失败");
            }
        }

        [HttpPost]
        public ActionResult Create(ModelCommonFlowEntityCreate model)
        {
            try
            {
                model = _WorkFlowService.PostModelCommonFlowEntityCreate(model);

                return Content("<script>window.top.menu.closeDialogAndRefreshParent();</script>");

            }
            catch
            {
                ModelCommonFlowEntityCreate modelGet = _WorkFlowService.GetModelCommonFlowEntityCreate(model);
                modelGet.CommonFlowEntity = model.CommonFlowEntity;
                Error = "保存信息失败";
                return View(modelGet);
            }
        }
    }
}