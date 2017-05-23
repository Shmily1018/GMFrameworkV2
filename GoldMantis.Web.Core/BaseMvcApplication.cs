/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         BaseMvcApplication.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      自定义HttpApplication
** VersionInfo:
*********************************************************/

using System;
using System.Configuration;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using GoldMantis.Common;
using GoldMantis.DI;
using GoldMantis.Web.Core.Session;

namespace GoldMantis.Web.Core
{
    public class BaseMvcApplication : HttpApplication
    {
        protected virtual void Application_Start()
        {


        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {
        }

        protected virtual void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.Redirect("~/Error/Code404/");
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            //NHibernateHelper.BindSession();
        }

        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {
            //NHibernateHelper.UnBindSession();
        }
    }
}
