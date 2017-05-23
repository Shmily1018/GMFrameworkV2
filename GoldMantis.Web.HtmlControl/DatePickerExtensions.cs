using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using GoldMantis.Common;

namespace GoldMantis.Web.HtmlControl
{
    public static class DatePickerExtensions
    {
        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, DatePickerAttribute attribute)
        {
            if (attribute.Value.HasValue)
            {
                attribute.DateBox.Value = attribute.Value.Value.ToString(attribute.Format);
            }

            return DatePickerHelper(attribute.Name, attribute.Width, attribute.GetHtmlAttribute(), htmlHelper.TextBox(attribute.DateBox).ToString());
        }

        public static MvcHtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, DatePickerAttribute<TModel, TProperty> attribute)
        {
            string name = ExpressionHelper.GetExpressionText(attribute.DateBox.Expression);
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            string id = TagBuilder.CreateSanitizedId(fullHtmlFieldName, HtmlHelper.IdAttributeDotReplacement);
            string dateBox = ReplaceDateTime(htmlHelper.TextBoxFor(attribute.DateBox).ToString(), attribute.Format);

            return DatePickerHelper(id, attribute.Width, attribute.GetHtmlAttribute(), dateBox);
        }

        private static MvcHtmlString DatePickerHelper(string name, string width, IDictionary<string, object> htmlAttribute, string dateBox)
        {
            TagBuilder wrapBuilder = new TagBuilder("div");
            wrapBuilder.MergeAttribute("id", string.Format("{0}_wrap", name));
            wrapBuilder.AddCssClass(width);

            TagBuilder groupBuilder = new TagBuilder("div");
            groupBuilder.MergeAttribute("id", string.Format("{0}_group", name));
            groupBuilder.MergeAttributes(htmlAttribute);

            groupBuilder.InnerHtml = string.Format("{0}{1}", dateBox, "<span class='input-group-addon'> <i class='fa fa-calendar'></i> </span>");
            wrapBuilder.InnerHtml = groupBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(wrapBuilder.ToString(TagRenderMode.Normal));
        }

        internal static string ReplaceDateTime(string inputHtml, string formate)
        {
            DateTime date;
            string key = "value";
            string dateStr = HtmlParser.ParseAttributeValue(inputHtml, key);

            if (DateTime.TryParse(dateStr, out date))
            {
                string newDate = date == DateTime.MinValue ? string.Empty : date.ToString(formate);
                inputHtml = HtmlParser.ReplaceAttributeValue(inputHtml, key, newDate);
            }

            return inputHtml;
        }
    }
}