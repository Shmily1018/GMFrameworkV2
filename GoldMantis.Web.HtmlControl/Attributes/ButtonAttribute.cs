using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public class ButtonAttribute : HtmlAttribute
    {
        public ButtonAttribute(string text):this(text,ButtonType.Button)
        {
        }

        public ButtonAttribute(string text, ButtonType btnType)
        {
            this.Text = text;
            this.ButtonType = btnType;
            this.Class.Add(Bootstrap.Button.Btn);
        }

        public string Text { get; set; }

        public ButtonType ButtonType { get; set; }
    }

    public enum ButtonType
    {
        Button,
        Submit,
        Reset
    }
}