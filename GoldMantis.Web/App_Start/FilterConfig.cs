using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GoldMantis.Log;
using GoldMantis.Web.Core.Session;
using Newtonsoft.Json;

namespace GoldMantis.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyExceptionAttribute());

            filters.Add(new AuthenticationAttribute());
        }
    }


    public class MyExceptionAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 搜集500错误，并跳转至错误页。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception ex = filterContext.Exception;
            var request = HttpContext.Current.Request;

            dynamic errorMsg = new ExpandoObject();
            errorMsg.RequestUrl = request.Url;
            errorMsg.ClientIP = request.UserHostAddress == "::1" ? "127.0.0.1" : request.UserHostAddress;
            errorMsg.Message = ex.Message;
            errorMsg.MessageType = "全局捕获";
            errorMsg.Source = ex.Source;
            errorMsg.TargetSite = ex.Source;
            errorMsg.StackTrace = ex.StackTrace;
            errorMsg.InnerException = ex.InnerException == null;
           
            //LogHelper.WriteLog(SessionManager.UserInfo.Account, SessionManager.UserInfo.UserName, String.Empty,
            //    "全局捕获500错误", JsonConvert.SerializeObject(errorMsg));
        }

       
    }

    /// <summary>
    /// 需要认证的Action加上此Attribute
    /// </summary>
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            base.OnActionExecuting(filterContext);
        }
    }
}
