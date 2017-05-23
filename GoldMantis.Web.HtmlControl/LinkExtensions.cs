using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace GoldMantis.Web.HtmlControl
{
    public static class LinkExtensions
    {
        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, LinkAttribute attribute)
        {
            return htmlHelper.ActionLink(attribute.LinkText, attribute.ActionName, attribute.ControllerName, attribute.Protocol, 
                attribute.HostName, attribute.Fragment, attribute.RouteValues, attribute.GetHtmlAttribute());
        }

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, RouteLinkAttribute attribute)
        {
            return htmlHelper.RouteLink(attribute.LinkText, attribute.RouteName, attribute.Protocol, attribute.HostName,
                attribute.Fragment, attribute.RouteValues, attribute.GetHtmlAttribute());
        }
    }
}