using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.Common.CustomEnum;
using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.User;
using GoldMantis.WorkFlow.WCFClient;

namespace GoldMantis.Web.Core
{
    public class WFController : BaseController
    {
        private TokenServiceClient wf = new TokenServiceClient();

        //doCommand方法返回值是否为脚本
        private bool doCommandTypeResultIsScript = false;
        private static WFModel model;
        protected IWorkFlowService _WorkFlowService;
        protected ISCFlowMapingService _SCFlowMapingBLL;
        protected IPermissionService _PermissionService;


        public WFController()
        {

        }

        /// <summary>
        /// 获取页面URL
        /// </summary>
        /// <returns></returns>
        public string GetPageUrl()
        {
            string menuUrl = string.Empty;
            if (Request.ApplicationPath.Length > 1)
            {
                menuUrl = Request.Url.AbsolutePath.Substring(Request.ApplicationPath.Length + 1);
            }
            else
            {
                menuUrl = Request.Url.AbsolutePath.Substring(1) + (Regex.IsMatch(Request.Url.PathAndQuery, @"(?<=\W)MenuType=\d+") ? ("?" + Regex.Match(Request.Url.PathAndQuery, @"(?<=\W)MenuType=\d+").Value) : "");
            }
            return menuUrl.ToUpper();
        }

        public SAMenu GetMenu(string menuUrl)
        {

            if (string.IsNullOrEmpty(menuUrl))
            {
                return null;
            }
            var dicUrls = _PermissionService.GetSAMenuList();



            if (dicUrls.Count(x=>x.MenuURL.ToUpper().Contains(menuUrl))>0)
            {
                return dicUrls.FirstOrDefault(x => x.MenuURL.ToUpper().Contains(menuUrl));
            }
            return null;
        }

        public void InitWFController(WFModel modelTrans)
        {

            model = modelTrans;

            //获取提交地址的前缀（当前Controller）
            model.SubmitPrefix = String.Format("{0}/{1}", RouteData.DataTokens["area"].ToString(), RouteData.Values["controller"].ToString());

            //wfc 允许自定义流程（现取流程表单实例中的自定义流程）
            model.AllowCustomFlow = true;

            //add by chenjd 
            //获取当前页面的菜单信息。
            //var menu= GetMenu(GetPageUrl());
            //model.MenuID = menu.ID.ToString();
            //model.MenuURL = menu.MenuURL;


           
            #region OnInit

            //默认获取workID
            if (!String.IsNullOrEmpty(model.MenuID))
            {
                //查询流程时候启用
                List<SCFlowMaping> lst =
                    _SCFlowMapingBLL.GetMenuWFFromCache(Int32.Parse(model.MenuID), model.DeptID,model.KeyID,0);

                model.EnableWorkFlow = lst != null && lst.Count > 0;

                if (lst != null && lst.Count > 0)
                {
                    foreach (SCFlowMaping mapEntity in lst)
                    {
                        model.AllowCustomFlow = model.AllowCustomFlow || mapEntity.AllowCustomFlow;
                        if (model.AllowCustomFlow)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    model.AllowCustomFlow = false;
                }
            }

            #endregion

            #region 菜单标题

            //SAMenuEntity menuEntiy = (this.Page as LoginBasePage).CurrentPageMenu;
            //if (menuEntiy != null)
            //{
            //    lblMain.Text = menuEntiy.MenuName;
            //    lblCode.Text = menuEntiy.ID.ToString().PadLeft(6, '0');
            //    if (!string.IsNullOrEmpty(menuEntiy.MenuImage.Trim()))
            //        imgMain.ImageUrl = menuEntiy.MenuImage;
            //}

            #endregion

            FlowCheck();

            ButtonControl(model.EnableWorkFlow, model.FormCheckStatus);

        }

        /// <summary>
        /// 流程检查
        /// </summary>
        public void FlowCheck()
        {
            #region 测试数据

            int businessID = model.KeyID;
            int deptID = 0;
            EnumPageMode authPageMode = model.EnumPageMode;

            #endregion

            if (model.ActorTokenId.Equals(Guid.Empty))
            {
                //查找默认的WFPublishID
                List<SCFlowMaping> lst = _WorkFlowService.GetSCFlowMapingByMenuIDAndDeptID(int.Parse(model.MenuID), model.DeptID);

                
                if ((authPageMode == EnumPageMode.View|| authPageMode == EnumPageMode.Check)&& businessID > 0)
                {

                    #region 

                    if (deptID > 0)
                    {
                        _WorkFlowService.GetSCFlowMapingByMenuIDAndDeptID(int.Parse(model.MenuID), 0).ForEach(p =>
                        {
                            if (!lst.Contains(p))
                            {
                                lst.Add(p);
                            }
                        });
                    }
                    string wfName = string.Empty;
                    model.WFPublishID = wf.GetPublishIdByList(out wfName,
                        lst == null ? new List<Guid>() : lst.ConvertAll<Guid>(p => { return p.WFPID; })
                        , model.MenuURL
                        , businessID.ToString());
                    model.WFName = wfName;


                    #region 从表单审批的处理

                    if (!model.WFPublishID.Equals(Guid.Empty))
                    {
                        model.ActorTokenId = wf.GetActorTokenId(model.WFPublishID, model.KeyID.ToString(), SessionManager.UserInfo.UserID.ToString());
                        if (!model.ActorTokenId.Equals(Guid.Empty))
                        {
                            if (wf != null)
                                wf.Close();
                            wf = null;

                            if (!string.IsNullOrEmpty(Request.QueryString["isLinkByMaintain"]))
                            {
                                //是否由列表页面链接 by lq
                                //this.Page.Response.Redirect(
                                //    string.Format(
                                //        "{0}?T=Check&ID={1}&OPID={2}&WFPID={3}&WFName={4}&isLinkByMaintain={5}",
                                //        this.Request.Url.AbsolutePath, businessID.ToString(), ActorTokenId,
                                //        WFPublishID, Server.UrlEncode(WFName), Request.QueryString["isLinkByMaintain"]));
                            }
                            else
                            {
                                //this.Page.Response.Redirect(
                                //    string.Format("{0}?T=Check&ID={1}&OPID={2}&WFPID={3}&WFName={4}",
                                //        this.Request.Url.AbsolutePath, businessID.ToString(), ActorTokenId, WFPublishID,
                                //        Server.UrlEncode(WFName)));
                            }
                        }
                    }

                    #endregion

                    #endregion

                }
                else if (lst.Count == 1)
                {
                    model.WFPublishID = lst[0].WFPID;
                    model.WFName = lst[0].WFName;
                }
            }
            else
            {
                //需要校验权限 
                if (!wf.IsOwnerToken(model.ActorTokenId, model.WFPublishID, businessID.ToString(),SessionManager.UserInfo.UserID.ToString()))
                {
                    if (wf != null)
                        wf.Close();
                    wf = null;
                    throw new Exception("没有此任务权限");
                }
            }

        }

        public void ButtonControl(bool enableWorkFlow, EnumCheckStatus checkStatus)
        {
            //保存默认可见
            model.ButtonSaveVisible = true;

            #region 测试数据

            enableWorkFlow = true;

            #endregion

            if (enableWorkFlow)
            {
                IList<CommandModel> cmds = new List<CommandModel>();

                if (model.ActorTokenId.Equals(Guid.Empty)) //启动
                {
                    model.ButtonSubmitVisible = checkStatus == EnumCheckStatus.Init;
                    model.ButtonSaveVisible = checkStatus == EnumCheckStatus.Init;

                    model.ButtonReSubmitVisible = false;
                    model.ButtonBackVisible = false;
                    model.ButtonThroughVisible = false;
                    model.ButtonAgreeVisible = false;
                    model.ButtonDeprecateVisible = false;
                    model.ButtonCCVisible = false;
                    model.ButtonCancelVisible = false;
                    model.ButtonStopVisible = false;

                }
                else
                {
                    #region 

                    string nodeName = string.Empty;

                    wf = new TokenServiceClient();
                    cmds = wf.GetNodeCommands(model.ActorTokenId, out nodeName);
                    model.NodeName = nodeName;
                    model.IsEmergency = wf.IsEmergency(model.ActorTokenId);

                    //退回
                    model.ButtonBackVisible = IsContains(cmds, CommandCatalog.Refuse.GetHashCode().ToString());

                    //已阅
                    model.ButtonViewVisible = IsContains(cmds, CommandCatalog.View.GetHashCode().ToString());

                    //直送
                    model.ButtonThroughVisible = IsContains(cmds, CommandCatalog.Through.GetHashCode().ToString());

                    //同意
                    model.ButtonAgreeVisible = IsContains(cmds, CommandCatalog.Agree.GetHashCode().ToString());

                    //不同意
                    model.ButtonDeprecateVisible = IsContains(cmds, CommandCatalog.Disagree.GetHashCode().ToString());

                    //委托
                    model.ButtonAssignVisible = IsContains(cmds, CommandCatalog.Assign.GetHashCode().ToString());

                    model.ButtonSaveVisible = false;
                    model.ButtonSubmitVisible = false;
                    //提交
                    model.ButtonReSubmitVisible = IsContains(cmds, CommandCatalog.Commit.GetHashCode().ToString());

                    //弃权
                    model.ButtonCancelVisible = IsContains(cmds, CommandCatalog.Cancel.GetHashCode().ToString());

                    //抄送
                    model.ButtonCCVisible = IsContains(cmds, CommandCatalog.Cancel.GetHashCode().ToString());
                    //btnCC.Visible = false;

                    //挂起
                    model.ButtonSuspendVisible = IsContains(cmds, CommandCatalog.Suspend.GetHashCode().ToString());

                    //恢复
                    model.ButtonResumeVisible = IsContains(cmds, CommandCatalog.Resume.GetHashCode().ToString());

                    //终止
                    model.ButtonStopVisible = IsContains(cmds, CommandCatalog.Stop.GetHashCode().ToString());

                    //查看意见
                    model.ButtonViewOpinionVisible = IsContains(cmds, CommandCatalog.Track.GetHashCode().ToString());

                    #endregion
                }

                model.ButtonViewOpinionVisible = !model.ButtonSubmitVisible;
                model.ButtonViewDiagramVisible = true;
                model.ButtonCustomFlowVisible = model.ButtonSubmitVisible && model.AllowCustomFlow;

            }
            else
            {
                model.ButtonViewOpinionVisible = false;
                model.ButtonViewDiagramVisible = false;
                model.ButtonSubmitVisible = false;
                model.ButtonBackVisible = false;
                model.ButtonThroughVisible = false;
                model.ButtonAgreeVisible = false;
                model.ButtonDeprecateVisible = false;
                model.ButtonCCVisible = false;
                model.ButtonCancelVisible = false;
                model.ButtonStopVisible = false;
            }
            //有已阅时，保存按钮不显示
            if (model.ButtonViewVisible)
            {
                model.ButtonSaveVisible = false;
            }
            //else
            //{
            //    //一般情况下，保存按钮都会显示
            //    model.ButtonSaveVisible = true;
            //}
        }

        private string DoCommand(string command, List<ActorModel> processUsers)
        {
            //从缓存中取得数据
            if (model.WFPublishID != Guid.Empty)
            {
                #region 获取最短输入意见（优先级靠后）
                //int ideaLenth = SCWFApproveConfigBLL.GetApproveConfigFromCache(model.WFPublishID + "-" + command);
                int ideaLenth = 0;
                #endregion

                if (!string.IsNullOrEmpty(model.Idea))
                {
                    //如果有配置的话
                    if (ideaLenth > 0 && ideaLenth > model.Idea.Trim().Length)
                    {
                        return "审批意见不应少于" + ideaLenth + "个字！";
                    }
                }

            }

            if (command == CommandCatalog.Stop.GetHashCode().ToString() ||
                command == CommandCatalog.Refuse.GetHashCode().ToString())
            {
                if (model.Idea.Trim() == "退回" || model.Idea.Trim() == "终止" || string.IsNullOrEmpty(model.Idea))
                {
                    return  "(退回/终止)审批时请输入理由！";
                }
            }

            //进行流程之前判断是否需要保存
            if (model.IsDirty &&
                command != CommandCatalog.Stop.GetHashCode().ToString() &&
                command != CommandCatalog.Refuse.GetHashCode().ToString() &&
                command != CommandCatalog.Suspend.GetHashCode().ToString() &&
                command != CommandCatalog.Resume.GetHashCode().ToString() &&
                command != CommandCatalog.Assign.GetHashCode().ToString() &&
                command != CommandCatalog.View.GetHashCode().ToString())
            {
                string msg;
                model.CurrentCommand = command;
                if (!Save(out msg))
                {
                    if (!model.IsExecuteJSAfterFailedSubmit)
                    {
                        return msg;
                    }
                }
                model.IsDirty = false;
            }
            else
            {
                if (model.IsDirty && model.IsSave)
                {
                    string msg;
                    if (!Save(out msg))
                    {
                        return  msg;
                    }
                    model.IsDirty = false;
                }
            }

            #region 提交（准备工作）

            if (command == CommandCatalog.Commit.GetHashCode().ToString())
            {
                List<SCFlowMaping> lst = _WorkFlowService.GetSCFlowMapingByMenuIDAndDeptID(int.Parse(model.MenuID),
                    model.DeptID);


                if (!model.IsCustomFlow && lst.Count > 0)
                {
                    if (lst.Count == 1)
                    {
                        model.WFPublishID = lst[0].WFPID;
                    }
                    else
                    {
                        #region 
                        //选择流程类别 deptID 很重要
                        doCommandTypeResultIsScript = true;
                        return string.Format("Edit.WFMutualSelectFlow('{0}','{1}');", lst[0].MenuID, model.DeptID);
                        #endregion
                    }
                }
            }

            TransitionCallbackHandler wfCall = new TransitionCallbackHandler();
            wfCall.Started += delegate(object sender, WorkflowEventArgs e)
            {
                string msg1 = string.Empty;
                if (!OnWorkflowCompleted(out msg1, e, model.KeyID))
                {
                }
            };
            wfCall.Completed += delegate(object sender, WorkflowEventArgs e)
            {
                string msg1 = string.Empty;
                if (!OnWorkflowCompleted(out msg1, e, model.KeyID))
                {
                }
            };

            wfCall.Viewed += delegate(object sender, WorkflowEventArgs e)
            {
                string msg1 = string.Empty;
                if (!OnWorkflowViewCompleted(out msg1, e, model.KeyID))
                {
                }
            };


            #endregion

            wfCall.onFilterUsers = new TransitionCallbackHandler.OnFilterUsers(FilterUsers);

            MessageModel message = null;
            using (TransitionServiceClient wf = new TransitionServiceClient(new InstanceContext(wfCall)))
            {
                if (command == CommandCatalog.Commit.GetHashCode().ToString())
                {
                    StartModel start = new StartModel
                    {
                        BusinessId = model.KeyID.ToString(),
                        WFPublishId = model.WFPublishID,
                        UserId = model.CurrentUser.ToString(),
                        Priority = TokenPriority.Normal,
                        ActorTokenId = Guid.Empty,
                        SubmitDesc = model.Idea,
                        ExtendInfo = model.TokenExtendInfo,
                        WFTokenName = model.WFTokenName,
                        MenuId = model.MenuID,
                        SourceApp = "1",
                        IsEmergency = model.IsEmergency
                    };

                    if (processUsers.Count > 0 || !string.IsNullOrEmpty(model.SelectNextID))
                    {
                        message = wf.StartByInputValue(processUsers, model.SelectNextID.Trim(), start);
                    }
                    else
                    {
                        message = wf.Start(start);
                    }
                }
                else
                {
                    TokenModel token = new TokenModel
                    {
                        ActorTokenId = model.ActorTokenId,
                        BusinessId = model.KeyID.ToString(),
                        DoMemo = model.Idea,
                        DoResult = command,
                        DoUserId = model.CurrentUser.ToString(),
                        ExtendInfo = model.TokenExtendInfo
                    };

                    if (command == CommandCatalog.Agree.GetHashCode().ToString())
                        token.IsEmergency = model.IsEmergency;

                    if (processUsers.Count > 0 ||!string.IsNullOrEmpty(model.SelectNextID) ||!string.IsNullOrEmpty(model.SelectActivityID.Trim()))
                    {
                        message = wf.ExecutionByInputValue(processUsers, model.SelectNextID.Trim(),
                            model.SelectActivityID.Trim(), token);
                    }
                    else
                    {
                        message = wf.Execution(token);
                    }
                }
            }



            if (message.Actors != null)
            {
                //todo 和用户交互(选择处理者)
                StringBuilder sbUsers = new StringBuilder();
                List<string> strOtherRoles = null;
                //当取的审批人是角色并且只有一个角色时,则添加人员动态中的对应角色的审批人
                if (message.Actors.Count == 1 && message.Actors[0].ActorType == ActorType.Role)
                {
                    string roleName = message.Actors[0].ActorName;
                    int currentProjectID = model.CurrentProjectID; //当前项目ID
                    strOtherRoles = GetUserIDsFromPersonDynamic(currentProjectID, roleName);
                }

                sbUsers.Append("[");
                string mutualActors = string.Empty, userModel = string.Empty;
                foreach (ActorModel user in message.Actors)
                {
                    if (user.ActorType != ActorType.User)
                    {
                        userModel = @"{""ActorType"":" + user.ActorType.GetHashCode().ToString() +
                                    @",""ActorName"":""" + user.ActorName +
                                    @""",""SelectType"":" + user.SelectType.GetHashCode().ToString();

                        List<string> selectUsers = user.ActorList;
                        //附加人员动态中相关审批人
                        if (strOtherRoles != null)
                        {
                            selectUsers.AddRange(strOtherRoles);
                        }
                        if (selectUsers != null)
                        {
                            userModel += @",""UserIds"":""";
                            foreach (string key in selectUsers)
                            {
                                userModel += key.ToString() + ",";
                            }

                        }

                        userModel = userModel.TrimEnd(',');
                        userModel += @""",""IsMutal"":" + (user.IsMutal ? "1" : "0");
                        userModel += @",""IsAssign"":" + (user.IsAssign ? "1" : "0");
                        userModel += @"},";
                        sbUsers.Append(userModel);
                    }
                    else
                    {
                        foreach (string a in user.ActorList)
                            mutualActors = a + ",";
                    }
                }

                if (!string.IsNullOrEmpty(mutualActors))
                    mutualActors = mutualActors.TrimEnd(',');

                model.InputKey = mutualActors;
                model.SelectUserID = string.Empty;


                doCommandTypeResultIsScript = true;
                return string.Format("Edit.MutualPage('指定人','WorkFlow/WorkFlow/MutualSelectActor','{0}')",
                        sbUsers.Remove(sbUsers.ToString().Length - 1, 1) + "]");


            }

            else if (message.RecallList != null)
            {
                //todo 和用户交互（选择退回环节）
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                string nodeModel = "";
                foreach (DoneNodes node in message.RecallList)
                {
                    nodeModel = @"{""ActorTokenId"":""" + node.ActorTokenId.ToString() +
                                @""",""NodeName"":""" + node.NodeName +
                                @""",""DoTime"":""" + node.DoTime.ToString() +
                                @""",""DoUserName"":""" + node.DoUserName;

                    nodeModel += @"""},";
                    sb.Append(nodeModel);
                }

                doCommandTypeResultIsScript = true;
                return string.Format("Edit.MutualPage('指定环节','WorkFlow/WorkFlow/MutualSelectNode','{0}')",
                        sb.Remove(sb.ToString().Length - 1, 1) + "]");
            }
            else if (message.NodeList != null)
            {
                //todo 和用户交互（选择下一环节）
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                string nodeModel = "";
                foreach (MualNodes node in message.NodeList)
                {
                    nodeModel = @"{""NodeId"":""" + node.NodeId.ToString() +
                                @""",""NodeName"":""" + node.NodeName;
                    nodeModel += @"""},";
                    sb.Append(nodeModel);
                }


                return string.Format("Edit.MutualPage('指定环节','WorkFlow/WorkFlow/MutualSelectNext','{0}')",
                            sb.Remove(sb.ToString().Length - 1, 1) + "]");

            }
            else
            {
                if (!string.IsNullOrEmpty(model.ExecuteJS))
                {
                    return message.Message;
                }
                else
                {
                    if (string.IsNullOrEmpty(model.IsDialog))
                    {
                        if (!model.IsShowConfirm)
                        {
                            return message.Message;
                        }

                    }
                    else
                    {
                        return message.Message;
                    }
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 客户端按钮统一提交的Action
        /// </summary>
        /// <param name="commandType">命令类别</param>
        /// <returns></returns>
        public ActionResult Confirm(string commandType, string idea, EnumPageState? pageState, int keyID, string wfName, Guid? wfPublishID, bool isCustomFlow, string selectUserID, bool IsEmergency)
        {
            model.KeyID = keyID;
            model.WFName = wfName;
            model.WFPublishID = wfPublishID.HasValue ? wfPublishID.Value : Guid.Empty;
            model.IsCustomFlow = isCustomFlow;
            model.SelectUserID = selectUserID;
            model.Idea = idea;
            model.CurrentUser = SessionManager.UserInfo.UserID;
            model.EnableWorkFlow = true;
            model.IsEmergency = IsEmergency;

            if (model.EnableWorkFlow)
            {
                var extend = new WorkFlowExtend
                {
                    DeptId = model.DeptID,
                    DeptName =SessionManager.UserInfo.DisplayDepartMent,
                    MenuId = int.Parse(model.MenuID),
                    PageClass = model.PageClass,
                    WFType = EnumERPSystem.OA.GetHashCode(),
                    ID = model.KeyID.ToString()
                };
                extend.AddFormInfo("moduletype", "174", true);//用于android APP 模块对接
                model.TokenExtendInfo = extend.ToString();
            }

            if (commandType == CommandCatalog.Commit.GetHashCode().ToString())
            {
                //设置IsDirty
                model.IsDirty = pageState == EnumPageState.Add || pageState == EnumPageState.Edit;
                model.FormCheckStatus = EnumCheckStatus.Init;
            }
            else
            {
                model.FormCheckStatus = EnumCheckStatus.Doing;
                model.SelectActivityID = string.Empty;
            }


            model.ReStartCommand = commandType;
            string resMsg = DoCommand(model.ReStartCommand, new List<ActorModel>());
            model.SelectNextID = string.Empty;
            if (doCommandTypeResultIsScript)
            {
                return Content(resMsg, "application/x-javascript");
            }
            else
            {
                return Content("window.top.bootstrapGM.alert({width: 450,title: '系统提示',msg: '" + resMsg + "'});window.top.menu.closeTabCallBack();",
                    "application/x-javascript");
            }
        }

        /// <summary>
        /// 从人员动态中获取与角色名对应的审批人的UserID
        /// </summary>
        /// <param name="currentProjectID">当前项目ID</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        private List<string> GetUserIDsFromPersonDynamic(int currentProjectID, string roleName)
        {
            int roleID = 0;
            List<string> strOtherRoles = null;
            //根据角色从PMProjectPostion取角色ID,来自CommonEnum中的PMProjectPostion枚举
            if (roleName == "分公司项目经理")
            {
                roleID = 1;//项目经理
            }
            else if (roleName == "项目部员工")
            {
                roleID = 8;//质量员
            }
            else if (roleName == "分公司预算员")
            {
                roleID = 5;//预算员
            }
            else if (roleName == "安全员")
            {
                roleID = 9;//安全员
            }
            else if (roleName == "分公司施工员")
            {
                roleID = 3;//施工员
            }
            else if (roleName == "分公司小区经理")
            {
                roleID = 13;//小区经理
            }
            else if (roleName == "分公司总经理")
            {
                roleID = 11;//分公司总经理
            }
            else if (roleName == "资料员")
            {
                roleID = 6;//资料员
            }
            else if (roleName == "分公司大区经理")
            {
                roleID = 12;//大区经理
            }
            else if (roleName == "现场设计")
            {
                roleID = 4;//现场设计
            }
            //从PMManager取ManagerID该角色人员列表
            if (roleID != 0)
            {
                #region need rewrite

                if (currentProjectID == 0)
                {
                    throw new Exception("无项目ID,请更换流程。");
                }
                //strOtherRoles = PMManagerBLL.GetManagerIDsByProjectIDAndRoleID(currentProjectID, roleID);

                #endregion

            }

            return strOtherRoles;
        }

        public virtual bool Save(out string message)
        {
            message = "保存成功";
            return true;
        }

        public virtual bool OnWorkflowCompleted(out string message, WorkflowEventArgs e,int keyID)
        {
            message = string.Empty;

            return true;
        }

        public virtual bool OnWorkflowViewCompleted(out string message,WorkflowEventArgs e, int keyID)
        {
            message = string.Empty;

            return true;
        }

        public virtual List<string> FilterUsers(List<string> sourceUser)
        {
            //功能权限过滤 
            //return _SAUserServiceService.FilterUserByFunction(sourceUser, model.DeptID);
            return sourceUser;

        }

        private bool IsContains(IList<CommandModel> cmds, string commandId)
        {
            CommandModel cmd = cmds.SingleOrDefault(p => p.CommandId.Equals(commandId));
            if (cmd != null)
                return true;
            return false;
        }

        public ActionResult SelectUserConfrim(string reStartCommand, string selectUsers, string selectActivityID, string inputKey)
        {
            model.SelectActivityID = selectActivityID;
            if (string.IsNullOrEmpty(model.ReStartCommand))
            {
                return Content("系统异常请和管理员联系！", "application/x-javascript");
            }
            else
            {
                List<ActorModel> userList = new List<ActorModel>();
                if (!string.IsNullOrEmpty(inputKey))
                {
                    ActorModel mutualActor = new ActorModel();
                    string[] actorArray = inputKey.Split(',');
                    mutualActor.ActorList = actorArray.ToList<string>();
                    userList.Add(mutualActor);
                }

                if (!string.IsNullOrEmpty(selectUsers))
                {
                    string[] users = selectUsers.Split('$');
                    foreach (string str in FilterActorUser(userList, users[0].Split(',')))
                    {
                        ActorModel actor = new ActorModel();
                        actor.ActorList = new List<string> { str };
                        if (users.Length > 1)
                        {
                            actor.AssignRecall = Convert.ToBoolean(users[1]);
                        }
                        userList.Add(actor);
                    }
                }
                string resMsg = DoCommand(reStartCommand, userList);
                model.InputKey = String.Empty;
                model.SelectUserID = string.Empty;
                if (doCommandTypeResultIsScript)
                {
                    return Content(resMsg, "application/x-javascript");
                }
                else
                {
                    return Content("window.top.bootstrapGM.alert({width: 450,title: '系统提示',msg: '" + resMsg + "'});window.top.menu.closeTabCallBack();",
                        "application/x-javascript");
                }
            }
        }

        private List<string> FilterActorUser(List<ActorModel> userList, string[] filted)
        {
            List<string> result = new List<string>();

            foreach (string user in filted)
            {
                if (userList.Any(model => model.ActorList.Contains(user)) || result.Contains(user))
                    continue;

                result.Add(user);
            }
            return result;
        }

    }

    public class WorkFlowExtend
    {
        public WorkFlowExtend()
        {
            ProjectName = string.Empty;
            DeptName = string.Empty;
            ContactUnitName = string.Empty;
            WorkGroupName = string.Empty;
            MaterialTypeName = string.Empty;
            PageClass = string.Empty;
            PersonName = string.Empty;
            ID = string.Empty;
            Amount = 0;
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId
        {
            get;
            set;
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get;
            set;
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName
        {
            get;
            set;
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int DeptId
        {
            get;
            set;
        }

        /// <summary>
        /// 往来单位
        /// </summary>
        public string ContactUnitName
        {
            get;
            set;
        }

        /// <summary>
        /// 班组名称
        /// </summary>
        public string WorkGroupName
        {
            get;
            set;
        }

        public string MaterialTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 人名
        /// </summary>
        public string PersonName
        {
            get;
            set;
        }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount
        {
            get;
            set;
        }

        /// <summary>
        /// 页面MenuId
        /// </summary>
        public int MenuId
        {
            get;
            set;
        }

        /// <summary>
        /// 模块
        /// </summary>
        public int WFType
        {
            get;
            set;
        }

        /// <summary>
        ///表单流水号
        /// </summary>
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// 页面对应的数据库表名
        /// </summary>
        public string PageClass
        {
            get;
            set;
        }

        protected NameValueCollection FormParamList
        {
            get;
            set;
        }

        public override string ToString()
        {
            string extendInfo = string.Empty;
            extendInfo = "ProjectName:" + ProjectName + ";"
                + "DeptName:" + DeptName + ";"
                + "ContactUnitName:" + ContactUnitName.ToString() + ";"
                + "Amount:" + (Amount == 0 ? "0" : Amount.ToString()) + ";"
                + "MenuId:" + (MenuId == 0 ? "" : MenuId.ToString()) + ";"
                + "WFType:" + WFType.ToString() + ";"
                + "ProjectId:" + (ProjectId == 0 ? "" : ProjectId.ToString()) + ";"
                + "DeptId:" + (DeptId == 0 ? "0" : DeptId.ToString()) + ";"
                + "PersonName:" + PersonName.ToString() + ";"
                + "ID:" + ID;

            if (FormParamList != null)
            {
                foreach (string key in FormParamList.Keys)
                {
                    if (extendInfo.IndexOf(key) == -1)
                        extendInfo += ";" + key + ":" + FormParamList[key];
                }
            }

            return extendInfo;
        }


        //public static string BuildMemo(string extendInfos)
        //{
        //    string extendInfo = string.Empty;
        //    string[] infos = extendInfos.Split(';');
        //    if (infos.Length > 1)
        //    {
        //        string[] namevalue = null;
        //        if (infos.Length > 0 && !string.IsNullOrEmpty(infos[0]))
        //        {
        //            namevalue = infos[0].Split(':');
        //            if (!string.IsNullOrEmpty(namevalue[1]))
        //                extendInfo = "项目:" + namevalue[1];
        //        }

        //        if (infos.Length > 1 && !string.IsNullOrEmpty(infos[1]))
        //        {
        //            namevalue = infos[1].Split(':');
        //            if (!string.IsNullOrEmpty(namevalue[1]))
        //            {
        //                if (!string.IsNullOrEmpty(extendInfo))
        //                    extendInfo += "；部门:" + namevalue[1];
        //                else
        //                    extendInfo += "部门:" + namevalue[1];
        //            }
        //        }

        //        if (infos.Length > 2 && !string.IsNullOrEmpty(infos[2]))
        //        {
        //            namevalue = infos[2].Split(':');
        //            if (!string.IsNullOrEmpty(namevalue[1]))
        //            {
        //                if (!string.IsNullOrEmpty(extendInfo))
        //                    extendInfo += "；往来单位:" + namevalue[1];
        //                else
        //                    extendInfo += "往来单位:" + namevalue[1];
        //            }
        //        }

        //        if (infos.Length > 3 && !string.IsNullOrEmpty(infos[3]))
        //        {
        //            namevalue = infos[3].Split(':');
        //            if (!string.IsNullOrEmpty(namevalue[1]))
        //            {
        //                if (!string.IsNullOrEmpty(extendInfo) && namevalue[0].Trim() == "Amount")
        //                    extendInfo += "；金额:" + Convert.ToDecimal(namevalue[1]).ToString();
        //                else if (namevalue[0].Trim() == "Amount")
        //                    extendInfo += "金额:" + Convert.ToDecimal(namevalue[1]).ToString();
        //            }
        //        }

        //        if (infos.Length > 9 && !string.IsNullOrEmpty(infos[8]))
        //        {
        //            namevalue = infos[8].Split(':');
        //            if (!string.IsNullOrEmpty(namevalue[1]) && namevalue[0].Trim() == "PersonName")
        //            {
        //                if (!string.IsNullOrEmpty(extendInfo))
        //                    extendInfo += "；姓名:" + namevalue[1];
        //                else
        //                    extendInfo += "姓名:" + namevalue[1];
        //            }
        //        }
        //    }

        //    return extendInfo;
        //}

        /// <summary>
        /// 获取扩展字段
        /// </summary>
        /// <param name="extendInfos"></param>
        /// <param name="extendInfoName">字段名称不区分大小写</param>
        /// <returns></returns>
        public static string GetExtendInfo(string extendInfos, string extendInfoName)
        {
            string result = string.Empty;
            string[] infos = extendInfos.Split(';');
            foreach (string row in infos)
            {
                string[] namevalue = null;
                namevalue = row.Split(':');
                if (namevalue.Length > 1 && namevalue[0].ToLower() == extendInfoName)
                {
                    result = namevalue[1];
                }
            }
            return result;
        }

        /// <summary>
        /// 添加页面采集信息
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="isPersisent">是否持久化</param>
        public WorkFlowExtend AddFormInfo(string fieldName, string fieldValue, bool isPersisent)
        {
            if (FormParamList == null)
            {
                FormParamList = new NameValueCollection();
            }

            if (FormParamList.GetValues(fieldName) == null)
            {
                FormParamList.Add(fieldName, fieldValue);
            }

            return this;
        }

        /// <summary>
        /// 移除页面采集信息
        /// </summary>
        /// <param name="fieldName"></param>
        public void RemoveFormInfo(string fieldName)
        {
            if (FormParamList != null)
            {
                FormParamList.Remove(fieldName);
            }

        }

    }
}