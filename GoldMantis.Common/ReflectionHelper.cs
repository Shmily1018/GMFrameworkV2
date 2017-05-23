using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace GoldMantis.Common
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string FullHtmlFieldName<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string FullHtmlFieldId<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));

            if (!fullName.IsNullOrEmpty())
            {
                fullName = fullName.Replace('.', '_');
            }

            return fullName;
        }

        /// <summary>
        /// 排序列，根据类名和字段名返回：例: x=>x.Name
        /// </summary>
        /// <typeparam name="T">类名</typeparam>
        /// <param name="propertyName">字段名</param>
        /// <returns>x=>x.Name</returns>
        public static Expression<Func<T, object>> GetClassPropertyExpression<T>(string propertyName)
        {
            if (propertyName.IsNullOrEmpty())
            {
                return null;
            }


            ParameterExpression paramter = Expression.Parameter(typeof (T));
            MemberExpression memberExpression = Expression.Property(paramter, propertyName);
            UnaryExpression unaryExpression = Expression.Convert(memberExpression, typeof (object));
            Expression<Func<T, object>> tempExpression = Expression.Lambda<Func<T, object>>(unaryExpression, paramter);

            return tempExpression;
        }

        /// <summary>
        /// 根据表达式返回 属性名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public static string GetPropertyNameFromExpression<T>(Expression<Func<T, object>> propertyExpression)
        {
            MemberExpression memberExpr = null;

            if (propertyExpression.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr = propertyExpression.Body.As<UnaryExpression>().Operand as MemberExpression;
            }
            else if (propertyExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = propertyExpression.Body as MemberExpression;
            }


            return memberExpr.Member.Name;
        }

        /// <summary>
        /// 根据数据源返回其中两个字段的Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sources"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ComposeDictionaryFromCollection<T>(IList<T> sources, Expression<Func<T, object>> key, Expression<Func<T, object>> value)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            if (!sources.HasItems())
            {
                return dictionary;
            }


            Type t = typeof (T);
            PropertyInfo keyProperty = t.GetProperty(GetPropertyNameFromExpression(key));
            PropertyInfo valueProperty = t.GetProperty(GetPropertyNameFromExpression(value));

            foreach (var item in sources)
            {
                object keyObj = keyProperty.GetValue(item, null);
                object valueObj = valueProperty.GetValue(item, null);

                if (keyObj == null || dictionary.ContainsKey(keyObj.ToString()))
                {
                    continue;
                }

                dictionary.Add(keyObj.ToString(), valueObj);
            }


            return dictionary;
        }

        /// <summary>
        /// 根据枚举返回text和Description
        /// key : text
        /// description : description
        /// </summary>
        /// <typeparam name="T">需要提取key,Discription的枚举</typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> GetEnumDescriptionText<T>()
        {
            Dictionary<string, string> dictDescriptionText = new Dictionary<string, string>();
            T[] values = (T[]) Enum.GetValues(typeof (T));

            for (int i = 0; i < values.Length; i++)
            {
                string description = AttachDataAttribute.GetAttachedData<T>(values[i].ToString(), AttachDataKey.String) == null
                    ? values[i].ToString()
                    : AttachDataAttribute.GetAttachedData<T>(values[i].ToString(), AttachDataKey.String);

                string text = AttachDataAttribute.GetAttachedData<T>(values[i].ToString(), AttachDataKey.String) != null
                    ? values[i].ToString()
                    : Convert.ToString((int) (Object) values[i]);

                dictDescriptionText.Add(text, description);
            }


            return dictDescriptionText;
        }

        /// <summary>
        /// 根据枚举返回text和Description   dic keys 中必须存在key
        /// key : text
        /// description : description
        /// </summary>
        /// <typeparam name="T">需要提取key,Discription的枚举</typeparam>
        /// <param name="key">必须包含key</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetEnumDescriptionText<T>(string key)
        {
            Dictionary<string, string> dictDescriptionText = new Dictionary<string, string>();
            T[] values = (T[]) Enum.GetValues(typeof (T));

            for (int i = 0; i < values.Length; i++)
            {
                string description = AttachDataAttribute.GetAttachedData<T>(values[i].ToString(), AttachDataKey.String);
                string text = values[i].ToString();

                //必须包含key
                if (!text.Contains(key))
                {
                    continue;
                }

                dictDescriptionText.Add(text, description);
            }


            return dictDescriptionText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> BuildListHelper<T>()
        {
            return new List<T>();
        }

        /// <summary>
        /// 返回一个列表，根据T 两个属性，决定text、value，找出text 中包含keyword 对应的value值，然后返回IList<U>列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="keyword"></param>
        /// <param name="sources"></param>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IList<U> ListValueByKeyword<T, U>(string keyword, IList<T> sources, Expression<Func<T, object>> text, Expression<Func<T, object>> value)
        {
            IList<U> result = new List<U>();

            if (!sources.HasItems())
            {
                return result;
            }


            Type t = typeof (T);
            PropertyInfo keyProperty = t.GetProperty(GetPropertyNameFromExpression(text));
            PropertyInfo valueProperty = t.GetProperty(GetPropertyNameFromExpression(value));

            foreach (var item in sources)
            {
                object textObj = keyProperty.GetValue(item, null);
                object valueObj = valueProperty.GetValue(item, null);

                if (textObj == null || !textObj.ToString().Contains(keyword) || result.Contains((U) valueObj))
                {
                    continue;
                }

                result.Add((U) valueObj);
            }


            return result;
        }
    }
}