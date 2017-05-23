using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using GoldMantis.Web.HtmlControl.Classes;

namespace GoldMantis.Web.HtmlControl
{
    public static class ValueExtensions
    {
        public static MvcHtmlString Value(this HtmlHelper html, string name, ControlClass controlClass)
        {
            return html.Value(name, null, controlClass);
        }

        public static MvcHtmlString Value(this HtmlHelper html, string name, string format, ControlClass controlClass)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            return ValueForHelper(html, name, null, format, true, controlClass);
        }

        public static MvcHtmlString ValueFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, ControlClass controlClass)
        {
            return html.ValueFor(expression, null, controlClass);
        }

        public static MvcHtmlString ValueFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string format, ControlClass controlClass)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            return ValueForHelper(html, System.Web.Mvc.ExpressionHelper.GetExpressionText(expression), metadata.Model, format, false, controlClass);
        }

        public static MvcHtmlString ValueForHelper(HtmlHelper html, string name, object value, string format, bool useViewData, ControlClass controlClass)
        {
            string str3;
            string fullHtmlFieldName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            string modelStateValue = (string)html.GetModelStateValue(fullHtmlFieldName, typeof(string));

            if (modelStateValue != null)
            {
                str3 = modelStateValue;
            }
            else if (useViewData)
            {
                if (name.Length == 0)
                {
                    ModelMetadata metadata = ModelMetadata.FromStringExpression(string.Empty, html.ViewContext.ViewData);
                    str3 = html.FormatValue(metadata.Model, format);
                }
                else
                {
                    str3 = html.EvalString(name, format);
                }
            }
            else
            {
                str3 = html.FormatValue(value, format);
            }


            //return MvcHtmlString.Create(html.AttributeEncode(str3));
            return html.Label(str3, controlClass);
        }
    }
}