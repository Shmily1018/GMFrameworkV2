/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         IEnumerableExtensions.cs
** Creator:          Joe
** CreateDate:       2015-04-01
** Changer:
** LastChangeDate:
** Description:      IEnumerable扩展类
** VersionInfo:
*********************************************************/

using System.Collections.Generic;
using System.Linq;

namespace GoldMantis.Common
{
    public static class IEnumerableExtensions
    {
        public static string Join<T>(this IEnumerable<T> values, string separator = ",")
        {
            if (values.IsNull() || values.Count() == 0)
            {
                return string.Empty;
            }


            if (separator.IsNull())
            {
                separator = string.Empty;
            }


            return string.Join(separator, values);
        }

        public static bool HasItems<T>(this IEnumerable<T> values)
        {
            return values.IsNotNull() && values.Count() > 0;
        }
    }
}
