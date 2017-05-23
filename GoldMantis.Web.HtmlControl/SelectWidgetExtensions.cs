using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Classes;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public static class SelectWidgetExtensions
    {
        public static MvcHtmlString SelectWidget(this HtmlHelper htmlHelper, SelectWidgetAttribute attribute)
        {
            attribute.PrepareWidgetHtmlAttribute();
            string textHtml = htmlHelper.TextBox(attribute.TextInput).ToString();
            string valueHtml = htmlHelper.Hidden(attribute.ValueInput).ToString();

            
            return SelectWidgetHelper(textHtml, valueHtml, attribute);
        }

        public static MvcHtmlString SelectWidgetFor<TModel, TPText,TPValue>(this HtmlHelper<TModel> htmlHelper,SelectWidgetAttribute<TModel, TPText,TPValue> attribute)
        {
            string name, value, text;

            name = ExpressionHelper.GetExpressionText(attribute.ValueInput.Expression);
            name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            string id = TagBuilder.CreateSanitizedId(name, HtmlHelper.IdAttributeDotReplacement);

            ModelMetadata valueMetadata = ModelMetadata.FromLambdaExpression(attribute.ValueInput.Expression, htmlHelper.ViewData);
            value = htmlHelper.FormatValue(valueMetadata.Model, null);

            ModelMetadata textMetadata = ModelMetadata.FromLambdaExpression(attribute.TextInput.Expression, htmlHelper.ViewData);
            text = htmlHelper.FormatValue(textMetadata.Model, null);

            attribute.PrepareWidgetHtmlAttribute(id, text, value);
            string textHtml = htmlHelper.TextBoxFor(attribute.TextInput).ToString();
            textHtml = HtmlParser.ReplaceAttributeValue(textHtml, "id", string.Format("{0}_Text", id));
            string valueHtml = htmlHelper.HiddenFor(attribute.ValueInput).ToString();


            return SelectWidgetHelper(textHtml, valueHtml, attribute);
        }

        private static MvcHtmlString SelectWidgetHelper(string textHtml, string valueHtml, SelectWidgetPartAttribute attribute)
        {
            TagBuilder widthBuilder = new TagBuilder("div");
            widthBuilder.AddCssClass(attribute.WidthClass);
            widthBuilder.MergeAttribute("id", string.Format("{0}_Wrap", attribute.Name), true);

            if (!attribute.Visible)
            {
                widthBuilder.AddCssClass(Bootstrap.Typesetting.Hidden);
            }


            TagBuilder containerBuilder = new TagBuilder("div");
            containerBuilder.MergeAttributes(attribute.GetHtmlAttribute());

            TagBuilder clearBuilder = new TagBuilder("span");
            clearBuilder.MergeAttributes(attribute.ClearSpan.GetHtmlAttribute());
            TagBuilder remove = new TagBuilder("i");
            remove.AddCssClass(Bootstrap.Glyphicon.Glyphicon);
            remove.AddCssClass(Bootstrap.Glyphicon.GlyphiconRemove);
            clearBuilder.InnerHtml = remove.ToString(TagRenderMode.Normal);

            TagBuilder selectBuilder = new TagBuilder("span");
            selectBuilder.MergeAttributes(attribute.SelectSpan.GetHtmlAttribute());
            TagBuilder search = new TagBuilder("i");
            search.AddCssClass(Bootstrap.Glyphicon.Glyphicon);
            search.AddCssClass(Bootstrap.Glyphicon.GlyphiconSearch);
            selectBuilder.InnerHtml = search.ToString(TagRenderMode.Normal);

            containerBuilder.InnerHtml = string.Format("{0}{1}{2}{3}",
                textHtml,
                clearBuilder.ToString(TagRenderMode.Normal),
                selectBuilder.ToString(TagRenderMode.Normal),
                valueHtml);

            widthBuilder.InnerHtml = containerBuilder.ToString(TagRenderMode.Normal);

            return widthBuilder.ToMvcHtmlString(TagRenderMode.Normal);
        }

    }

    
}