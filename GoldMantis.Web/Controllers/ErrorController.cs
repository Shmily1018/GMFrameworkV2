using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldMantis.Web.Core;

namespace GoldMantis.Web.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Index()
        {
            return (ActionResult)this.View("Index");
        }

        public ActionResult Code401()
        {
            return (ActionResult)this.View("Code401");
        }

        /// <summary>
        /// 404错误处理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Code404()
        {
            //记录日志
            return (ActionResult)this.View("Code404");
        }

    }
}