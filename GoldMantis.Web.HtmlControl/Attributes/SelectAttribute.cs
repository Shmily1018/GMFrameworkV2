using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class SelectAttribute : HtmlAttribute
    {
        public SelectAttribute(string name)
        {
            this.Name = name;
            this.SelectList = new List<SelectListItem>();
            this.Class.Add(Bootstrap.Form.FormControl);
        }

        public string Name { get; set; }

        public List<SelectListItem> SelectList { get; set; }

        public string OptionLabel { get; set; }
    }

    public class SelectAttribute<TModel, TProperty> : HtmlAttribute<TModel, TProperty>
    {
        public SelectAttribute(Expression<Func<TModel, TProperty>> expression) : base(expression)
        {
            this.SelectList = new List<SelectListItem>();
            this.Class.Add(Bootstrap.Form.FormControl);
        }

        public List<SelectListItem> SelectList { get; set; }

        public string OptionLabel { get; set; }
    }
}