/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         RepositoryView.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类--查询操作
** VersionInfo:
*********************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using GoldMantis.Common;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace GoldMantis.Service.Dal
{
    public class RepositoryView<T> : Repository<T>, IRepositoryQuery<T> where T : class
    {
        protected static OrderExpression<T>[] _orderExpressions;
        protected string _defaultKeyID = "ID";
        protected Type type = typeof(T);

        public virtual string KeyID
        {
            get { return _defaultKeyID; }
        }

        /// <summary>
        /// HQL ：Hibernate Query Language
        /// 根据Hibernate查询语言查询
        /// </summary>
        /// <param name="hql">Hibernate查询语言</param>
        /// <returns>实体集合</returns>
        public virtual IList<T> FindByHql(string hql)
        {
            IQuery query = Session.CreateQuery(hql);
            IList<T> list = query.Enumerable<T>().ToList();

            return list;
        }

        /// <summary>
        /// 根据ID获得对象
        /// </summary>
        /// <typeparam name="TInput">主键数据类型</typeparam>
        /// <param name="id">主键值</param>
        /// <returns>对象</returns>
        public virtual T GetByKey<TInput>(TInput id)
        {
            IList<T> list = Session.CreateCriteria<T>().Add(Restrictions.Eq(KeyID, id)).List<T>();

            if (list.HasItems())
            {
                return list[0];
            }

            return null;
        }

        #region First

        public virtual T First()
        {
            return First(null, null);
        }

        public virtual T First(Expression<Func<T, bool>> where)
        {
            return First(where, null);
        }

        public virtual T First(OrderExpression<T>[] orders)
        {
            return First(null, orders);
        }

        public virtual T First(Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
            return First(null, orderColumn, orderBy);
        }

        public virtual T First(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
            OrderExpression<T>[] orders = null;

            if (orderColumn.IsNotNull() && orderBy.IsNotNull())
            {
                orders = new OrderExpression<T>[]
                {
                    new OrderExpression<T>()
                    {
                        OrderBy = orderBy,
                        OrderColumn = orderColumn
                    }
                };
            }

            return First(where, orders);
        }

        /// <summary>
        /// 查询首条记录
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orders">order条件</param>
        /// <returns>首条记录</returns>
        public virtual T First(Expression<Func<T, bool>> where, OrderExpression<T>[] orders)
        {
            var list = ListTopX(1, where, orders);

            if (list.HasItems())
            {
                return list[0];
            }

            return null;
        }

        #endregion

        #region List

        public virtual IList<T> List()
        {
            return List(null, null);
        }

        public virtual IList<T> List(Expression<Func<T, bool>> where)
        {
            return List(where, null);
        }

        public virtual IList<T> List(OrderExpression<T>[] orders)
        {
            return List(null, orders);
        }

        public virtual IList<T> List(Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
            return List(null, orderColumn, orderBy);
        }

        public virtual IList<T> List(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
            OrderExpression<T>[] orders = null;

            if (orderColumn.IsNotNull() && orderBy.IsNotNull())
            {
                orders = new OrderExpression<T>[]
                {
                    new OrderExpression<T>()
                    {
                        OrderBy = orderBy,
                        OrderColumn = orderColumn
                    }
                };
            }

            return List(where, orders);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="where">where条件</param>
        /// <param name="orders">order条件</param>
        /// <returns>列表</returns>
        public virtual IList<T> List(Expression<Func<T, bool>> where, OrderExpression<T>[] orders)
        {
            IQueryable<T> query = Session.Query<T>();

            if (where.IsNotNull())
            {
                query = query.Where(where);
            }

            //添加排序条件
            query = AttachOrders(query, orders);

            return query.ToList();
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
            MemberExpression me = property.Body as MemberExpression;
            string propertyName = string.Empty;

            if (me.IsNull())
            {
                string body = property.Body.ToString();
                string strBody = body.Substring(8, body.Length - 8 - 1);
                propertyName = strBody.Substring(strBody.IndexOf(".") + 1, strBody.Length - strBody.IndexOf(".") - 1);
            }
            else
            {
                propertyName = me.Member.Name;
            }


            string strIds = string.Join(",", collection.ToArray().ConvertAll(x => string.Format("'{0}'", x.ToString())));
            IQuery query = Session.CreateQuery(string.Format("Select t from {0} t  where {2} in ({1})",
                NHibernateHelper.NHConguration.GetClassMapping(type).MappedClass.Name, strIds, propertyName));

            return query.List<T>();
        }

        #endregion

        #region ListTopX

        public virtual IList<T> ListTopX(int count)
        {
            return ListTopX(count, null, null);
        }

        public virtual IList<T> ListTopX(int count, Expression<Func<T, bool>> where)
        {
            return ListTopX(count, where, null);
        }

        public virtual IList<T> ListTopX(int count, OrderExpression<T>[] orders)
        {
            return ListTopX(count, null, orders);
        }

        public virtual IList<T> ListTopX(int count, Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
            return ListTopX(count, null, orderColumn, orderBy);
        }

        public virtual IList<T> ListTopX(int count, Expression<Func<T, bool>> where, Expression<Func<T, object>> orderColumn, OrderBy orderBy)
        {
            OrderExpression<T>[] orders = null;

            if (orderColumn.IsNotNull() && orderBy.IsNotNull())
            {
                orders = new OrderExpression<T>[]
                {
                    new OrderExpression<T>()
                    {
                        OrderBy = orderBy,
                        OrderColumn = orderColumn
                    }
                };
            }

            return ListTopX(count, where, orders);
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
            IQueryable<T> query = Session.Query<T>();

            if (where.IsNotNull())
            {
                query = query.Where(where);
            }

            //添加排序条件
            query = AttachOrders(query, orders);

            return query.Take(count).ToList();
        }

        #endregion

        #region PaginateList

        public virtual IList<T> PaginateList(int pageSize, int pageIndex, ref int count)
        {
            return PaginateList(null, null, pageSize, pageIndex, ref count);
        }

        public virtual IList<T> PaginateList(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria, params OrderExpression<T>[] orderCollection)
        {
            IList<T> list = null;
            IQueryable<T> query = Session.Query<T>();
            if (criteria != null)
                query = query.Where(criteria);

            //添加排序条件
            query = AttachOrders(query, orderCollection);
            //进行分页
            list = query.Skip((pageIndex) * pageSize).Take(pageSize).ToList();
            count = criteria == null ? Session.Query<T>().Count() : Session.Query<T>().Where(criteria).Count();
            return list;
        }

        public virtual IList<T> PaginateList(int pageSize, int pageIndex, ref int count, Expression<Func<T, bool>> criteria, Expression<Func<T, object>> path, OrderBy direction)
        {
            OrderExpression<T>[] orderCollection = null;
            orderCollection = path == null ? _orderExpressions : (new OrderExpression<T>[] { new OrderExpression<T>() { OrderBy = direction, OrderColumn = path } });
            return PaginateList(pageSize, pageIndex, ref count, criteria, orderCollection);
        }

        public virtual IList<T> PaginateList(Expression<Func<T, bool>> where, int pageSize, int pageIndex, ref int count)
        {
            return PaginateList(where, null, pageSize, pageIndex, ref count);
        }

        public virtual IList<T> PaginateList(OrderExpression<T>[] orders, int pageSize, int pageIndex, ref int count)
        {
            return PaginateList(null, orders, pageSize, pageIndex, ref count);
        }

        public virtual IList<T> PaginateList(Expression<Func<T, object>> orderColumn, OrderBy orderBy,
            int pageSize, int pageIndex, ref int count)
        {
            return PaginateList(null, orderColumn, orderBy, pageSize, pageIndex, ref count);
        }

        public virtual IList<T> PaginateList(Expression<Func<T, bool>> where, Expression<Func<T, object>> orderColumn, OrderBy orderBy,
            int pageSize, int pageIndex, ref int count)
        {
            OrderExpression<T>[] orders = null;

            if (orderColumn.IsNotNull() && orderBy.IsNotNull())
            {
                orders = new OrderExpression<T>[]
                {
                    new OrderExpression<T>()
                    {
                        OrderBy = orderBy,
                        OrderColumn = orderColumn
                    }
                };
            }

            return PaginateList(where, orders, pageSize, pageIndex, ref count);
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
            IList<T> list = null;
            IQueryable<T> query = Session.Query<T>();

            if (where.IsNotNull())
            {
                query = query.Where(where);
            }

            //添加排序条件
            query = AttachOrders(query, orders);
            //进行分页
            list = query.Skip((pageIndex) * pageSize).Take(pageSize).ToList();
            count = Count(where);

            return list;
        }

        #endregion

        #region Count

        public virtual int Count()
        {
            return Count(null);
        }

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="where">where条件</param>
        /// <returns>总数</returns>
        public virtual int Count(Expression<Func<T, bool>> where)
        {
            IQueryable<T> query = Session.Query<T>();

            if (where.IsNotNull())
            {
                query = query.Where(where);
            }

            return query.Count();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual object FindUniqueResultBySQL(string sql)
        {
            ISQLQuery query = Session.CreateSQLQuery(sql);
            DataTable dt = new DataTable();

            return query.UniqueResult();
        }

        /// <summary>
        /// 排序条件添加
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orderCollection"></param>
        /// <returns></returns>
        private IQueryable<T> AttachOrders(IQueryable<T> query, OrderExpression<T>[] orderCollection)
        {
            if (!orderCollection.HasItems())
            {
                return query;
            }

            for (int i = 0; i < orderCollection.Length; i++)
            {
                if (orderCollection[i].OrderColumn != null)
                {
                    query = orderCollection[i].OrderBy == OrderBy.ASC
                    ? query.OrderBy(orderCollection[i].OrderColumn)
                    : query.OrderByDescending(orderCollection[i].OrderColumn);
                }
                
            }

            return query;
        }
    }
}