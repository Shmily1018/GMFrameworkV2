using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Classes;

namespace GoldMantis.Web.HtmlControl
{
    public static class SelectExtensions
    {
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, SelectAttribute attribute)
        {
            return htmlHelper.WrapHtmlControl( html => html.DropDownList(attribute.Name, attribute.SelectList, attribute.OptionLabel, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, SelectAttribute<TModel, TProperty> attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.DropDownListFor(attribute.Expression,attribute.SelectList,attribute.OptionLabel,attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString ListBox(this HtmlHelper htmlHelper, SelectAttribute attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.ListBox(attribute.Name, attribute.SelectList, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString ListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, SelectAttribute<TModel, TProperty> attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.ListBoxFor(attribute.Expression, attribute.SelectList, attribute.GetHtmlAttribute()), attribute);
        }
    }
}