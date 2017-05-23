using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using GoldMantis.Common;
using GoldMantis.DI;
using GoldMantis.Web.Core;
using GoldMantis.Web.Core.Session;

  



namespace GoldMantis.Web 
{
    public class MvcApplication : HttpApplication
    {


        protected void Application_Start()
        {

            foreach (ServerConfig serverConfig in ServicesConfig.GetServicesConfig().ServiceConfigItems)
            {
                if (serverConfig.serverType.ToUpper() == "LOCAL")
                {
                    NHibernateHelper.Configure();
                    break;
                }
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ControllerBuilder.Current.SetControllerFactory(typeof(ControllerFactory));


        }

        protected virtual void Session_Start(object sender, EventArgs e)
        {
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
           NHibernateHelper.BindSession();
        }

        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {
            NHibernateHelper.UnBindSession();
        }
   
    }
}
