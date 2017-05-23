using System.Web.Mvc;
using  System.Web.Mvc.Html;

namespace GoldMantis.Web.HtmlControl
{
    public static class EditorExtensions
    {
        public static MvcHtmlString Editor(this HtmlHelper htmlHelper, EditorAttribute attribute)
        {
            return htmlHelper.Editor(attribute.Expression, attribute.TemplateName, attribute.HtmlFieldName, attribute.AdditionalViewData);
        }

        public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, EditorAttribute<TModel, TValue> attribute)
        {
            return htmlHelper.EditorFor(attribute.Expression, attribute.TemplateName, attribute.HtmlFieldName, attribute.AdditionalViewData);
        }
    }
}