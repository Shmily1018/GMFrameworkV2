using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GoldMantis.Web.HtmlControl
{
    public static class ButtonExtensions
    {
        public static MvcHtmlString Button(this HtmlHelper htmlHelper, ButtonAttribute attribute)
        {
            return htmlHelper.Button(attribute.ButtonType, attribute.Text, attribute.GetHtmlAttribute());
        }


        internal static MvcHtmlString Button(this HtmlHelper htmlHelper, ButtonType btntType, string text, IDictionary<string,object> htmlAttribute)
        {
            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", btntType.ToString().ToLower());
            tagBuilder.MergeAttribute("value", text);
            tagBuilder.MergeAttributes(htmlAttribute);

            return tagBuilder.ToMvcHtmlString(TagRenderMode.SelfClosing);
        }
    }

}