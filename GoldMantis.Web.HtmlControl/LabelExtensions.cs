using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Classes;

namespace GoldMantis.Web.HtmlControl
{
    public static class LabelExtensions
    {
        public static MvcHtmlString Label(this HtmlHelper htmlHelper, LabelAttribute attribute)
        {
            return htmlHelper.Label(attribute.Expression, attribute.LabelText, attribute.GetHtmlAttribute());
        }

        public static MvcHtmlString LabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, LabelAttribute<TModel, TProperty> attribute)
        {
            return htmlHelper.LabelFor(attribute.Expression, attribute.LabelText, attribute.GetHtmlAttribute());
        }

    }
}