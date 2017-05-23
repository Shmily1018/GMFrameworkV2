using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace GoldMantis.Web.HtmlControl
{
    public static class DateRangeExtensions
    {
        public static MvcHtmlString DateRange(this HtmlHelper htmlHelper, DateRangeAttribute attribute)
        {
            TagBuilder wrapBuilder = new TagBuilder("div");
            wrapBuilder.MergeAttribute("id", string.Format("{0}_wrap", attribute.Name));
            wrapBuilder.AddCssClass(attribute.Width);

            TagBuilder groupBuilder = new TagBuilder("div");
            groupBuilder.MergeAttribute("id", string.Format("{0}_group", attribute.Name));
            groupBuilder.MergeAttributes(attribute.GetHtmlAttribute());

            groupBuilder.InnerHtml = string.Format("{0}{1}{2}",
                htmlHelper.TextBox(attribute.DateBox),
                "<span class='input-group-addon'>~</span>",
                htmlHelper.TextBox(attribute.DateBoxEnd));

            wrapBuilder.InnerHtml = groupBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(wrapBuilder.ToString(TagRenderMode.Normal));
        }
    }
}