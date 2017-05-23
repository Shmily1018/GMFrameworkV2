using System.Web.Mvc;

namespace GoldMantis.Web.Areas.WorkFlow
{
    public class WorkFlowAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WorkFlow";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WorkFlow_default",
                "WorkFlow/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}