using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class TableAttribute<T> : HtmlAttribute
    {
        public TableAttribute()
        {
            this.Class.Add(Bootstrap.Table.Table, Bootstrap.Table.TableStriped, Bootstrap.Table.TableBordered,
                Bootstrap.Table.TableHover);

            this.HtmlAttributes.Add("role", "grid");
        }

        public TableRowAttribute<T> TableHeaderRow { get; set; }

        /// <summary>
        /// BodyRow模板，根据DataSource属性自动生成,优先级高于TableBodyRows
        /// </summary>
        public TableRowAttribute<T> TableBodyRowTemplete { get; set; }

        /// <summary>
        /// 把要显示的行都写在这里
        /// </summary>
        public TableRowAttribute<T> TableFooterRow { get; set; }

        public ICollection<T> DataSource { get; set; }
    }

    public class TableAttribute<TModel, TProperty> : HtmlAttribute
    {
        public TableAttribute(Expression<Func<TModel, ICollection<TProperty>>> expression)
        {
            this.Class.Add(Bootstrap.Table.Table, Bootstrap.Table.TableStriped, Bootstrap.Table.TableBordered,
                Bootstrap.Table.TableHover, Bootstrap.Table.TableCondensed, "gm-grid-table");

            this.HtmlAttributes.Add("role", "grid");
            this.Expression = expression;
        }

        public Expression<Func<TModel, ICollection<TProperty>>> Expression { get; set; }

        public TableRowAttribute TableHeaderRow { get; set; }

        /// <summary>
        /// BodyRow模板，根据DataSource属性自动生成,优先级高于TableBodyRows
        /// </summary>
        public TableRowAttribute TableBodyRowTemplete { get; set; }

        /// <summary>
        /// 把要显示的行都写在这里
        /// </summary>
        public TableRowAttribute TableFooterRow { get; set; }

        public void PrepareTableAttribute(string name)
        {
            base.MergeAttributeValue(base.HtmlAttributes, "id", name);
        }
    }
}