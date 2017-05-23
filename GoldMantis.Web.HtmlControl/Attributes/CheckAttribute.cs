using System;
using System.Linq.Expressions;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class CheckAttribute : HtmlAttribute
    {
        public CheckAttribute(string name)
        {
            this.Name = name;
            this.Class.Add(Bootstrap.Grid.ExtraSmall(1), Bootstrap.Form.FormControl, Bootstrap.Form.InputSm);
        }

        public string Name { get; set; }

        public bool IsChecked { get; set; }
    }

    public class CheckAttribute<TModel> : HtmlAttribute<TModel, bool>
    {
        public CheckAttribute(Expression<Func<TModel, bool>> expression)
            : base(expression)
        {
            this.Class.Add(Bootstrap.Grid.ExtraSmall(1), Bootstrap.Form.FormControl, Bootstrap.Form.InputSm);
        }
    }
}