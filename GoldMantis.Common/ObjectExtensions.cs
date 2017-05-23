/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         ObjectExtensions.cs
** Creator:          Joe
** CreateDate:       2015-03-26
** Changer:
** LastChangeDate:
** Description:      Object扩展方法. 包括空值判断和类型转换功能
** VersionInfo:
*********************************************************/

using System;

namespace GoldMantis.Common
{
    public static class ObjectExtensions
    {
        #region 1.0 Object 空值判断

        /// <summary>
        /// 判断对象值是否是null
        /// </summary>
        /// <param name="value">判断的对象</param>
        /// <returns>是否是null</returns>
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        /// <summary>
        /// 判断对象值是否 不是null
        /// </summary>
        /// <param name="value">判断的对象</param>
        /// <returns>是否 不是null</returns>
        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }

        /// <summary>
        /// 判断对象值是否是null或者DBNull
        /// </summary>
        /// <param name="value">判断的对象</param>
        /// <returns>是否是null或者DBNull</returns>
        public static bool IsDbNull(this object value)
        {
            return value == null || value.Equals(DBNull.Value);
        }

        /// <summary>
        /// 判断对象值是否 不是null或者DBNull
        /// </summary>
        /// <param name="value">判断的对象</param>
        /// <returns>是否 不是null或者DBNull</returns>
        public static bool IsNotDbNull(this object value)
        {
            return !value.IsDbNull();
        }

        #endregion

        #region 2.0 Object To Int32

        /// <summary>
        /// Object转换为整型
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>整型</returns>
        public static int ToInt32(this object value)
        {
            if (value.IsDbNull())
            {
                throw new ArgumentNullException("value");
            }


            return value.ToString().StringToInt32();
        }

        /// <summary>
        /// Object转换为整型
        /// 如果Object为空值,返回指定默认值
        /// </summary>
        /// <param name="value">Object</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>整型</returns>
        public static int ToDefaultInt32(this object value, int defaultValue = 0)
        {
            return value.IsDbNull() ? defaultValue : value.ToString().StringToDefaultInt32(defaultValue);
        }

        /// <summary>
        /// Object转换为可空整型
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>整型</returns>
        public static int? ToNullableInt32(this object value)
        {
            if (value.IsDbNull())
            {
                return null;
            }


            return value.ToString().StringToNullableInt32();
        }

        #endregion

        #region 3.0 Object To Decimal

        /// <summary>
        /// Object转换为Decimal
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Decimal</returns>
        public static decimal ToDecimal(this object value)
        {
            if (value.IsDbNull())
            {
                throw new ArgumentNullException("value");
            }


            return value.ToString().StringToDecimal();
        }

        /// <summary>
        /// Object转换为Decimal
        /// 如果Object为空值,返回指定默认值
        /// </summary>
        /// <param name="value">Object</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>Decimal</returns>
        public static decimal ToDefaultDecimal(this object value, decimal defaultValue = 0)
        {
            return value.IsDbNull() ? defaultValue : value.ToString().StringToDefaultDecimal(defaultValue);
        }

        /// <summary>
        /// Object转换为可空Decimal
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Decimal</returns>
        public static decimal? ToNullableDecimal(this object value)
        {
            if (value.IsDbNull())
            {
                return null;
            }


            return value.ToString().StringToNullableDecimal();
        }

        #endregion

        #region 4.0 Object To Double

        /// <summary>
        /// Object转换为Double
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Double</returns>
        public static double ToDouble(this object value)
        {
            if (value.IsDbNull())
            {
                throw new ArgumentNullException("value");
            }


            return value.ToString().StringToDouble();
        }

        /// <summary>
        /// Object转换为Double
        /// 如果Object为空值,返回指定默认值
        /// </summary>
        /// <param name="value">Object</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>Double</returns>
        public static double ToDefaultDouble(this object value, double defaultValue = 0)
        {
            return value.IsDbNull() ? defaultValue : value.ToString().StringToDefaultDouble(defaultValue);
        }

        /// <summary>
        /// Object转换为可空Double
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Double</returns>
        public static double? ToNullableDouble(this object value)
        {
            if (value.IsDbNull())
            {
                return null;
            }


            return value.ToString().StringToNullableDouble();
        }

        #endregion

        #region 5.0 Object To Boolean

        /// <summary>
        /// Object转换为Boolean
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Boolean</returns>
        public static bool ToBoolean(this object value)
        {
            if (value.IsDbNull())
            {
                throw new ArgumentNullException("value");
            }


            return value.ToString().StringToBoolean();
        }

        /// <summary>
        /// Object转换为Boolean
        /// 如果Object为空值,返回指定默认值
        /// </summary>
        /// <param name="value">Object</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>Boolean</returns>
        public static bool ToDefaultBoolean(this object value, bool defaultValue = false)
        {
            return value.IsDbNull() ? defaultValue : value.ToString().StringToDefaultBoolean(defaultValue);
        }

        /// <summary>
        /// Object转换为可空Boolean
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Boolean</returns>
        public static bool? ToNullableBoolean(this object value)
        {
            if (value.IsDbNull())
            {
                return null;
            }


            return value.ToString().StringToNullableBoolean();
        }

        #endregion

        #region 6.0 Object To DateTime

        /// <summary>
        /// Object转换为DateTime
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(this object value)
        {
            if (value.IsDbNull())
            {
                throw new ArgumentNullException("value");
            }


            return value.ToString().StringToDateTime();
        }

        /// <summary>
        /// Object转换为DateTime
        /// 如果Object为空值,返回指定默认值
        /// </summary>
        /// <param name="value">Object</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDefaultDateTime(this object value, DateTime defaultValue)
        {
            return value.IsDbNull() ? defaultValue : value.ToString().StringToDefaultDateTime(defaultValue);
        }

        /// <summary>
        /// Object转换为可空DateTime
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>DateTime</returns>
        public static DateTime? ToNullableDateTime(this object value)
        {
            if (value.IsDbNull())
            {
                return null;
            }


            return value.ToString().StringToNullableDateTime();
        }

        #endregion

        #region 7.0 Object To Guid

        /// <summary>
        /// Object转换为Guid
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Guid</returns>
        public static Guid ToGuid(this object value)
        {
            if (value.IsDbNull())
            {
                throw new ArgumentNullException("value");
            }


            return value.ToString().StringToGuid();
        }

        /// <summary>
        /// Object转换为Guid
        /// 如果Object为空值,返回指定默认值
        /// </summary>
        /// <param name="value">Object</param>
        /// <param name="defaultValue">指定默认值</param>
        /// <returns>Guid</returns>
        public static Guid ToDefaultGuid(this object value, Guid defaultValue)
        {
            return value.IsDbNull() ? defaultValue : value.ToString().StringToDefaultGuid(defaultValue);
        }

        /// <summary>
        /// Object转换为可空Guid
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Guid</returns>
        public static Guid? ToNullableGuid(this object value)
        {
            if (value.IsDbNull())
            {
                return null;
            }


            return value.ToString().StringToNullableGuid();
        }

        #endregion

        /// <summary>
        /// 将对象转换为强类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T As<T>(this object value) where T : class
        {
            if (value.IsDbNull())
            {
                return null;
            }

            return value as T;
        }
    }
}
