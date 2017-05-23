using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Classes;

namespace GoldMantis.Web.HtmlControl
{
    public static class TextAreaExtensions
    {
        public static MvcHtmlString TextArea(this HtmlHelper htmlHelper, TextAreaAttribute attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.TextArea(attribute.Name, attribute.Value, attribute.Rows, attribute.Columns, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString TextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, TextAreaAttribute<TModel, TProperty> attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.TextAreaFor(attribute.Expression, attribute.Rows, attribute.Columns, attribute.GetHtmlAttribute()), attribute);
        }
    }
}