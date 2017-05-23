using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Classes;

namespace GoldMantis.Web.HtmlControl
{
    public static class FormExtensions
    {
        public static MvcForm BeginForm(this HtmlHelper htmlHelper, FormAttribute attribute)
        {
            return htmlHelper.BeginForm(attribute.ActionName, attribute.ControllerName, attribute.RouteValues, attribute.Method, attribute.GetHtmlAttribute());
        }

        public static MvcForm BeginRouteForm(this HtmlHelper htmlHelper, RouteFormAttribute attribute)
        {
            return htmlHelper.BeginRouteForm(attribute.RouteName, attribute.RouteValues, attribute.Method, attribute.GetHtmlAttribute());
        }
    }
}