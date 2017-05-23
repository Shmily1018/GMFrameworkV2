/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         BaseViewBiz.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类
** VersionInfo:
*********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GoldMantis.Common;
using GoldMantis.Service.Dal;

namespace GoldMantis.Service.Biz
{
    public abstract class BaseViewBiz<T> : BaseBiz<T> where T : class
    {
        /// <summary>
        /// DAL实例
        /// </summary>
        protected RepositoryView<T> DataAccess
        {
            get { return Repository.As<RepositoryView<T>>(); }
        }

        /// <summary>
        /// HQL ：Hibernate Query Language
        /// 根据Hibernate查询语言查询
        /// </summary>
        /// <param name="hql">Hibernate查询语言</param>
        /// <returns>实体集合</returns>
        public virtual IList<T> FindByHql(string hql)
        {
            return DataAccess.FindByHql(hql);
        }

        /// <summary>
        /// 根据ID获得对象
        /// </summary>
        /// <typeparam name="TInput">主键数据类型</typeparam>
        /// <param name="id">主键值</param>
        /// <returns>对象</returns>
        public virtual T GetByKey<TInput>(TInput id)
        {
            return DataAccess.GetByKey(id);
        }

        #region First

        public virtual T First()
        {
            return DataAccess.First();
        }

        public virtual T First(Expression<Func<T, bool>> where)
        {
            return DataAccess.First(where);
        }

        public virtual T First(OrderExpression<T>[] orders)
        {
            return DataAccess.First(orders);
        }

        public virtual T First(Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
            return DataAccess.First(orderColumn, orderBy);
        }

        public virtual T First(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
            return DataAccess.First(where, orderColumn, orderBy);
        }

        /// <summary>
        /// 查询首条记录
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orders">order条件</param>
        /// <returns>首条记录</returns>
        public virtual T First(Expression<Func<T, bool>> where, OrderExpression<T>[] orders)
        {
            return DataAccess.First(where, orders);
        }

        #endregion

        #region List

        public virtual IList<T> List()
        {
            return DataAccess.List();
        }

        public virtual IList<T> List(Expression<Func<T, bool>> where)
        {
            return DataAccess.List(where);
        }

        public virtual IList<T> List(OrderExpression<T>[] orders)
        {
            return DataAccess.List(orders);
        }

        public virtual IList<T> List(Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
            return DataAccess.List(orderColumn, orderBy);
        }

        public virtual IList<T> List(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
           
            return List(where, orderColumn, orderBy);
        }

        public virtual IList<T> List(Expression<Func<T, bool>> where, string path, OrderBy orderBy)
        {
            

            OrderExpression<T>[] orderCollection =  { new OrderExpression<T>() { OrderBy = orderBy, OrderColumn = ReflectionHelper.GetClassPropertyExpression<T>(path) } };
            return DataAccess.List(where, orderCollection);
        }


        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orders">order条件</param>
        /// <returns>列表</returns>
        public virtual IList<T> List(Expression<Func<T, bool>> where, OrderExpression<T>[] orders)
        {
            return DataAccess.List(where, orders);
        }

        #endregion

        #region ListIn

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="property">列名</param>
        /// <returns>列表</returns>
        public virtual IList<T> ListIn<U>(IList<U> collection, Expression<Func<T, object>> property)
        {
            return DataAccess.ListIn<U>(collection, property);
        }

        #endregion

        #region ListTopX

        public virtual IList<T> ListTopX(int count)
        {
            return DataAccess.ListTopX(count);
        }

        public virtual IList<T> ListTopX(int count, Expression<Func<T, bool>> where)
        {
            return DataAccess.ListTopX(count, where);
        }

        public virtual IList<T> ListTopX(int count, OrderExpression<T>[] orders)
        {
            return DataAccess.ListTopX(count, orders);
        }

        public virtual IList<T> ListTopX(int count, Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
            return DataAccess.ListTopX(count, orderColumn, orderBy);
        }

        public virtual IList<T> ListTopX(int count, Expression<Func<T, bool>> where, Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
            return ListTopX(count, where, orderColumn, orderBy);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="count">数量</param>
        /// <param name="where">where条件</param>
        /// <param name="orders">order条件</param>
        /// <returns>列表</returns>
        public virtual IList<T> ListTopX(int count, Expression<Func<T, bool>> where, OrderExpression<T>[] orders)
        {
            return DataAccess.ListTopX(count, where, orders);
        }

        #endregion

        #region PaginateList

        public virtual IList<T> PaginateList(int pageSize, int pageIndex, ref int count)
        {
            return DataAccess.PaginateList(pageSize, pageIndex, ref count);
        }

        public virtual IList<T> PaginateList(Expression<Func<T, bool>> where, int pageSize, int pageIndex, ref int count)
        {
            return DataAccess.PaginateList(where, pageSize, pageIndex, ref count);
        }

        public virtual IList<T> PaginateList(OrderExpression<T>[] orders, int pageSize, int pageIndex, ref int count)
        {
            return DataAccess.PaginateList(orders, pageSize, pageIndex, ref count);
        }

        public virtual IList<T> PaginateList(Expression<Func<T, object>> orderColumn, OrderBy orderBy,
            int pageSize, int pageIndex, ref int count)
        {
            return DataAccess.PaginateList(orderColumn, orderBy, pageSize, pageIndex, ref count);
        }

        public virtual IList<T> PaginateList(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderColumn, OrderBy orderBy,
            int pageSize, int pageIndex, ref int count)
        {
            return DataAccess.PaginateList(where, orderColumn, orderBy , pageSize, pageIndex, ref count);
        }

        /// <summary>
        /// 按页查询
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orders">order条件</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="count">总数</param>
        /// <returns>页码内的集合</returns>
        public virtual IList<T> PaginateList(Expression<Func<T, bool>> where, OrderExpression<T>[] orders,
            int pageSize, int pageIndex, ref int count)
        {
            return DataAccess.PaginateList(where, orders, pageSize, pageIndex, ref count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="count"></param>
        /// <param name="criteria"></param>
        /// <param name="path"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        protected virtual IList<T> PaginateList(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria, string path, OrderBy direction)
        {
            return DataAccess.PaginateList(pageSize, pageIndex, ref count, criteria, ReflectionHelper.GetClassPropertyExpression<T>(path), direction);
        }

        #endregion

        #region Count

        public virtual int Count()
        {
            return DataAccess.Count();
        }

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns>总数</returns>
        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return DataAccess.Count(where);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public OrderExpression<T>[] ConvertPathToLinqOrderArray(string path, string direction)
        {
            var pathCollection = path.Split(new char[] {'#'}, StringSplitOptions.RemoveEmptyEntries);
            var directionCollection = direction.Split(new char[] {'#'}, StringSplitOptions.RemoveEmptyEntries);
            IList<OrderExpression<T>> linqOrderCollection = new List<OrderExpression<T>>();

            for (int i = 0; i < pathCollection.Count(); i++)
            {
                var tempExpression = ReflectionHelper.GetClassPropertyExpression<T>(pathCollection[i]);

                if (tempExpression == null)
                {
                    continue;
                }


                OrderBy tempOrder;

                if ((directionCollection.Length - 1) >= i)
                {
                    if (directionCollection[i].Trim() != ((int) OrderBy.DESC).ToString() &&
                        directionCollection[i].Trim().ToUpper() != ((int) OrderBy.ASC).ToString())
                    {
                        tempOrder = OrderBy.ASC;
                    }
                    else
                    {
                        tempOrder = (OrderBy) Convert.ToInt32(directionCollection[i]);
                    }
                }
                else
                {
                    tempOrder = OrderBy.ASC;
                }

                OrderExpression<T> tempLinqOrder = new OrderExpression<T>();
                tempLinqOrder.OrderColumn = tempExpression;
                tempLinqOrder.OrderBy = tempOrder;
                linqOrderCollection.Add(tempLinqOrder);
            }


            return linqOrderCollection.ToArray();
        }
    }
}