using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GoldMantis.Common;

namespace GoldMantis.Web.HtmlControl
{
    public class TableCellAttribute<T> : HtmlAttribute
    {
        public TableCellAttribute()
        {
        }

        public TableCellAttribute(bool isHeadCell)
        {
            this.IsHeadCell = isHeadCell;
        }

        public TableCellAttribute(T value) : this(value, false)
        {
        }

        public TableCellAttribute(T value, bool isHeadCell)
        {
            this.IsHeadCell = isHeadCell;
            this.DelegateObject = value;
        }

        /*
         * InnerText,InnerHtml,InnerHtmlDelegate只取其中之一，优先级依次增高
         */
        public string InnerText { get; set; }

        public bool Order { get; set; }

        public string InnerHtml { get; set; }

        public Func<T, object> InnerHtmlDelegate { get; set; }

        public int CellWidth { get; set; }

        public bool IsHeadCell { get; set; }

        public T DelegateObject { get; set; }

        public string TipText { get; set; }

        /// <summary>
        /// 优先级高于TipText
        /// </summary>
        public Func<T, object> TipTextDelegate { get; set; }

        public List<Func<T, KeyValuePair<string, object>>> HtmlAttributeDelegates { get; set; }

        public override IDictionary<string, object> GetHtmlAttribute()
        {
            if (HtmlAttributeDelegates.HasItems())
            {
                foreach (Func<T, KeyValuePair<string, object>> deleg in this.HtmlAttributeDelegates)
                {
                    KeyValuePair<string, object> pair = deleg(DelegateObject);
                    base.MergeAttributeValue(base.HtmlAttributes, pair.Key, pair.Value);
                }
            }


            base.GetHtmlAttribute();

            /** TipText */
            string tipText = string.Empty;

            if (this.TipTextDelegate.IsNotNull())
            {
                tipText = this.TipTextDelegate(this.DelegateObject).ToString();
            }
            else if (!this.TipText.IsNullOrEmpty())
            {
                tipText = this.TipText;
                
            }

            if (!tipText.IsNullOrEmpty())
            {
                base.MergeAttributeValue(base.HtmlAttributes, "td-data-content", tipText);
            }

            /** CellWidth */
            if (CellWidth > 0)
            {
                // 由于不建议用户写style控制样式，所以这个地方直接覆盖
                base.MergeAttributeValue(base.HtmlAttributes, "style", string.Format("vertical-align: middle; width: {0}px;", this.CellWidth));
            }

            return base.HtmlAttributes;
        }
    }

    public class TableCellAttribute : HtmlAttribute
    {
        public TableCellAttribute(string innerHtml)
        {
            this.InnerHtml = innerHtml;
        }

        public TableCellAttribute(Func<int, object> innerHtmlDelegate)
        {
            this.InnerHtmlDelegate = innerHtmlDelegate;
        }

        public string InnerText { get; set; }

        public string InnerHtml { get; set; }

        public int CellWidth { get; set; }

        public bool IsHeadCell { get; set; }

        public int Index { get; set; }

        public Func<int, object> InnerHtmlDelegate { get; set; }

        public string TipText { get; set; }

        public Func<int, object> TipTextDelegate { get; set; }

        public List<Func<int, KeyValuePair<string, object>>> HtmlAttributeDelegates { get; set; }

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


            base.GetHtmlAttribute();

            /** TipText */
            string tipText = string.Empty;

            if (this.TipTextDelegate.IsNotNull())
            {
                tipText = this.TipTextDelegate(this.Index).ToString();
            }
            else if (!this.TipText.IsNullOrEmpty())
            {
                tipText = this.TipText;

            }

            if (!tipText.IsNullOrEmpty())
            {
                base.MergeAttributeValue(base.HtmlAttributes, "td-data-content", tipText);
            }

            /** CellWidth */
            if (CellWidth > 0)
            {
                // 由于不建议用户写style控制样式，所以这个地方直接覆盖
                base.MergeAttributeValue(base.HtmlAttributes, "style", string.Format("vertical-align: middle; width: {0}px;", this.CellWidth));
            }

            return base.HtmlAttributes;
        }
    }
}