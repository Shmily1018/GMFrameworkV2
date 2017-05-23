using System.Web.Mvc;
using System.Web.Routing;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class FormAttribute : HtmlAttribute
    {
        public FormAttribute()
        {
            this.HtmlAttributes.Add("role", "form");
            this.Class.Add(Bootstrap.Form.FormHorizontal);
            this.Method = FormMethod.Post;
        }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public RouteValueDictionary RouteValues { get; set; }

        public FormMethod Method { get; set; }
    }

    public class RouteFormAttribute : HtmlAttribute
    {
        public RouteFormAttribute()
        {
            this.HtmlAttributes.Add("role", "form");
            this.Class.Add(Bootstrap.Form.FormHorizontal);
        }

        public string RouteName { get; set; }

        public RouteValueDictionary RouteValues { get; set; }

        public FormMethod Method { get; set; }
    }
}