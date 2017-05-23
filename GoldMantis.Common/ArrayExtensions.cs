/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         ArrayExtensions.cs
** Creator:          Joe
** CreateDate:       2015-04-01
** Changer:
** LastChangeDate:
** Description:      Array扩展
** VersionInfo:
*********************************************************/

using System;

namespace GoldMantis.Common
{
    public static class ArrayExtensions
    {
        //public delegate bool Predicate<in T>(T obj)
        //public delegate TOutput Converter<in TInput, out TOutput>(TInput input)

        #region 1.0 将一种类型的数组转换为另一种类型的数组

        /// <summary>
        /// 将一种类型的数组转换为另一种类型的数组
        /// </summary>
        /// <typeparam name="TInput">源数组元素的类型</typeparam>
        /// <typeparam name="TOutput">目标数组元素的类型</typeparam>
        /// <param name="array">要转换为目标类型的从零开始的一维 Array</param>
        /// <param name="converter">用于将每个元素从一种类型转换为另一种类型</param>
        /// <returns>目标类型的数组，包含从源数组转换而来的元素</returns>
        public static TOutput[] ConvertAll<TInput, TOutput>(this TInput[] array, Converter<TInput, TOutput> converter)
        {
            return Array.ConvertAll(array, converter);
        }

        #endregion

        #region 2.0 判断数组中是否包含符合条件Predicate的元素

        /// <summary>
        /// 确定指定数组包含的元素是否与指定谓词定义的条件匹配
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="match">定义要搜索的元素的条件</param>
        /// <returns>如果 array 包含一个或多个元素与指定谓词定义的条件匹配，则为 true；否则为 false</returns>
        public static bool Exists<T>(this T[] array, Predicate<T> match)
        {
            return Array.Exists(array, match);
        }

        #endregion

        #region 3.0 判断数组中是否包含元素

        /// <summary>
        /// 判断数组中是否包含元素
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="value">要搜索的元素</param>
        /// <returns>如果 array 包含一个或多个元素，则为 true；否则为 false</returns>
        public static bool Contains<T>(this T[] array, T value)
        {
            return array.IndexOf(value) != -1;
        }

        #endregion

        #region 4.0 遍历数组,执行操作

        /// <summary>
        /// 对指定数组的每个元素执行指定操作
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">从零开始的一维 Array，要对其元素执行操作</param>
        /// <param name="action">要对 array 的每个元素执行的 Action</param>
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            Array.ForEach(array, action);
        }

        #endregion

        #region 5.0 判断数组所有元素是否都符合某一条件

        /// <summary>
        /// 确定数组中的每个元素是否都与指定谓词定义的条件匹配
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要对照条件进行检查的从零开始的一维 Array</param>
        /// <param name="match">定义检查元素时要对照的条件</param>
        /// <returns>如果 array 中的每个元素都与指定谓词定义的条件匹配，则为 true；否则为 false。 如果数组中没有元素，则返回值为 true</returns>
        public static bool TrueForAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.TrueForAll(array, match);
        }

        #endregion

        #region 6.0 查找数组中符合条件Predicate的元素,如果没有,返回类型的默认值

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 Array 中的第一个匹配元素
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="match">定义要搜索的元素的条件</param>
        /// <returns>如果找到与指定谓词定义的条件匹配的第一个元素，则为该元素；否则为类型 T 的默认值</returns>
        public static T Find<T>(this T[] array, Predicate<T> match)
        {
            return Array.Find(array, match);
        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 Array 中的最后一个匹配元素
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="match">定义要搜索的元素的条件</param>
        /// <returns>如果找到，则为与指定谓词所定义的条件相匹配的最后一个元素；否则为类型 T 的默认值</returns>
        public static T FindLast<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLast(array, match);
        }

        /// <summary>
        /// 检索与指定谓词定义的条件匹配的所有元素
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="match">定义要搜索的元素的条件</param>
        /// <returns>如果找到一个其中所有元素均与指定谓词定义的条件匹配的 Array，则为该数组；否则为一个空 Array</returns>
        public static T[] FindAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindAll(array, match);
        }

        #endregion

        #region 7.0 查找数组中符合条件的第一个元素的索引,没有则返回-1

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 Array 中第一个匹配元素的从零开始的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="match">定义要搜索的元素的条件</param>
        /// <returns>如果找到与 match 定义的条件相匹配的第一个元素，则为该元素的从零开始的索引；否则为 -1</returns>
        public static int FindIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回 Array 中从指定索引到最后一个元素的元素范围内第一个匹配项的从零开始的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="startIndex">从零开始的搜索的起始索引</param>
        /// <param name="match">定义要搜索的元素的条件</param>
        /// <returns>如果找到与 match 定义的条件相匹配的第一个元素，则为该元素的从零开始的索引；否则为 -1</returns>
        public static int FindIndex<T>(this T[] array, int startIndex, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, match);
        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的一个元素，并返回 Array 中从指定的索引开始、包含指定元素个数的元素范围内第一个匹配项的从零开始的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="startIndex">从零开始的搜索的起始索引</param>
        /// <param name="count">要搜索的部分中的元素数</param>
        /// <param name="match">定义要搜索的元素的条件</param>
        /// <returns>如果找到与 match 定义的条件相匹配的第一个元素，则为该元素的从零开始的索引；否则为 -1</returns>
        public static int FindIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, count, match);
        }

        #endregion

        #region 8.0 查找数组中符合条件的最后一元素的索引,没有则返回-1

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回整个 Array 中最后一个匹配元素的从零开始的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="match">定义要搜索的元素的条件</param>
        /// <returns>如果找到与 match 定义的条件相匹配的最后一个元素，则为该元素的从零开始的索引；否则为 -1</returns>
        public static int FindLastIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLastIndex(array, match);
        }

        /// <summary>
        /// 搜索与由指定谓词定义的条件相匹配的元素，并返回 Array 中从第一个元素到指定索引的元素范围内最后一个匹配项的从零开始的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="startIndex">向后搜索的从零开始的起始索引</param>
        /// <param name="match">定义要搜索的元素的条件</param>
        /// <returns>如果找到与 match 定义的条件相匹配的最后一个元素，则为该元素的从零开始的索引；否则为 -1</returns>
        public static int FindLastIndex<T>(this T[] array, int startIndex, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, match);
        }

        /// <summary>
        /// 搜索与指定谓词所定义的条件相匹配的元素，并返回 Array 中包含指定元素个数、到指定索引结束的元素范围内最后一个匹配项的从零开始的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="startIndex">向后搜索的从零开始的起始索引</param>
        /// <param name="count">要搜索的部分中的元素数</param>
        /// <param name="match">定义要搜索的元素的条件</param>
        /// <returns>如果找到与 match 定义的条件相匹配的最后一个元素，则为该元素的从零开始的索引；否则为 -1</returns>
        public static int FindLastIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, count, match);
        }

        #endregion

        #region 9.0 查找数组中第一个元素的索引,没有则返回-1

        /// <summary>
        /// 搜索指定的对象，并返回整个 Array 中第一个匹配项的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="value">要在 array 中查找的对象</param>
        /// <returns>如果在整个 array 中找到 value 的第一个匹配项，则为该项的从零开始的索引；否则为 -1</returns>
        public static int IndexOf<T>(this T[] array, T value)
        {
            return Array.IndexOf(array, value);
        }

        /// <summary>
        /// 搜索指定的对象，并返回 Array 中从指定索引到最后一个元素这部分元素中第一个匹配项的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="value">要在 array 中查找的对象</param>
        /// <param name="startIndex">从零开始的搜索的起始索引。 空数组中 0（零）为有效值</param>
        /// <returns>如果在 array 中从 startIndex 到最后一个元素这部分元素中找到 value 的匹配项，则为第一个匹配项的从零开始的索引；否则为 -1</returns>
        public static int IndexOf<T>(this T[] array, T value, int startIndex)
        {
            return Array.IndexOf(array, value, startIndex);
        }

        /// <summary>
        /// 搜索指定的对象，并返回 Array 中从指定索引开始包含指定个元素的这部分元素中第一个匹配项的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="value">要在 array 中查找的对象</param>
        /// <param name="startIndex">从零开始的搜索的起始索引。 空数组中 0（零）为有效值</param>
        /// <param name="count">要搜索的部分中的元素数</param>
        /// <returns>如果在 array 中从 startIndex 开始、包含 count 所指定的元素个数的这部分元素中，找到 value 的匹配项，则为第一个匹配项的从零开始的索引；否则为 -1</returns>
        public static int IndexOf<T>(this T[] array, T value, int startIndex, int count)
        {
            return Array.IndexOf(array, value, startIndex, count);
        }

        #endregion

        #region 10.0查找数组中最后一元素的索引,没有则返回-1

        /// <summary>
        /// 搜索指定的对象，并返回整个 Array 中最后一个匹配项的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="value">要在 array 中查找的对象</param>
        /// <returns>如果在整个 array 中找到 value 的匹配项，则为最后一个匹配项的从零开始的索引；否则为 -1</returns>
        public static int LastIndexOf<T>(this T[] array, T value)
        {
            return Array.LastIndexOf(array, value);
        }

        /// <summary>
        /// 搜索指定的对象，并返回 Array 中从第一个元素到指定索引这部分元素中最后一个匹配项的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="value">要在 array 中查找的对象</param>
        /// <param name="startIndex">向后搜索的从零开始的起始索引</param>
        /// <returns>如果在 array 中从第一个元素到 startIndex 这部分元素中找到 value 的匹配项，则为最后一个匹配项的从零开始的索引；否则为 -1</returns>
        public static int LastIndexOf<T>(this T[] array, T value, int startIndex)
        {
            return Array.LastIndexOf(array, value, startIndex);
        }

        /// <summary>
        /// 搜索指定的对象，并返回 Array 中到指定索引为止包含指定个元素的这部分元素中最后一个匹配项的索引
        /// </summary>
        /// <typeparam name="T">数组元素的类型</typeparam>
        /// <param name="array">要搜索的从零开始的一维 Array</param>
        /// <param name="value">要在 array 中查找的对象</param>
        /// <param name="startIndex">向后搜索的从零开始的起始索引</param>
        /// <param name="count">要搜索的部分中的元素数</param>
        /// <returns>如果在 array 中到 startIndex 为止、包含 count 所指定的元素个数的这部分元素中，找到 value 的匹配项，则为最后一个匹配项的从零开始的索引；否则为 -1</returns>
        public static int LastIndexOf<T>(this T[] array, T value, int startIndex, int count)
        {
            return Array.LastIndexOf(array, value, startIndex, count);
        }

        #endregion
    }
}
