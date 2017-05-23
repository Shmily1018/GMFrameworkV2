using System.Web.Mvc;

namespace GoldMantis.Web.Areas.UserEmailAnalysis
{
    public class UserEmailAnalysisAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UserEmailAnalysis";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UserEmailAnalysis_default",
                "UserEmailAnalysis/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}