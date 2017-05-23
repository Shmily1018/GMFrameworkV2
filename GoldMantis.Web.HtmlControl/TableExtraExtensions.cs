using System.Web.Mvc;

namespace GoldMantis.Web.HtmlControl
{
    public static class TableExtraExtensions
    {
        public static MvcHtmlString TableBanner(this HtmlHelper htmlHelper, int count)
        {
            string banner = string.Format( "<div class='alert alert-warning text-center' style='margin-bottom: 0px; padding: 8px;'><label>共 {0} 条数据</label></div>", count);

            return MvcHtmlString.Create(banner);
        }
    }
}