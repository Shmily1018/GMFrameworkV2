using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Xml;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Framework.CryptogramService;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Web.Core;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.Home;
using GoldMantis.Web.ViewModel.User;
using GoldMantis.WorkFlow.WCFClient;
using Newtonsoft.Json;
using SCFlowMaping = GoldMantis.Common.SCFlowMaping;

namespace GoldMantis.Web.Controllers
{
    public class HomeController : BaseController
    {
        protected IPermissionService _PermissionService;
        protected ISCFlowMapingService _SCFlowMapingService;

        public IList<MyTokensModel> GetWaitHandle()
        {
            IList<MyTokensModel> tokens = null;
            using (TokenServiceClient wf = new TokenServiceClient())
            {
                tokens = wf.GetToDoTokens(1000, SessionManager.UserInfo.UserID.ToString(), string.Empty, string.Empty,
                    string.Empty, string.Empty, string.Empty).OrderByDescending(x => x.NodeCreateTime).ToList();
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
            return tokens;
        }

        public ActionResult Index()
        {
            //未登录返回登录页。
            if (SessionManager.UserInfo == null)
                return this.RedirectToAction("LogOn", "Home");

            ////待办事宜
            //ViewBag.Tasks = GetWaitHandle();

            ////菜单
            //IList<SAMenu> menus = _PermissionService.GetUserMenuInfo(SessionManager.UserInfo.UserID,0); //0无任何意义
            //ViewBag.Menus = menus;

            //var quickMenus = menus.Where(x => !String.IsNullOrWhiteSpace(x.MenuURL) && x.IsOn)
            //    .Select(x => new { label = String.Format("{0} {1}", NPinyin.Pinyin.GetInitials(x.MenuName), x.MenuName), 
            //        value = String.Format("menu.addTabs('{0}', '{1}')",x.MenuName,x.MenuURL)  });


            //ViewBag.Text = TreeSAMenu.Menu(menus);

            //ViewBag.QuickMenusJson = JsonConvert.SerializeObject(quickMenus);
            

            return View();
        }

        /// <summary>
        /// 锁屏页开发
        /// </summary>
        /// <returns></returns>
        public ActionResult LockScreen()
        {
            return View();
        }

        public ActionResult LogOn(LogOnModel model)
        {
            model = new LogOnModel();
            return View(model);
        }

        /// <summary>
        /// 本代码纯属测试代码，后期需调整
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            string message = String.Empty;

            try
            {
                if (this.ModelState.IsValid)
                {

                    #region 调用远程接口，验证ERP登录结果

                    UserInfo userInfo = _PermissionService.GetLoginInfo(model.UserName, model.Password, false,
                        ref message);

                    #endregion


                    if (userInfo != null && userInfo.LoginResult)
                    {
                        SessionManager.UserInfo = userInfo;
                        FormsAuthentication.SetAuthCookie(model.UserName, true);

                        //写入登录日志
                        base.WriteLoginLog("登录成功");
                    }
                    else
                    {
                        Error = message;
                        return View(model);
                    }
                }
                else
                {
                    Error = "用户名或密码不能为空";
                }
                return RedirectToAction("Index");



            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
            }

            return View("LogOn", (object)model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            this.Session.Abandon();
            return (ActionResult)this.RedirectToAction("LogOn");
        }
    }
}



