using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Routing;
using GoldMantis.Common;
using System.Web.Mvc;
using GoldMantis.Web.HtmlControl.Classes;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class TextAreaAttribute : HtmlAttribute
    {
        public TextAreaAttribute(string name)
        {
            this.Name = name;
            this.Rows = 5;
            this.Columns = 100;

            this.Class.Add(Bootstrap.Form.FormControl);
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }
    }


    public class TextAreaAttribute<TModel, TProperty> : HtmlAttribute<TModel, TProperty>
    {
        public TextAreaAttribute(Expression<Func<TModel, TProperty>> expression)
            : base(expression)
        {
            this.Rows = 5;
            this.Columns = 100;

            this.Class.Add(Bootstrap.Form.FormControl);
        }

        public int Rows { get; set; }

        public int Columns { get; set; }
    }
}