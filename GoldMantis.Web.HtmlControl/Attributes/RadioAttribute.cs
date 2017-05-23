using System;
using System.Linq.Expressions;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class RadioAttribute : HtmlAttribute
    {
        public RadioAttribute(string name)
        {
            this.Name = name;
            this.Class.Add(Bootstrap.Grid.ExtraSmall(1), Bootstrap.Form.FormControl, Bootstrap.Form.InputSm);
        }

        public string Name { get; set; }

        public object Value { get; set; } 

        public bool IsChecked { get; set; }
    }

    public class RadioAttribute<TModel, TProperty> : HtmlAttribute<TModel, TProperty>
    {
        public RadioAttribute(Expression<Func<TModel, TProperty>> expression) : base(expression)
        {
            this.Class.Add(Bootstrap.Grid.ExtraSmall(1), Bootstrap.Form.FormControl, Bootstrap.Form.InputSm);
        }

        public object Value { get; set; } 
    }

}