/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         BaseTableBiz.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类--Table基类
** VersionInfo:
*********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GoldMantis.Common;
using GoldMantis.Service.Dal;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Type;

namespace GoldMantis.Service.Biz
{
    public abstract class BaseTableBiz<T> : BaseViewBiz<T> where T : class
    {
        /// <summary>
        /// DAL实例
        /// </summary>
        protected RepositoryTable<T> DataAccess
        {
            get { return Repository.As<RepositoryTable<T>>(); }
        }

        /// <summary>
        /// 保存纪录
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Save(T entity)
        {
            ITransaction tx = DataAccess.Session.BeginTransaction();

            using (tx)
            {
                DataAccess.Save(entity);
                tx.Commit();
            }
        }

        /// <summary>
        /// 更新纪录
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Update(T entity)
        {
            ITransaction tx = DataAccess.Session.BeginTransaction();

            using (tx)
            {
                DataAccess.Update(entity);
                tx.Commit();
            }
        }

        /// <summary>
        /// 保存或者更新数据纪录，如果T的主键不为空，则保存,为空，则更新
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void SaveOrUpdate(T entity)
        {
            ITransaction tx = DataAccess.Session.BeginTransaction();

            using (tx)
            {
                DataAccess.SaveOrUpdate(entity);
                tx.Commit();
            }
        }

        /// <summary>
        /// 删除纪录
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Delete(T entity)
        {
            ITransaction tx = DataAccess.Session.BeginTransaction();

            using (tx)
            {
                DataAccess.Delete(entity);
                tx.Commit();
            }
        }

        /// <summary>
        /// 根据Key进行删除
        /// </summary>
        /// <typeparam name="TInput">主键类型</typeparam>
        /// <param name="id">主键</param>
        /// <param name="nullableType"></param>
        public virtual void DeleteByKey<TInput>(TInput id, NullableType nullableType)
        {
            ITransaction tx = DataAccess.Session.BeginTransaction();

            using (tx)
            {
                DataAccess.DeleteByKey(id, nullableType);
                tx.Commit();
            }
        }

        /// <summary>
        /// 根据Keys进行删除
        /// </summary>
        /// <typeparam name="TInput">主键类型</typeparam>
        /// <param name="ids">主键数组</param>
        public virtual void DeleteByKeys<TInput>(TInput[] ids)
        {
            ITransaction tx = DataAccess.Session.BeginTransaction();

            using (tx)
            {
                DataAccess.DeleteByKeys(ids);
                tx.Commit();
            }
        }

        /// <summary>
        /// 根据条件进行批量删除
        /// </summary>
        /// <param name="criteria"></param>
        public virtual void DeleteBy(DetachedCriteria criteria)
        {
            ITransaction tx = DataAccess.Session.BeginTransaction();

            using (tx)
            {
                DataAccess.DeleteBy(criteria);
                tx.Commit();
            }

        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Merge(T entity)
        {
            ITransaction tx = DataAccess.Session.BeginTransaction();
            T t = default(T);
             
            using (tx)
            {
                t = DataAccess.Merge(entity);
                tx.Commit();
            }

            return t;
        }

        /// <summary>
        /// 持久化
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Persist(T entity)
        {
            ITransaction tx = DataAccess.Session.BeginTransaction();

            using (tx)
            {
                DataAccess.Persist(entity);
                tx.Commit();
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="direction"></param>
        ///// <returns></returns>
        //private OrderExpression<T>[] ConvertPathToLinqOrderArray(string path, string direction)
        //{
        //    var pathCollection = path.Split(new char[] {'#'}, StringSplitOptions.RemoveEmptyEntries);
        //    var directionCollection = direction.Split(new char[] {'#'}, StringSplitOptions.RemoveEmptyEntries);
        //    IList<OrderExpression<T>> linqOrderCollection = new List<OrderExpression<T>>();

        //    for (int i = 0; i < pathCollection.Count(); i++)
        //    {
        //        var tempExpression = ReflectionHelper.GetClassPropertyExpression<T>(pathCollection[i]);

        //        if (tempExpression.IsNull())
        //        {
        //            continue;
        //        }


        //        OrderBy tempOrder;

        //        if ((directionCollection.Length - 1) >= i)
        //        {
        //            if (directionCollection[i].Trim() != ((int) OrderBy.DESC).ToString() &&
        //                directionCollection[i].Trim().ToUpper() != ((int) OrderBy.ASC).ToString())
        //            {
        //                tempOrder = OrderBy.ASC;
        //            }
        //            else
        //            {
        //                tempOrder = (OrderBy) Convert.ToInt32(directionCollection[i]);
        //            }
        //        }
        //        else
        //        {
        //            tempOrder = OrderBy.ASC;
        //        }

        //        OrderExpression<T> tempLinqOrder = new OrderExpression<T>();
        //        tempLinqOrder.OrderColumn = tempExpression;
        //        tempLinqOrder.OrderBy = tempOrder;
        //        linqOrderCollection.Add(tempLinqOrder);
        //    }


        //    return linqOrderCollection.ToArray();
        //}
    }
}