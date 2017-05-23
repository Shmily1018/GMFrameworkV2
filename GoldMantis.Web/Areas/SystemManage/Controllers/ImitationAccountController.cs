using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GoldMantis.Common;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Web.Core;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.ViewModel.SystemManage;
using Newtonsoft.Json;

namespace GoldMantis.Web.Areas.SystemManage.Controllers
{
    public class ImitationAccountController : BaseController
    {
        // GET: SystemManage/ImitationAccount

        protected IPermissionService _PermissionService;

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(ModelImitationAccountIndex model)
        {

            string message = String.Empty;

            UserInfo userInfo = _PermissionService.ImitationAccount(model.ImitationAccountUserID, SessionManager.UserInfo.UserID, ref message);

            if (userInfo.LoginResult)
            {
                SessionManager.UserInfo = userInfo;
                FormsAuthentication.SetAuthCookie(SessionManager.UserInfo.UserName, true);


                return Content("<script>window.top.location.href=window.top.location.href</script>");
            }
            else
            {
                Error = message;
                return View();
            }

            return null;
        }
    }
}