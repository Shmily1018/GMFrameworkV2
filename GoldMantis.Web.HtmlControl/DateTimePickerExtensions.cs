using System.Collections.Generic;
using System.Web.Mvc;

namespace GoldMantis.Web.HtmlControl
{
    public static class DateTimePickerExtensions
    {
        public static MvcHtmlString DateTimePicker(this HtmlHelper htmlHelper, DateTimePickerAttribute attribute)
        {
            if (attribute.Value.HasValue)
            {
                attribute.DateBox.Value = attribute.Value.Value.ToString(attribute.Format);
            }

            return DateTimePickerHelper(attribute.Name, attribute.Width, attribute.GetHtmlAttribute(), htmlHelper.TextBox(attribute.DateBox).ToString());
        }

        public static MvcHtmlString DateTimePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, DateTimePickerAttribute<TModel, TProperty> attribute)
        {
            string name = ExpressionHelper.GetExpressionText(attribute.DateBox.Expression);
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            string id = TagBuilder.CreateSanitizedId(fullHtmlFieldName, HtmlHelper.IdAttributeDotReplacement);
            string dateBox = DatePickerExtensions.ReplaceDateTime(htmlHelper.TextBoxFor(attribute.DateBox).ToString(), attribute.Format);

            return DateTimePickerHelper(id, attribute.Width, attribute.GetHtmlAttribute(), dateBox);
        }

        private static MvcHtmlString DateTimePickerHelper(string name, string width, IDictionary<string, object> htmlAttribute, string dateBox)
        {
            TagBuilder wrapBuilder = new TagBuilder("div");
            wrapBuilder.MergeAttribute("id", string.Format("{0}_wrap", name));
            wrapBuilder.AddCssClass(width);

            TagBuilder groupBuilder = new TagBuilder("div");
            groupBuilder.MergeAttribute("id", string.Format("{0}_group", name));
            groupBuilder.MergeAttributes(htmlAttribute);

            groupBuilder.InnerHtml = string.Format("{0}{1}", dateBox, "<span class='input-group-addon'> <i class='fa fa-calendar date-set'></i> </span>");
            wrapBuilder.InnerHtml = groupBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(wrapBuilder.ToString(TagRenderMode.Normal));
        }

    }
}