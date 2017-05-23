using System;
using System.Linq.Expressions;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class TextBoxAttribute : HtmlAttribute
    {
        public TextBoxAttribute(string name)
        {
            this.Name = name;
            this.Class.Add(Bootstrap.Form.FormControl);
        }

        public TextBoxAttribute()
        {
            this.Class.Add(Bootstrap.Form.FormControl);
        }

        public string Name { get; set; }

        public object Value { get; set; }

        public string Format { get; set; }
    }

    public class TextBoxAttribute<TModel, TProperty> : HtmlAttribute<TModel, TProperty>
    {
        public TextBoxAttribute(Expression<Func<TModel, TProperty>> expression) : base(expression)
        {
            this.Class.Add(Bootstrap.Form.FormControl);
        }

        public string Format { get; set; }
    }
}