using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class DateRangeAttribute : DatePickerAttribute
    {
        public DateRangeAttribute(string name, string startName, string endName)
            : base(name)
        {
            base.Class.Add("input-daterange");

            //this.DateRangeStart = new TextBoxAttribute(startName);
            //this.DateRangeStart.HtmlAttributes.Add("readonly", "readonly");
            base.DateBox.Name = startName;

            this.DateBoxEnd = new TextBoxAttribute(endName);
            this.DateBoxEnd.HtmlAttributes.Add("readonly", "readonly");
        }

        //public TextBoxAttribute DateRangeStart { get; private set; }

        public TextBoxAttribute DateBoxEnd { get; private set; }
    }
}