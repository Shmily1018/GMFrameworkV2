using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.SessionState;
using GoldMantis.Common;

namespace GoldMantis.ServiceNew
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

            HostingEnvironment.RegisterVirtualPathProvider(new ServicePathProvider());

            NHibernateHelper.Configure();

            //AssemblyCache.Initialize();

            //生成Exe文件
            if (!File.Exists(HttpRuntime.BinDirectory + DynamicGenerateAssembly.EXE_FILE_NAME))
            {
                DynamicGenerateAssembly.CreateExe();
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}