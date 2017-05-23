using System.Web.Mvc;

namespace GoldMantis.Web.Areas.UploadFileManage
{
    public class UploadFileManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UploadFileManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UploadFileManage_default",
                "UploadFileManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}