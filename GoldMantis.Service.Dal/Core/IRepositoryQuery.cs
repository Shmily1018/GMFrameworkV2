/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         IRepositoryQuery.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类--查询操作
** VersionInfo:
*********************************************************/

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GoldMantis.Common;

namespace GoldMantis.Service.Dal
{
    public interface IRepositoryQuery<T> where T : class
    {
        /// <summary>
        /// 根据主键查询实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        T GetByKey<TInput>(TInput id);

        /// <summary>
        /// 查询首条记录
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orders">order条件</param>
        /// <returns>首条记录</returns>
        T First(Expression<Func<T, bool>> where, OrderExpression<T>[] orders);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orders">order条件</param>
        /// <returns>列表</returns>
        IList<T> List(Expression<Func<T, bool>> where, OrderExpression<T>[] orders);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="property">列名</param>
        /// <returns>列表</returns>
        IList<T> ListIn<U>(IList<U> collection, Expression<Func<T, object>> property);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="where">where条件</param>
        /// <param name="orders">order条件</param>
        /// <returns>列表</returns>
        IList<T> ListTopX(int count, Expression<Func<T, bool>> where, OrderExpression<T>[] orders);

        /// <summary>
        /// 按页查询
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orders">order条件</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="count">总数</param>
        /// <returns>页码内的集合</returns>
        IList<T> PaginateList(Expression<Func<T, bool>> where, OrderExpression<T>[] orders,int pageSize, int pageIndex, ref int count);

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns>总数</returns>
        int Count(Expression<Func<T, bool>> where);
    }
}