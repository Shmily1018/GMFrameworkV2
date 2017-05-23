/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         HtmlParser.cs
** Creator:          Joe
** CreateDate:       2015-10-12
** Changer:
** LastChangeDate:
** Description:      解析HTML
** VersionInfo:
*********************************************************/
using System.Text.RegularExpressions;
using NHibernate.Mapping;

namespace GoldMantis.Common
{
    public static class HtmlParser
    {
        /// <summary>
        /// 解析HTML中某个属性的值
        /// </summary>
        /// <param name="html">源HTML</param>
        /// <param name="key">属性名</param>
        /// <returns>属性值</returns>
        public static string ParseAttributeValue(string html, string key)
        {
            string group = "group";
            string patten = string.Format(" {0}=[\"|\'](?<{1}>.*?)[\"|\'][ |/|>]", key, group);

            if (html.IsMatch(patten))
            {
                return html.Match(patten).Groups[group].Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// 替换HTML中某个属性的值
        /// </summary>
        /// <param name="html">源HTML</param>
        /// <param name="key">属性名</param>
        /// <param name="newValue">替换值</param>
        /// <returns>替换后的HTML</returns>
        public static string ReplaceAttributeValue(string html, string key, string newValue)
        {
            string group1 = "group1", group2 = "group2", group3 = "group3";
            string patten = string.Format("(?<{0}>.* {1}=[\"|\']?)(?<{2}>.*?)(?<{3}>[\"|\'].+)", group1, key, group2, group3);

            if (html.IsMatch(patten))
            {
                Match mc = html.Match(patten);
                html = string.Format("{0}{1}{2}", mc.Groups[group1].Value, newValue, mc.Groups[group3].Value);
            }


            return html;
        }
    }
}