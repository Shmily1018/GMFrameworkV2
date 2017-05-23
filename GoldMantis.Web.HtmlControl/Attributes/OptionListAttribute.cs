using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GoldMantis.Web.HtmlControl.Styles;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Classes;

namespace GoldMantis.Web.HtmlControl
{
    public class OptionListAttribute : HtmlAttribute
    {
        public OptionListAttribute(string name)
        {
            this.Name = name;
            this.Enable = true;
            this.Visible = true;
            this.TextAlign = TextAlign.Right;
            this.OptionBoxList = new List<SelectListItem>();
            //this.Class.Add(Bootstrap.Grid.ExtraSmall(4));

            this.ItemClass = new ControlClass();
            //this.ItemClass.Add(Bootstrap.Grid.ExtraSmall(3));
        }

        public string Name { get; set; }

        public bool Enable { get; set; }

        public bool Visible { get; set; }

        public TextAlign TextAlign { get; set; }

        public List<SelectListItem> OptionBoxList { get; protected set; }

        public ControlClass ItemClass { get; protected set; }

        public override IDictionary<string, object> GetHtmlAttribute()
        {
            if (!this.Visible)
            {
                this.Class.Add(Bootstrap.Typesetting.Hidden);
            }

            return base.GetHtmlAttribute();
        }

        public IDictionary<string, object> GetItemHtmlAttribute()
        {
            RouteValueDictionary itemHtmlAttribute = new RouteValueDictionary();
            itemHtmlAttribute.Add(ControlClass.ClassAttribute, ItemClass.ToString());

            return itemHtmlAttribute;
        }
    }

    public class OptionListAttribute<TModel, TProperty> : HtmlAttribute<TModel, TProperty>
    {
        public OptionListAttribute(Expression<Func<TModel, TProperty>> expression)
            : this(expression, new List<SelectListItem>())
        {
        }

        public OptionListAttribute(Expression<Func<TModel, TProperty>> expression, List<SelectListItem> optionBoxList)
            : base(expression)
        {
            this.Enable = true;
            this.Visible = true;
            this.TextAlign = TextAlign.Right;
            this.OptionBoxList = optionBoxList;
            //this.Class.Add(Bootstrap.Grid.ExtraSmall(4));

            this.ItemClass = new ControlClass();
            //this.ItemClass.Add(Bootstrap.Grid.ExtraSmall(3));
        }

        public bool Enable { get; set; }

        public bool Visible { get; set; }

        public TextAlign TextAlign { get; set; }

        public List<SelectListItem> OptionBoxList { get; protected set; }

        public ControlClass ItemClass { get; protected set; }

        public override IDictionary<string, object> GetHtmlAttribute()
        {
            if (!this.Visible)
            {
                this.Class.Add(Bootstrap.Typesetting.Hidden);
            }

            return base.GetHtmlAttribute();
        }

        public IDictionary<string, object> GetItemHtmlAttribute()
        {
            RouteValueDictionary itemHtmlAttribute = new RouteValueDictionary();
            itemHtmlAttribute.Add(ControlClass.ClassAttribute, ItemClass.ToString());
            
            return itemHtmlAttribute;
        }
    }


}