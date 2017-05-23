using System;
using System.Linq.Expressions;

namespace GoldMantis.Web.HtmlControl
{
    public class EditorAttribute : HtmlAttribute
    {
        public EditorAttribute(string expression)
        {
            this.Expression = expression;
        }

        public string Expression { get; set; }

        public string TemplateName { get; set; }

        public string HtmlFieldName { get; set; }

        public object AdditionalViewData { get; set; }
    }

    public class EditorAttribute<TModel, TProperty> : HtmlAttribute<TModel, TProperty>
    {
        public EditorAttribute(Expression<Func<TModel, TProperty>> expression) : base(expression)
        {
        }

        public string TemplateName { get; set; }

        public string HtmlFieldName { get; set; }

        public object AdditionalViewData { get; set; }
    }
}