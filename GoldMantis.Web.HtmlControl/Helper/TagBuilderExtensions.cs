using System.Web.Mvc;

namespace GoldMantis.Web.HtmlControl
{
    public static class TagBuilderExtensions
    {
        public static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
        {
            return new MvcHtmlString(tagBuilder.ToString(renderMode));
        }
    }


}