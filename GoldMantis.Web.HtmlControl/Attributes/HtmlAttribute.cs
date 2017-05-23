using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Routing;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Classes;
using System.Web.Mvc;

namespace GoldMantis.Web.HtmlControl
{
    public class HtmlAttribute : IHtmlAttribute
    {
        public HtmlAttribute()
        {
            this.HtmlAttributes = new RouteValueDictionary();
            this.Class = new ControlClass();

            this.HtmlAttributes.Add("autocomplete", "off");
        }

        public virtual ControlClass Class { get; protected set; }

        public virtual object ObjectAttribute { get; set; }

        public virtual IDictionary<string, object> HtmlAttributes { get; protected set; }

        public virtual IDictionary<string, object> GetHtmlAttribute()
        {
            // AnonymousObjectToHtmlAttributes方法会对参数判断null，且参数为null时返回一个空的RouteValueDictionary（不是null）
            RouteValueDictionary objDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(this.ObjectAttribute);

            // HtmlAttribute不包含ObjectAttribute中的键值，则添加
            // HtmlAttribute 优先级  高于  ObjectAttribute
            foreach (string key in objDictionary.Keys)
            {
                if (!this.HtmlAttributes.ContainsKey(key))
                {
                    this.HtmlAttributes.Add(key, objDictionary[key]);
                }
            }

            this.Class.InflateClass(this.HtmlAttributes);
            
            
            return this.HtmlAttributes;
        }

        protected virtual void MergeAttributeValue(IDictionary<string, object> attribute, string key, object value)
        {
            if (attribute.ContainsKey(key))
            {
                attribute[key] = value;
            }
            else
            {
                attribute.Add(key, value);
            }
        }
    }

    public class HtmlAttribute<TModel, TProperty> : HtmlAttribute
    {
        public HtmlAttribute(Expression<Func<TModel, TProperty>> expression)
        {
            this.Expression = expression;
        }

        public Expression<Func<TModel, TProperty>> Expression { get; set; }
    }
}