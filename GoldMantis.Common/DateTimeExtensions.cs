/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         DateTimeExtensions.cs
** Creator:          Joe
** CreateDate:       2015-03-31
** Changer:
** LastChangeDate:
** Description:      DateTime辅助操作
** VersionInfo:
*********************************************************/

using System;

namespace GoldMantis.Common
{
    public static class DateTimeExtensions
    {
        #region 1.0 DateTime格式化为特定字符串

        /// <summary>
        /// 转换为 "yyyy-mm-dd hh24:mi:ss" 格式的字符串
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns>目标字符串</returns>
        public static string ToDbDateTimeString(this DateTime datetime)
        {
            return datetime.ToString(FormatString.DBDateTimeFormat);
        }

        /// <summary>
        /// 转换为 "yyyy-MM-dd" 格式的字符串
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns>目标字符串</returns>
        public static string ToDateString(this DateTime datetime)
        {
            return datetime.ToString(FormatString.DateFormat);
        }

        /// <summary>
        /// 转换为 "yy.MM.dd" 格式的字符串
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns>目标字符串</returns>
        public static string ToPointDateString(this DateTime datetime)
        {
            return datetime.ToString(FormatString.DateFormatPoint);
        }

        /// <summary>
        /// 转换为 "yyyy年MM月dd日" 格式的字符串
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns>目标字符串</returns>
        public static string ToDateUpperString(this DateTime datetime)
        {
            return datetime.ToString(FormatString.DateUpperFormat);
        }

        /// <summary>
        /// 转换为 "yyyy-MM-dd HH:mm:ss" 格式的字符串
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns>目标字符串</returns>
        public static string ToDateTimeString(this DateTime datetime)
        {
            return datetime.ToString(FormatString.DateTimeFormat);
        }

        /// <summary>
        /// 转换为 "yyyy-MM-dd HH:mm" 格式的字符串
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns>目标字符串</returns>
        public static string ToDateHMString(this DateTime datetime)
        {
            return datetime.ToString(FormatString.DateHMFormat);
        }

        /// <summary>
        /// 转换为 "MM-dd HH:mm" 格式的字符串
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns>目标字符串</returns>
        public static string ToDayTimeString(this DateTime datetime)
        {
            return datetime.ToString(FormatString.DayTimeFormat);
        }

        /// <summary>
        /// 转换为 "HH:mm:ss" 格式的字符串
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns>目标字符串</returns>
        public static string ToTimeString(this DateTime datetime)
        {
            return datetime.ToString(FormatString.TimeFormat);
        }

        #endregion

        #region 2.0 计算时间间隔

        /// <summary>
        /// 获取两个时间的间隔
        /// </summary>
        /// <param name="time1">基准时间</param>
        /// <param name="time2">比较的时间</param>
        /// <returns>时间戳TimeSpan</returns>
        public static TimeSpan TimeSpanOfTimeInterval(this DateTime time1, DateTime time2)
        {
            return time1.Subtract(time2);
        }

        /// <summary>
        /// 获取两个时间的间隔
        /// </summary>
        /// <param name="time1">基准时间</param>
        /// <param name="time2">比较的时间</param>
        /// <returns>间隔天数</returns>
        public static double TotalDaysOfTimeInterval(this DateTime time1, DateTime time2)
        {
            return time1.TimeSpanOfTimeInterval(time2).TotalDays;
        }

        /// <summary>
        /// 获取两个时间的间隔
        /// </summary>
        /// <param name="time1">基准时间</param>
        /// <param name="time2">比较的时间</param>
        /// <returns>间隔小时数</returns>
        public static double TotalHoursOfTimeInterval(this DateTime time1, DateTime time2)
        {
            return time1.TimeSpanOfTimeInterval(time2).TotalHours;
        }

        /// <summary>
        /// 获取两个时间的间隔
        /// </summary>
        /// <param name="time1">基准时间</param>
        /// <param name="time2">比较的时间</param>
        /// <returns>间隔毫秒数</returns>
        public static double TotalMillisecondsOfTimeInterval(this DateTime time1, DateTime time2)
        {
            return time1.TimeSpanOfTimeInterval(time2).TotalMilliseconds;
        }

        /// <summary>
        /// 获取两个时间的间隔
        /// </summary>
        /// <param name="time1">基准时间</param>
        /// <param name="time2">比较的时间</param>
        /// <returns>间隔分钟数</returns>
        public static double TotalMinutesOfTimeInterval(this DateTime time1, DateTime time2)
        {
            return time1.TimeSpanOfTimeInterval(time2).TotalMinutes;
        }

        /// <summary>
        /// 获取两个时间的间隔
        /// </summary>
        /// <param name="time1">基准时间</param>
        /// <param name="time2">比较的时间</param>
        /// <returns>间隔秒数</returns>
        public static double TotalSecondsOfTimeInterval(this DateTime time1, DateTime time2)
        {
            return time1.TimeSpanOfTimeInterval(time2).TotalSeconds;
        }

        #endregion

        #region 3.0 计算向上取整的时间间隔

        /// <summary>
        /// 获取两个时间的间隔
        /// </summary>
        /// <param name="time1">基准时间</param>
        /// <param name="time2">比较的时间</param>
        /// <returns>向上取整的间隔天数</returns>
        public static double CeilingDaysOfTimeInterval(this DateTime time1, DateTime time2)
        {
            return Math.Ceiling(time1.TotalDaysOfTimeInterval(time2));
        }

        /// <summary>
        /// 获取两个时间的间隔
        /// </summary>
        /// <param name="time1">基准时间</param>
        /// <param name="time2">比较的时间</param>
        /// <returns>向上取整的间隔小时数</returns>
        public static double CeilingHoursOfTimeInterval(this DateTime time1, DateTime time2)
        {
            return Math.Ceiling(time1.TotalHoursOfTimeInterval(time2));
        }

        /// <summary>
        /// 获取两个时间的间隔
        /// </summary>
        /// <param name="time1">基准时间</param>
        /// <param name="time2">比较的时间</param>
        /// <returns>向上取整的间隔毫秒数</returns>
        public static double CeilingMillisecondsOfTimeInterval(this DateTime time1, DateTime time2)
        {
            return Math.Ceiling(time1.TotalMillisecondsOfTimeInterval(time2));
        }

        /// <summary>
        /// 获取两个时间的间隔
        /// </summary>
        /// <param name="time1">基准时间</param>
        /// <param name="time2">比较的时间</param>
        /// <returns>向上取整的间隔分钟数</returns>
        public static double CeilingMinutesOfTimeInterval(this DateTime time1, DateTime time2)
        {
            return Math.Ceiling(time1.TotalMinutesOfTimeInterval(time2));
        }

        /// <summary>
        /// 获取两个时间的间隔
        /// </summary>
        /// <param name="time1">基准时间</param>
        /// <param name="time2">比较的时间</param>
        /// <returns>向上取整的间隔秒数</returns>
        public static double CeilingSecondsOfTimeInterval(this DateTime time1, DateTime time2)
        {
            return Math.Ceiling(time1.TotalSecondsOfTimeInterval(time2));
        }

        #endregion
    }
}
