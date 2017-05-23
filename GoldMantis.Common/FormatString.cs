/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         FormatString.cs
** Creator:          Joe
** CreateDate:       2015-03-31
** Changer:
** LastChangeDate:
** Description:      格式字符串
** VersionInfo:
*********************************************************/

namespace GoldMantis.Common
{
    public sealed class FormatString
    {
        //格式化日期
        public const string DBDateTimeFormat = "yyyy-mm-dd hh24:mi:ss";
        public const string DateFormat = "yyyy-MM-dd";
        public const string DateFormatPoint = "yy.MM.dd";
        public const string DateUpperFormat = "yyyy年MM月dd日";
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public const string DateHMFormat = "yyyy-MM-dd HH:mm";
        public const string DayTimeFormat = "MM-dd HH:mm";
        public const string TimeFormat = "HH:mm:ss";

        //格式化数字,带有千分符
        public const string GroupDecimal2Format = "#,#0.00";
        public const string GroupDecimal4Format = "#,#0.0000";
        public const string PercentGroupDecimal2Format = "#,#0.##%";
        public const string GroupIntFormat = "#,#0";
        public const string GroupDecimal1Format = "#,#0.0";

        //格式化数字,不带千分符
        public const string Decimal2Format = "#0.00";
        public const string Decimal4Format = "#0.0000";
        public const string PercentDecimal2Format = "#0.##%";
        public const string IntFormat = "#0";
        public const string Decimal1Format = "#0.0";

        /// <summary>
        /// 获取带有占位符的格式化字符串
        /// </summary>
        /// <param name="placeholder">占位位置</param>
        /// <param name="format">格式化字符串</param>
        /// <returns>带有占位符的格式化字符串</returns>
        public static string GetPlaceholderFormat(int placeholder, string format)
        {
            return string.Format("{{{0}:{1}}}", placeholder, format);
        }
    }
}
