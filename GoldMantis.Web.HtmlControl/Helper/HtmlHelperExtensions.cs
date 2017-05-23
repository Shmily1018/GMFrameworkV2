using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Classes;

namespace GoldMantis.Web.HtmlControl
{
    public static class HtmlHelperExtensions
    {
        public static object GetModelStateValue(this HtmlHelper htmlHelper, string key, Type destinationType)
        {
            ModelState state;

            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out state) && (state.Value != null))
            {
                return state.Value.ConvertTo(destinationType, null);
            }


            return null;
        }

        public static bool EvalBoolean(this HtmlHelper htmlHelper, string key)
        {
            return Convert.ToBoolean(htmlHelper.ViewData.Eval(key), CultureInfo.InvariantCulture);
        }

        public static string EvalString(this HtmlHelper htmlHelper, string key)
        {
            return Convert.ToString(htmlHelper.ViewData.Eval(key), CultureInfo.CurrentCulture);
        }

        public static string EvalString(this HtmlHelper htmlHelper, string key, string format)
        {
            return Convert.ToString(htmlHelper.ViewData.Eval(key, format), CultureInfo.CurrentCulture);
        }

        public static MvcHtmlString WrapHtmlControl(this HtmlHelper htmlHelper, Func<HtmlHelper,MvcHtmlString> funInner , HtmlAttribute attribute)
        {
            if (attribute.IsNull())
            {
                return MvcHtmlString.Empty;
            }


            TagBuilder divBuilder = null;

            if (attribute.Class.IsNotNull())
            {
                IEnumerable<string> gridClasses = attribute.Class.GetGridClasses();

                // 生成外包装div
                if (gridClasses.HasItems())
                {
                    divBuilder = new TagBuilder("div");
                    divBuilder.AddCssClass(string.Join(ControlClass.ClassSeprator, gridClasses));

                    attribute.Class.Remove(gridClasses);
                }
            }


            MvcHtmlString innerHtml = funInner(htmlHelper);

            if (divBuilder.IsNull())
            {
                return innerHtml;
            }

            divBuilder.InnerHtml = innerHtml.ToString();

            return divBuilder.ToMvcHtmlString(TagRenderMode.Normal);
        }

        public static MvcHtmlString WrapHtmlControl<TModel>(this HtmlHelper<TModel> htmlHelper, Func<HtmlHelper<TModel>, MvcHtmlString> funInner, HtmlAttribute attribute)
        {
            if (attribute.IsNull())
            {
                return MvcHtmlString.Empty;
            }


            TagBuilder divBuilder = null;

            if (attribute.Class.IsNotNull())
            {
                IEnumerable<string> gridClasses = attribute.Class.GetGridClasses();

                // 生成外包装div
                if (gridClasses.HasItems())
                {
                    divBuilder = new TagBuilder("div");
                    divBuilder.AddCssClass(string.Join(ControlClass.ClassSeprator, gridClasses));

                    attribute.Class.Remove(gridClasses);
                }
            }


            MvcHtmlString innerHtml = funInner(htmlHelper);

            if (divBuilder.IsNull())
            {
                return innerHtml;
            }

            divBuilder.InnerHtml = innerHtml.ToString();

            return divBuilder.ToMvcHtmlString(TagRenderMode.Normal);
        }

        private static MvcHtmlString WrapHtmlControl(MvcHtmlString innerHtml, HtmlAttribute attribute)
        {
            if (attribute.IsNull())
            {
                return MvcHtmlString.Empty;
            }


            TagBuilder divBuilder = null;

            if (attribute.Class.IsNotNull())
            {
                IEnumerable<string> gridClasses = attribute.Class.GetGridClasses();

                // 生成外包装div
                if (gridClasses.HasItems())
                {
                    divBuilder = new TagBuilder("div");
                    divBuilder.AddCssClass(string.Join(ControlClass.ClassSeprator, gridClasses));

                    attribute.Class.Remove(gridClasses);
                }
            }


            if (divBuilder.IsNull())
            {
                return innerHtml;
            }

            divBuilder.InnerHtml = innerHtml.ToString();

            return divBuilder.ToMvcHtmlString(TagRenderMode.Normal);
        }
    }
}