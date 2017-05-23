using System.Web.Routing;

namespace GoldMantis.Web.HtmlControl
{
    public class LinkAttribute : HtmlAttribute
    {
        public LinkAttribute(string linkText, string actionName)
        {
            this.LinkText = linkText;
            this.ActionName = actionName;
        }

        public string LinkText { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string Protocol { get; set; }

        public string HostName { get; set; }

        public string Fragment { get; set; }

        public RouteValueDictionary RouteValues { get; set; }
    }

    public class RouteLinkAttribute : HtmlAttribute
    {
        public RouteLinkAttribute(string linkText, object routeValues)
        {
            this.LinkText = linkText;
            this.RouteValues = new RouteValueDictionary(routeValues);
        }

        public RouteLinkAttribute(string linkText, string routeName)
        {
            this.LinkText = linkText;
            this.RouteName = routeName;
        }

        public string LinkText { get; set; }

        public string RouteName { get; set; }

        public string Protocol { get; set; }

        public string HostName { get; set; }

        public string Fragment { get; set; }

        public RouteValueDictionary RouteValues { get; set; }
    }


}