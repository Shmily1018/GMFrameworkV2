using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GoldMantis.Common;

namespace GoldMantis.Web.HtmlControl
{
    public class DateTimePickerAttribute : DatePickerAttribute
    {
        private int _minuteStep;

        public DateTimePickerAttribute(string name) : base(name)
        {
            base.Format = FormatString.DateTimeFormat;
            base.Class.Remove("input-date").Add("input-datetime");
            _minuteStep = 5;
        }

        /// <summary>
        /// 分钟间隔
        /// </summary>
        public int MinuteStep
        {
            get { return _minuteStep; }
            set
            {
                _minuteStep = value;

                if (_minuteStep < 1)
                {
                    _minuteStep = 1;
                }

                if (_minuteStep > 60)
                {
                    _minuteStep = 60;
                }
            }
        }

        public override IDictionary<string, object> GetHtmlAttribute()
        {
            base.GetHtmlAttribute();
            base.HtmlAttributes.Remove("data-date-week-start");
            base.HtmlAttributes.Remove("data-date-start-date");
            base.HtmlAttributes.Remove("data-date-end-date");
            base.MergeAttributeValue(base.HtmlAttributes, "data-date-weekstart", this.WeekStart.GetHashCode());
            base.MergeAttributeValue(base.HtmlAttributes, "data-minute-step", this.MinuteStep);

            if (this.DefaultDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date", this.DefaultDate.Value.ToDateTimeString());
            }

            if (!this.Format.IsNullOrEmpty())
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-format", this.Format.Replace("m","i").ToLower());
            }

            if (this.StartDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-startdate", this.StartDate.Value.ToDateString());
            }

            if (this.EndDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-enddate", string.Format("{0} 23:59:59", this.EndDate.Value.ToDateString()));
            }

            if (this.DaysOfWeekDisabled.HasItems())
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-days-of-week-disabled", string.Join(",", this.DaysOfWeekDisabled.Select(d => d.GetHashCode())));
            }

            return base.HtmlAttributes;
        }
    }

    public class DateTimePickerAttribute<TModel, TProperty> : DatePickerAttribute<TModel, TProperty>
    {
        private int _minuteStep;

        public DateTimePickerAttribute(Expression<Func<TModel, TProperty>> expression)
            : base(expression)
        {
            base.Format = FormatString.DateTimeFormat;
            base.Class.Remove("input-date").Add("input-datetime");
            _minuteStep = 5;
        }

        /// <summary>
        /// 分钟间隔
        /// </summary>
        public int MinuteStep
        {
            get { return _minuteStep; }
            set
            {
                _minuteStep = value;

                if (_minuteStep < 1)
                {
                    _minuteStep = 1;
                }

                if (_minuteStep > 60)
                {
                    _minuteStep = 60;
                }
            }
        }

        public override IDictionary<string, object> GetHtmlAttribute()
        {
            base.GetHtmlAttribute();
            base.HtmlAttributes.Remove("data-date-week-start");
            base.HtmlAttributes.Remove("data-date-start-date");
            base.HtmlAttributes.Remove("data-date-end-date");
            base.MergeAttributeValue(base.HtmlAttributes, "data-date-weekstart", this.WeekStart.GetHashCode());
            base.MergeAttributeValue(base.HtmlAttributes, "data-minute-step", this.MinuteStep);

            if (this.DefaultDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date", this.DefaultDate.Value.ToDateTimeString());
            }

            if (!this.Format.IsNullOrEmpty())
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-format", this.Format.Replace("m", "i").ToLower());
            }

            if (this.StartDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-startdate", this.StartDate.Value.ToDateString());
            }

            if (this.EndDate.HasValue)
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-enddate", string.Format("{0} 23:59:59", this.EndDate.Value.ToDateString()));
            }

            if (this.DaysOfWeekDisabled.HasItems())
            {
                base.MergeAttributeValue(base.HtmlAttributes, "data-date-days-of-week-disabled", string.Join(",", this.DaysOfWeekDisabled.Select(d => d.GetHashCode())));
            }

            return base.HtmlAttributes;
        }
    }
}