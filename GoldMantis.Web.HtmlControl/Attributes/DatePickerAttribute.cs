using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class DatePickerAttribute : HtmlAttribute
    {
        public DatePickerAttribute(string name)
        {
            this.Name = name;
            //this.Width = Bootstrap.Grid.ExtraSmall(4);
            this.Format = FormatString.DateFormat;
            this.WeekStart = Week.Sunday;
            //this.DefaultDate = DateTime.Now;
            base.Class.Add(Bootstrap.Form.InputGroup, "date", "input-date");

            this.DateBox = new TextBoxAttribute(name);
            this.DateBox.HtmlAttributes.Add("readonly", "readonly");
        }

        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 时间值
        /// </summary>
        public DateTime? Value { get; set; }

        /// <summary>
        /// 日期格式化字符串
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 一周的哪一天作为开始
        /// </summary>
        public Week WeekStart { get; set; }

        /// <summary>
        /// 默认日期
        /// </summary>
        public DateTime? DefaultDate { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 一周里那些天禁用
        /// </summary>
        public List<Week> DaysOfWeekDisabled { get; set; }

        /// <summary>
        /// 控件的宽度。使用Bootstrap.Grid赋值
        /// </summary>
        public string Width { get; set; }

        public TextBoxAttribute DateBox { get; private set; }

        public override IDictionary<string, object> GetHtmlAttribute()
        {
            base.GetHtmlAttribute();
            base.MergeAttributeValue(base.HtmlAttributes, "data-date-week-start",this.WeekStart.GetHashCode());

            if (this.DefaultDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date", this.DefaultDate.Value.ToDateString());
            }

            if (!this.Format.IsNullOrEmpty())
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-format", this.Format.ToLower());
            }

            if (this.StartDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-start-date", this.StartDate.Value.ToDateString());
            }

            if (this.EndDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-end-date",this.EndDate.Value.ToDateString());
            }

            if (this.DaysOfWeekDisabled.HasItems())
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-days-of-week-disabled", string.Join(",", this.DaysOfWeekDisabled.Select(d => d.GetHashCode())));
            }

            return base.HtmlAttributes;
        }
    }


    public class DatePickerAttribute<TModel, TProperty> : HtmlAttribute<TModel, TProperty>
    {
        public DatePickerAttribute(Expression<Func<TModel, TProperty>> expression)
            : base(expression)
        {
            if (!typeof (TProperty).IsAssignableFrom(typeof (DateTime)) &&
                !typeof (TProperty).IsAssignableFrom(typeof (DateTime?)))
            {
                throw new ApplicationException("DatePicker只能为DateTime或者DateTime？类型生成UI组件");
            }

            this.Format = FormatString.DateFormat;
            this.WeekStart = Week.Sunday;
            //this.DefaultDate = DateTime.Now;
            base.Class.Add(Bootstrap.Form.InputGroup, "date", "input-date");

            this.DateBox = new TextBoxAttribute<TModel, TProperty>(expression);
            this.DateBox.HtmlAttributes.Add("readonly", "readonly");
        }

        /// <summary>
        /// 日期格式化字符串
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 一周的哪一天作为开始
        /// </summary>
        public Week WeekStart { get; set; }

        /// <summary>
        /// 默认日期
        /// </summary>
        public DateTime? DefaultDate { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 一周里那些天禁用
        /// </summary>
        public List<Week> DaysOfWeekDisabled { get; set; }

        /// <summary>
        /// 控件的宽度。使用Bootstrap.Grid赋值
        /// </summary>
        public string Width { get; set; }

        public TextBoxAttribute<TModel, TProperty> DateBox { get; private set; }

        public override IDictionary<string, object> GetHtmlAttribute()
        {
            base.GetHtmlAttribute();
            base.MergeAttributeValue(base.HtmlAttributes, "data-date-week-start", this.WeekStart.GetHashCode());

            if (this.DefaultDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date", this.DefaultDate.Value.ToDateString());
            }

            if (!this.Format.IsNullOrEmpty())
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-format", this.Format.ToLower());
            }

            if (this.StartDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-start-date", this.StartDate.Value.ToDateString());
            }

            if (this.EndDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-end-date", this.EndDate.Value.ToDateString());
            }

            if (this.DaysOfWeekDisabled.HasItems())
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-days-of-week-disabled", string.Join(",", this.DaysOfWeekDisabled.Select(d => d.GetHashCode())));
            }

            return base.HtmlAttributes;
        }
    }


    public enum Week
    {
        /// <summary>
        /// 星期天 
        /// </summary>
        Sunday = 0,

        /// <summary>
        /// 星期一 
        /// </summary>
        Monday,

        /// <summary>
        /// 星期二 
        /// </summary>
        Tuesday,

        /// <summary>
        /// 星期三 
        /// </summary>
        Wednesday,

        /// <summary>
        /// 星期四 
        /// </summary>
        Thursday,

        /// <summary>
        /// 星期五 
        /// </summary>
        Friday,

        /// <summary>
        /// 星期六 
        /// </summary>
        Saturday
    }
}