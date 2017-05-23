using System;
using System.Linq.Expressions;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class LabelAttribute : HtmlAttribute
    {
        public LabelAttribute(string expression)
        {
            this.Expression = expression;

            //this.Class.Add(Bootstrap.Grid.ExtraSmall(2));
        }

        public string Expression { get; set; }
        public string LabelText { get; set; }
    }



    public class LabelAttribute<TModel, TProperty> : HtmlAttribute<TModel, TProperty>
    {
        public LabelAttribute(Expression<Func<TModel, TProperty>> expression) : base(expression)
        {
            //this.Class.Add(Bootstrap.Grid.ExtraSmall(2));
        }

        public string LabelText { get; set; }
    }
}