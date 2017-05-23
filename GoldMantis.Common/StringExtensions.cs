/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         StringExtensions.cs
** Creator:          Joe
** CreateDate:       2015-03-26
** Changer:
** LastChangeDate:
** Description:      字符串扩展方法. 包括空值判断和类型转换功能
** VersionInfo:
*********************************************************/

using System;
using System.Text;
using System.Web;

namespace GoldMantis.Common
{
    public static class StringExtensions
    {
        #region 1.0 字符串 空值判断

        /// <summary>
        /// 字符串是否为Null或Empty
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>是否为Null或Empty</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 字符串是否为Null或WhiteSpace
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>是否为Null或WhiteSpace</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 字符串是否为Null或Empty或WhiteSpace
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>是否为Null或Empty或WhiteSpace</returns>
        public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        #endregion

        #region 2.0 String To Int32

        /// <summary>
        /// 字符串转换为整型
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>整型值</returns>
        internal static int StringToInt32(this string value)
        {
            return int.Parse(value);
        }

        /// <summary>
        /// 字符串转换为整型.
        /// 如果字符串为空值,返回指定默认值
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>整型值</returns>
        internal static int StringToDefaultInt32(this string value, int defaultValue = 0)
        {
            return value.IsNullOrEmptyOrWhiteSpace() ? defaultValue : value.StringToInt32();
        }

        /// <summary>
        /// 字符串转换为可空整型
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>可空整型值</returns>
        internal static int? StringToNullableInt32(this string value)
        {
            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                return null;
            }


            return value.StringToInt32();
        }

        #endregion

        #region 3.0 String To Decimal

        /// <summary>
        /// 字符串转换为Decimal
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Decimal值</returns>
        internal static decimal StringToDecimal(this string value)
        {
            return decimal.Parse(value);
        }

        /// <summary>
        /// 字符串转换为Decimal
        /// 如果字符串为空值,返回指定默认值
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>Decimal值</returns>
        internal static decimal StringToDefaultDecimal(this string value, decimal defaultValue = 0)
        {
            return value.IsNullOrEmptyOrWhiteSpace() ? defaultValue : value.StringToDecimal();
        }

        /// <summary>
        /// 字符串转换为可空Decimal
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>可空Decimal值</returns>
        internal static decimal? StringToNullableDecimal(this string value)
        {
            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                return null;
            }


            return value.StringToDecimal();
        }

        #endregion

        #region 4.0 String To Double

        /// <summary>
        /// 字符串转换为Double
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Double值</returns>
        internal static double StringToDouble(this string value)
        {
            return double.Parse(value);
        }

        /// <summary>
        /// 字符串转换为Double
        /// 如果字符串为空值,返回指定默认值
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>Double值</returns>
        internal static double StringToDefaultDouble(this string value, double defaultValue = 0)
        {
            return value.IsNullOrEmptyOrWhiteSpace() ? defaultValue : value.StringToDouble();
        }

        /// <summary>
        /// 字符串转换为可空Double
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>可空Double值</returns>
        internal static double? StringToNullableDouble(this string value)
        {
            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                return null;
            }


            return value.StringToDouble();
        }

        #endregion

        #region 5.0 String To Boolean

        /// <summary>
        /// 字符串转换为Boolean
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Boolean值</returns>
        internal static bool StringToBoolean(this string value)
        {
            return bool.Parse(value);
        }

        /// <summary>
        /// 字符串转换为Boolean
        /// 如果字符串为空值,返回指定默认值
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>Boolean值</returns>
        internal static bool StringToDefaultBoolean(this string value, bool defaultValue = false)
        {
            return value.IsNullOrEmptyOrWhiteSpace() ? defaultValue : value.StringToBoolean();
        }

        /// <summary>
        /// 字符串转换为可空Boolean
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>可空Boolean值</returns>
        internal static bool? StringToNullableBoolean(this string value)
        {
            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                return null;
            }


            return value.StringToBoolean();
        }

        #endregion

        #region 6.0 String To DateTime

        /// <summary>
        /// 字符串转换为DateTime
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>DateTime值</returns>
        internal static DateTime StringToDateTime(this string value)
        {
            return DateTime.Parse(value);
        }

        /// <summary>
        /// 字符串转换为DateTime
        /// 如果字符串为空值,返回指定默认值
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>DateTime值</returns>
        internal static DateTime StringToDefaultDateTime(this string value, DateTime defaultValue)
        {
            return value.IsNullOrEmptyOrWhiteSpace() ? defaultValue : value.StringToDateTime();
        }

        /// <summary>
        /// 字符串转换为可空DateTime
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>可空DateTime值</returns>
        internal static DateTime? StringToNullableDateTime(this string value)
        {
            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                return null;
            }


            return value.StringToDateTime();
        }

        #endregion

        #region 7.0 String To Guid

        /// <summary>
        /// 字符串转换为Guid
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>Guid值</returns>
        internal static Guid StringToGuid(this string value)
        {
            return Guid.Parse(value);
        }

        /// <summary>
        /// 字符串转换为Guid
        /// 如果字符串为空值,返回指定默认值
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>Guid值</returns>
        internal static Guid StringToDefaultGuid(this string value, Guid defaultValue)
        {
            return value.IsNullOrEmptyOrWhiteSpace() ? defaultValue : value.StringToGuid();
        }

        /// <summary>
        /// 字符串转换为可空Guid
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns>可空Guid值</returns>
        internal static Guid? StringToNullableGuid(this string value)
        {
            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                return null;
            }


            return value.StringToGuid();
        }

        #endregion

        #region 8.0 Html&Url 编码解码

        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(this string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(this string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        #endregion

        #region 9.0 清除 空格/换行/回车

        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>清除回车/换行/空格之后的字符串</returns>
        public static string TrimRight(this string str)
        {
            if (!str.IsNullOrEmpty())
            {
                int i = 0;

                while ((i = str.Length) > 0)
                {
                    if (!str[i - 1].Equals(' ') && !str[i - 1].Equals('\r') && !str[i - 1].Equals('\n'))
                    {
                        break;
                    }


                    str = str.Substring(0, i - 1);
                }
            }


            return str;
        }

        /// <summary>
        /// 删除字符串头部的回车/换行/空格
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>清除回车/换行/空格之后的字符串</returns>
        public static string TrimLeft(this string str)
        {
            if (!str.IsNullOrEmpty())
            {
                while (str.Length > 0)
                {
                    if (!str[0].Equals(' ') && !str[0].Equals('\r') && !str[0].Equals('\n'))
                    {
                        break;
                    }


                    str = str.Substring(1);
                }
            }


            return str;
        }

        /// <summary>
        /// 删除字符串头部和尾部的回车/换行/空格
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>清除回车/换行/空格之后的字符串</returns>
        public static string TrimBlank(this string str)
        {
            return str.TrimLeft().TrimRight();
        }

        #endregion

        #region 10.0 汉字转拼音

        /// <summary>
        /// 汉字转拼音
        /// </summary>
        /// <param name="chinese">汉字</param>
        /// <param name="separator">分隔符</param>
        /// <param name="upperHead">首字母是否大写</param>
        /// <returns>拼音</returns>
        public static string ToPinYin(this string chinese, string separator = "", bool upperHead = false)
        {
            return ChineseToPinYin.ToPinYin(chinese, separator, upperHead);
        }

        /// <summary>
        /// 汉字转首字母大写拼音
        /// </summary>
        /// <param name="chinese">需要转换的汉字</param>
        /// <param name="separator">分隔符</param>
        /// <returns>首字母大写拼音</returns>
        public static string ToUpperHeadPinYin(this string chinese, string separator = "")
        {
            return ChineseToPinYin.ToUpperHeadPinYin(chinese, separator);
        }

        /// <summary>
        /// 汉字转大写拼音
        /// </summary>
        /// <param name="chinese">需要转换的汉字</param>
        /// <param name="separator">分隔符</param>
        /// <returns>大写拼音</returns>
        public static string ToUpperPinYin(this string chinese, string separator = "")
        {
            return ChineseToPinYin.ToUpperPinYin(chinese, separator);
        }

        /// <summary>
        /// 取汉字拼音的首字母
        /// </summary>
        /// <param name="chinese">汉字</param>
        /// <param name="lowwer">是否小写</param>
        /// <returns>首字母</returns>
        public static string ToHeadPinYin(this string chinese, bool lowwer = false)
        {
            return ChineseToPinYin.ToHeadPinYin(chinese, lowwer);
        }

        #endregion

        #region 其他

        /// <summary>
        /// 换为首字母大写的字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>首字母大写的字符串</returns>
        public static string ToUpperHead(this string str)
        {
            if (str.IsNullOrEmptyOrWhiteSpace() || (str[0] >= 'A' && str[0] <= 'Z'))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToUpper();
            }

            return string.Format("{0}{1}", str[0].ToString().ToUpper(), str.Substring(1).ToLower());
        }

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>真实长度</returns>
        public static int ByteLength(this string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        /// <summary>
        /// 截取特定长度的字符串, 1个汉字长度为2
        /// </summary>
        /// <param name="str">被截取字符串</param>
        /// <param name="length">截取长度</param>
        /// <returns>截取后字符串</returns>
        public static string ClipString(this string str, int length)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }

            char c;
            int recordLen = 0;
            StringBuilder result=new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                c = str[i];
                result.Append(c);
                recordLen += ValidateHelper.IsHanZiAscii(c) ? 2 : 1;

                if (recordLen >= length)
                {
                    break;
                }
            }


            if (result.Length < str.Length)
            {
                result.Append("...");
            }


            return result.ToString();
        }

        #endregion
    }
}
