using System;
using System.Collections.Generic;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class TableRowAttribute<T> : HtmlAttribute
    {
        public TableRowAttribute()
        {
            this.TableCells = new List<TableCellAttribute<T>>();

            base.Class.Add(Bootstrap.Text.TextCenter);
            base.HtmlAttributes.Add("role","row");
        }

        public T DelegateObject { get; set; }

        public List<Func<T, KeyValuePair<string, object>>> HtmlAttributeDelegates { get; set; }

        public ICollection<TableCellAttribute<T>> TableCells { get; private set; }

        public override IDictionary<string, object> GetHtmlAttribute()
        {
            if (HtmlAttributeDelegates.HasItems())
            {
                foreach (Func<T, KeyValuePair<string,object>> deleg in this.HtmlAttributeDelegates)
                {
                    KeyValuePair<string, object> pair = deleg(DelegateObject);
                    base.MergeAttributeValue(base.HtmlAttributes, pair.Key, pair.Value);
                }
            }

            return base.GetHtmlAttribute();
        }
    }

    public class TableRowAttribute : HtmlAttribute
    {
        public TableRowAttribute()
        {
            this.TableCells = new List<TableCellAttribute>();

            base.Class.Add(Bootstrap.Text.TextCenter);
            base.HtmlAttributes.Add("role", "row");
        }

        public List<Func<int, KeyValuePair<string, object>>> HtmlAttributeDelegates { get; set; }

        public ICollection<TableCellAttribute> TableCells { get; private set; }

        public int Index { get; set; }

        public override IDictionary<string, object> GetHtmlAttribute()
        {
            if (HtmlAttributeDelegates.HasItems())
            {
                foreach (Func<int, KeyValuePair<string, object>> deleg in this.HtmlAttributeDelegates)
                {
                    KeyValuePair<string, object> pair = deleg(this.Index);
                    base.MergeAttributeValue(base.HtmlAttributes, pair.Key, pair.Value);
                }
            }

            return base.GetHtmlAttribute();
        }
    }
}