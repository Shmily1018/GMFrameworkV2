/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         OrderExpression.cs
** Creator:          Joe
** CreateDate:       2015-03-27
** Changer:
** LastChangeDate:
** Description:      数据库排序
** VersionInfo:
*********************************************************/
using System;
using System.Linq.Expressions;

namespace GoldMantis.Common
{
    public class OrderExpression<T>
    {
        /// <summary>
        /// 排序列表达式
        /// </summary>
        public Expression<Func<T, object>> OrderColumn { get; set; }

        /// <summary>
        /// 排序方向
        /// </summary>
        public OrderBy OrderBy { get; set; }
    }
}