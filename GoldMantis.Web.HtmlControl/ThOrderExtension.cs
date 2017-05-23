using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoldMantis.Web.HtmlControl
{
    public static class ThOrderExtension
    {
        public static MvcHtmlString ThOrderHelper<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> propertyExpression, string headerText, object thAttributes = null, object aAttributes = null)
        {
            RouteValueDictionary routeValueDictionary1 = HtmlHelper.AnonymousObjectToHtmlAttributes(thAttributes);
            RouteValueDictionary routeValueDictionary2 = HtmlHelper.AnonymousObjectToHtmlAttributes(aAttributes);
            MemberExpression memberExpression = (MemberExpression)null;
            if (propertyExpression.Body.NodeType == ExpressionType.Convert)
                memberExpression = ((UnaryExpression)propertyExpression.Body).Operand as MemberExpression;
            else if (propertyExpression.Body.NodeType == ExpressionType.MemberAccess)
                memberExpression = propertyExpression.Body as MemberExpression;
            TagBuilder tagBuilder1 = new TagBuilder("th");
            TagBuilder tagBuilder2 = new TagBuilder("span");
            if (thAttributes != null)
                tagBuilder1.MergeAttributes<string, object>((IDictionary<string, object>)routeValueDictionary1);
            if (aAttributes != null)
                tagBuilder2.MergeAttributes<string, object>((IDictionary<string, object>)routeValueDictionary2);
            tagBuilder2.InnerHtml = headerText;

            tagBuilder1.MergeAttribute("data-order-field", memberExpression.Member.Name);
            tagBuilder1.MergeAttribute("class", "sorting");
            tagBuilder1.MergeAttribute("onclick",string.Format("javascript:commonPage.fieldOrder('{0}');", (object)memberExpression.Member.Name));
            tagBuilder1.InnerHtml = tagBuilder2.ToString();
            return new MvcHtmlString(tagBuilder1.ToString());
        }

        public static string ThOrderHelper<T>( Expression<Func<T, object>> propertyExpression, string headerText)
        {

            MemberExpression memberExpression = (MemberExpression)null;
            if (propertyExpression.Body.NodeType == ExpressionType.Convert)
                memberExpression = ((UnaryExpression)propertyExpression.Body).Operand as MemberExpression;
            else if (propertyExpression.Body.NodeType == ExpressionType.MemberAccess)
                memberExpression = propertyExpression.Body as MemberExpression;
            TagBuilder tagBuilder1 = new TagBuilder("th");
            TagBuilder tagBuilder2 = new TagBuilder("a");

            tagBuilder2.MergeAttribute("href", string.Format("javascript:commonPage.fieldOrder('{0}');", (object)memberExpression.Member.Name));
            tagBuilder2.InnerHtml = headerText;
            tagBuilder1.MergeAttribute("data-order-field", memberExpression.Member.Name);
            tagBuilder1.InnerHtml = tagBuilder2.ToString();
            return tagBuilder1.ToString();
        }
    }
}
