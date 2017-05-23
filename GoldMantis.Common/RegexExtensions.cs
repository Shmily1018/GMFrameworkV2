/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         RegexExtensions.cs
** Creator:          Joe
** CreateDate:       2015-04-01
** Changer:
** LastChangeDate:
** Description:      Regex扩展
** VersionInfo:
*********************************************************/

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GoldMantis.Common
{
    public static class RegexExtensions
    {
        #region 1.0 IsMatch 指定的正则表达式在指定的输入字符串中是否找到了匹配项
        /// <summary>
        /// 指示所指定的正则表达式在指定的输入字符串中是否找到了匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsMatch(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// 指示所指定的正则表达式是否使用指定的匹配选项在指定的输入字符串中找到了匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">枚举值的一个按位组合，这些枚举值提供匹配选项</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsMatch(this string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        /// <summary>
        /// ***如果需要使用pattern多次匹配检查,则不适合使用这个方法,存在性能问题***
        /// 指示 Regex 构造函数中指定的正则表达式在指定的输入字符串中，从该字符串中的指定起始位置开始是否找到了匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="startat">开始搜索的字符位置</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsMatch(this string input, string pattern, int startat)
        {
            return new Regex(pattern).IsMatch(input, startat);
        }

        #endregion

        #region 2.0 Match 在指定的输入字符串中搜索指定的正则表达式的第一个匹配项

        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的第一个匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>一个包含有关匹配的信息的对象</returns>
        public static Match Match(this string input, string pattern)
        {
            return Regex.Match(input, pattern);
        }

        /// <summary>
        /// 使用指定的匹配选项在输入字符串中搜索指定的正则表达式的第一个匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">枚举值的一个按位组合，这些枚举值提供匹配选项</param>
        /// <returns>一个包含有关匹配的信息的对象</returns>
        public static Match Match(this string input, string pattern, RegexOptions options)
        {
            return Regex.Match(input, pattern, options);
        }

        /// <summary>
        /// ***如果需要使用pattern多次匹配字符串,则不适合使用这个方法,存在性能问题***
        /// 从输入字符串中的指定起始位置开始，在该字符串中搜索正则表达式的第一个匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="startat">开始搜索的字符位置</param>
        /// <returns>一个包含有关匹配的信息的对象</returns>
        public static Match Match(this string input, string pattern, int startat)
        {
            return new Regex(pattern).Match(input, startat);
        }

        /// <summary>
        /// ***如果需要使用pattern多次匹配字符串,则不适合使用这个方法,存在性能问题***
        /// 从输入字符串中的指定起始位置开始，在该字符串中搜索正则表达式的第一个匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="beginning">开始搜索的字符位置</param>
        /// <param name="length">子字符串中包含在搜索中的字符数</param>
        /// <returns>一个包含有关匹配的信息的对象</returns>
        public static Match Match(this string input, string pattern, int beginning, int length)
        {
            return new Regex(pattern).Match(input, beginning, length);
        }

        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的第一个匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>匹配的字符串</returns>
        public static string MatchValue(this string input, string pattern)
        {
            Match match = input.Match(pattern);

            return match.Success ? match.Value : string.Empty;
        }

        /// <summary>
        /// 使用指定的匹配选项在输入字符串中搜索指定的正则表达式的第一个匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">枚举值的一个按位组合，这些枚举值提供匹配选项</param>
        /// <returns>匹配的字符串</returns>
        public static string MatchValue(this string input, string pattern, RegexOptions options)
        {
            Match match = input.Match(pattern, options);

            return match.Success ? match.Value : string.Empty;
        }

        /// <summary>
        /// ***如果需要使用pattern多次匹配字符串,则不适合使用这个方法,存在性能问题***
        /// 从输入字符串中的指定起始位置开始，在该字符串中搜索正则表达式的第一个匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="startat">开始搜索的字符位置</param>
        /// <returns>匹配的字符串</returns>
        public static string MatchValue(this string input, string pattern, int startat)
        {
            Match match = input.Match(pattern, startat);

            return match.Success ? match.Value : string.Empty;
        }

        /// <summary>
        /// ***如果需要使用pattern多次匹配字符串,则不适合使用这个方法,存在性能问题***
        /// 从输入字符串中的指定起始位置开始，在该字符串中搜索正则表达式的第一个匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="beginning">开始搜索的字符位置</param>
        /// <param name="length">子字符串中包含在搜索中的字符数</param>
        /// <returns>匹配的字符串</returns>
        public static string MatchValue(this string input, string pattern, int beginning, int length)
        {
            Match match = input.Match(pattern, beginning, length);

            return match.Success ? match.Value : string.Empty;
        }

        #endregion

        #region 3.0 Matches 在指定的输入字符串中搜索指定的正则表达式的所有匹配项

        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的所有匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>搜索操作找到的 Match 对象的集合.如果未找到匹配项，则此方法将返回一个空集合对象</returns>
        public static MatchCollection Matches(this string input, string pattern)
        {
            return Regex.Matches(input, pattern);
        }

        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的所有匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">枚举值的一个按位组合，这些枚举值提供匹配选项</param>
        /// <returns>搜索操作找到的 Match 对象的集合.如果未找到匹配项，则此方法将返回一个空集合对象</returns>
        public static MatchCollection Matches(this string input, string pattern, RegexOptions options)
        {
            return Regex.Matches(input, pattern, options);
        }

        /// <summary>
        /// ***如果需要使用pattern多次匹配字符串,则不适合使用这个方法,存在性能问题***
        /// 从字符串中的指定起始位置开始，在指定的输入字符串中搜索正则表达式的所有匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="startat">在输入字符串中开始搜索的字符位置</param>
        /// <returns>搜索操作找到的 Match 对象的集合。 如果未找到匹配项，则此方法将返回一个空集合对象</returns>
        public static MatchCollection Matches(this string input, string pattern, int startat)
        {
            return new Regex(pattern).Matches(input, startat);
        }

        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的所有匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>返回 枚举字符串</returns>
        public static IEnumerable<string> MatchesValue(this string input, string pattern)
        {
            foreach (Match match in input.Matches(pattern))
            {
                yield return match.Value;
            }
        }

        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的所有匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">枚举值的一个按位组合，这些枚举值提供匹配选项</param>
        /// <returns>返回 枚举字符串</returns>
        public static IEnumerable<string> MatchesValue(this string input, string pattern, RegexOptions options)
        {
            foreach (Match match in input.Matches(pattern, options))
            {
                yield return match.Value;
            }
        }

        /// <summary>
        /// ***如果需要使用pattern多次匹配字符串,则不适合使用这个方法,存在性能问题***
        /// 从字符串中的指定起始位置开始，在指定的输入字符串中搜索正则表达式的所有匹配项
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="startat">在输入字符串中开始搜索的字符位置</param>
        /// <returns>返回 枚举字符串</returns>
        public static IEnumerable<string> MatchesValue(this string input, string pattern, int startat)
        {
            foreach (Match match in input.Matches(pattern, startat))
            {
                yield return match.Value;
            }
        }

        #endregion

        #region 4.0 Replace 在指定的输入字符串内，使用指定的替换字符串替换与指定正则表达式匹配的所有字符串

        /// <summary>
        /// 在指定的输入字符串内，使用指定的替换字符串替换与指定正则表达式匹配的所有字符串
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <returns>一个与输入字符串基本相同的新字符串，唯一的差别在于，其中的每个匹配字符串已被替换字符串代替</returns>
        public static string ReplaceRegex(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }

        /// <summary>
        /// 在指定的输入字符串内，使用指定的替换字符串替换与指定正则表达式匹配的所有字符串
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="options">枚举值的一个按位组合，这些枚举值提供匹配选项</param>
        /// <returns>一个与输入字符串基本相同的新字符串，唯一的差别在于，其中的每个匹配字符串已被替换字符串代替</returns>
        public static string ReplaceRegex(this string input, string pattern, string replacement, RegexOptions options)
        {
            return Regex.Replace(input, pattern, replacement, options);
        }

        /// <summary>
        /// ***如果需要使用pattern多次替换字符串,则不适合使用这个方法,存在性能问题***
        /// 在指定输入字符串内，使用指定替换字符串替换与某个正则表达式模式匹配的字符串（其数目为指定的最大数目）。
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="count">可进行替换的最大次数</param>
        /// <returns>一个与输入字符串基本相同的新字符串，唯一的差别在于，其中的每个匹配字符串已被替换字符串代替</returns>
        public static string ReplaceRegex(this string input, string pattern, string replacement, int count)
        {
            return new Regex(pattern).Replace(input, replacement, count);
        }

        /// <summary>
        /// ***如果需要使用pattern多次替换字符串,则不适合使用这个方法,存在性能问题***
        /// 在指定输入子字符串内，使用指定替换字符串替换与某个正则表达式模式匹配的字符串（其数目为指定的最大数目）。
        /// </summary>
        /// <param name="input">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="count">可进行替换的最大次数</param>
        /// <param name="startat">输入字符串中开始执行搜索的字符位置</param>
        /// <returns>一个与输入字符串基本相同的新字符串，唯一的差别在于，其中的每个匹配字符串已被替换字符串代替</returns>
        public static string ReplaceRegex(this string input, string pattern, string replacement, int count, int startat)
        {
            return new Regex(pattern).Replace(input, replacement, count, startat);
        }


        #endregion

        #region 5.0 Split 在由正则表达式模式项定义的位置将输入字符串拆分为一个子字符串数组

        /// <summary>
        /// 在由正则表达式模式项定义的位置将输入字符串拆分为一个子字符串数组
        /// </summary>
        /// <param name="input">要拆分的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitRegex(this string input, string pattern)
        {
            return Regex.Split(input, pattern);
        }

        /// <summary>
        /// 在由正则表达式模式项定义的位置将输入字符串拆分为一个子字符串数组
        /// </summary>
        /// <param name="input">要拆分的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">枚举值的一个按位组合，这些枚举值提供匹配选项</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitRegex(this string input, string pattern, RegexOptions options)
        {
            return Regex.Split(input, pattern, options);
        }

        /// <summary>
        /// ***如果需要使用pattern多次分割字符串,则不适合使用这个方法,存在性能问题***
        /// 在由 Regex 构造函数中指定的正则表达式定义的位置，将输入字符串拆分为子字符串数组指定的最大次数
        /// </summary>
        /// <param name="input">要拆分的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="count">可拆分的最大次数</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitRegex(this string input, string pattern, int count)
        {
            return new Regex(pattern).Split(input, count);
        }

        /// <summary>
        /// ***如果需要使用pattern多次分割字符串,则不适合使用这个方法,存在性能问题***
        /// 在由 Regex 构造函数中指定的正则表达式定义的位置，将输入字符串拆分为子字符串数组指定的最大次数。 从输入字符串的指定字符位置开始搜索正则表达式模式
        /// </summary>
        /// <param name="input">要拆分的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="count">可拆分的最大次数</param>
        /// <param name="startat">输入字符串中开始搜索的字符位置</param>
        /// <returns>字符串数组</returns>
        public static string[] SplitRegex(this string input, string pattern, int count, int startat)
        {
            return new Regex(pattern).Split(input, count, startat);
        }

        #endregion
    }
}
