using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI.WebControls;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Classes;


namespace GoldMantis.Web.HtmlControl
{
    public static class InputExtensions
    {
        public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, CheckAttribute attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.CheckBox(attribute.Name, attribute.IsChecked, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString CheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, CheckAttribute<TModel> attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.CheckBoxFor(attribute.Expression, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, OptionListAttribute attribute)
        {
            return OptionButtonHelper(htmlHelper, attribute.Name, null, attribute.OptionBoxList, InputType.CheckBox,
                attribute.Enable, attribute.Visible, attribute.TextAlign, attribute.GetItemHtmlAttribute(),
                attribute.GetHtmlAttribute());
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, OptionListAttribute<TModel, ICollection<TProperty>> attribute)
        {
            string name = ExpressionHelper.GetExpressionText(attribute.Expression);
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(attribute.Expression, htmlHelper.ViewData);
            ICollection<string> values = null;

            if (metadata.IsNotNull() && metadata.Model.IsNotNull())
            {
                values = metadata.Model.As<ICollection<TProperty>>().Select(m => m.ToString()).ToList();
            }

            return OptionButtonHelper(htmlHelper, fullHtmlFieldName, values, attribute.OptionBoxList, InputType.CheckBox,
                attribute.Enable, attribute.Visible, attribute.TextAlign, attribute.GetItemHtmlAttribute(),
                attribute.GetHtmlAttribute());
        }

        public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, RadioAttribute attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.RadioButton(attribute.Name, attribute.Value, attribute.IsChecked, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString RadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, RadioAttribute<TModel, TProperty> attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.RadioButtonFor(attribute.Expression, attribute.Value, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, OptionListAttribute attribute)
        {
            return OptionButtonHelper(htmlHelper, attribute.Name, null, attribute.OptionBoxList, InputType.Radio,
                attribute.Enable, attribute.Visible, attribute.TextAlign, attribute.GetItemHtmlAttribute(),
                attribute.GetHtmlAttribute());
        }

        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, OptionListAttribute<TModel, TProperty> attribute)
        {
            string nameAttribute = ExpressionHelper.GetExpressionText(attribute.Expression);
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(attribute.Expression, htmlHelper.ViewData);
            ICollection<string> values = null;

            if (metadata.IsNotNull() && metadata.Model.IsNotNull())
            {
                values = new string[] { metadata.Model.ToString() };
            }

            return OptionButtonHelper(htmlHelper, nameAttribute, values, attribute.OptionBoxList, InputType.Radio,
                attribute.Enable, attribute.Visible, attribute.TextAlign, attribute.GetItemHtmlAttribute(),
                attribute.GetHtmlAttribute());
        }

        public static MvcHtmlString Hidden(this HtmlHelper htmlHelper, TextBoxAttribute attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.Hidden(attribute.Name, attribute.Value, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString HiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, TextBoxAttribute<TModel, TProperty> attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.HiddenFor(attribute.Expression, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString Password(this HtmlHelper htmlHelper, TextBoxAttribute attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.Password(attribute.Name, attribute.Value, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString PasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, TextBoxAttribute<TModel, TProperty> attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.PasswordFor(attribute.Expression, attribute.GetHtmlAttribute()), attribute);
        }


        public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, TextBoxAttribute attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.TextBox(attribute.Name, attribute.Value, attribute.Format, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, TextBoxAttribute<TModel, TProperty> attribute)
        {
            return htmlHelper.WrapHtmlControl(html => html.TextBoxFor(attribute.Expression, attribute.Format, attribute.GetHtmlAttribute()), attribute);
        }

        public static MvcHtmlString TextBoxFor<TModel, TProp>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProp>> expression, params Action<MvcInputBuilder>[] propertySetters)
        {
            MvcInputBuilder builder = new MvcInputBuilder();

            foreach (var propertySetter in propertySetters)
            {
                propertySetter.Invoke(builder);
            }

            var properties = new RouteValueDictionary(builder)
                .Select(kvp => kvp)
                .Where(kvp => kvp.Value != null)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return htmlHelper.TextBoxFor(expression, properties);
        }

        internal static MvcHtmlString OptionButtonHelper(HtmlHelper htmlHelper, string name, ICollection<string> values,
            List<SelectListItem> optionBoxList, InputType inputType, bool enable, bool visible, TextAlign textAlign,
            IDictionary<string, object> itemHtmlAttribute, IDictionary<string, object> htmlAttribute)
        {
            StringBuilder innerHtmlBuilder = new StringBuilder();

            TagBuilder divBuilder = new TagBuilder("div");
            divBuilder.MergeAttributes(htmlAttribute);
            divBuilder.MergeAttribute("id", TagBuilder.CreateSanitizedId(name, HtmlHelper.IdAttributeDotReplacement), false);

            foreach (SelectListItem item in optionBoxList)
            {
                TagBuilder checkBuilder = new TagBuilder("input");
                checkBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(inputType));
                checkBuilder.MergeAttribute("name", name, true);
                checkBuilder.MergeAttribute("value", item.Value, true);

                bool boxChecked = values == null ? item.Selected : values.Contains(item.Value);

                if (boxChecked)
                {
                    checkBuilder.MergeAttribute("checked", "checked", true);
                }

                if (!enable)
                {
                    checkBuilder.MergeAttribute("disabled", "disabled", true);
                }


                TagBuilder labelBuilder = new TagBuilder("label");
                labelBuilder.MergeAttributes(itemHtmlAttribute);

                if (textAlign == TextAlign.Right)
                {
                    labelBuilder.InnerHtml = string.Format("{0}&nbsp;&nbsp;{1}", checkBuilder.ToString(TagRenderMode.SelfClosing), item.Text);
                }
                else
                {
                    labelBuilder.InnerHtml = string.Format("{0}&nbsp;&nbsp;{1}", item.Text, checkBuilder.ToString(TagRenderMode.SelfClosing));
                }

                innerHtmlBuilder.Append(labelBuilder.ToString(TagRenderMode.Normal));
            }

            divBuilder.InnerHtml = innerHtmlBuilder.ToString();

            return divBuilder.ToMvcHtmlString(TagRenderMode.Normal);
        }
    }

    public class MvcInputBuilder
    {
        public int ID { get; set; }

        public string Class { get; set; }
    }
}
