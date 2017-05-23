using System.Collections.Generic;
using System.Web.Mvc;
using GoldMantis.Common;

namespace GoldMantis.Web.HtmlControl
{
    public static class TableCellExtensions
    {
        public static MvcHtmlString TableCell<T>(this HtmlHelper htmlHelper, TableCellAttribute<T> attribute)
        {
            string innerHtml = string.Empty;
            bool order = false;

            if (attribute.InnerHtmlDelegate.IsNotNull())
            {
                innerHtml = attribute.InnerHtmlDelegate(attribute.DelegateObject).ToString();
            }
            else if (!attribute.InnerHtml.IsNullOrEmpty())
            {
                innerHtml = attribute.InnerHtml;
            }
            else if (!attribute.InnerText.IsNullOrEmpty())
            {
                innerHtml = attribute.InnerText;
            }
            else if (attribute.Order)
            {
                order = true;
            }

            return TableCellHelper(htmlHelper, attribute.GetHtmlAttribute(), attribute.IsHeadCell, innerHtml,order);
        }

        public static MvcHtmlString TableCellFor(this HtmlHelper htmlHelper, TableCellAttribute attribute)
        {
            string innerHtml = string.Empty;

            if (attribute.InnerHtmlDelegate.IsNotNull())
            {
                innerHtml = attribute.InnerHtmlDelegate(attribute.Index).ToString();
            }
            else if (!attribute.InnerHtml.IsNullOrEmpty())
            {
                innerHtml = attribute.InnerHtml;
            }
            else if (!attribute.InnerText.IsNullOrEmpty())
            {
                innerHtml = attribute.InnerText;
            }

            return TableCellHelper(htmlHelper, attribute.GetHtmlAttribute(), attribute.IsHeadCell, innerHtml);
        }

        internal static MvcHtmlString TableCellHelper(HtmlHelper htmlHelper, IDictionary<string, object> htmlAttribute,
            bool isHeadCell, string innerHtml,bool? order=false)
        {
            string tdTag = isHeadCell ? "th" : "td";
            TagBuilder tdTagBuilder = new TagBuilder(tdTag);
            tdTagBuilder.MergeAttributes(htmlAttribute);

            TagBuilder textHiddenDiv = new TagBuilder("div");
            textHiddenDiv.AddCssClass("text-hidden");

            textHiddenDiv.InnerHtml = innerHtml;
            tdTagBuilder.InnerHtml = textHiddenDiv.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tdTagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}