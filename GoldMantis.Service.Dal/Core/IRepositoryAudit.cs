/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         IRepositoryAudit.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类--增删改操作
** VersionInfo:
*********************************************************/

using System.Collections.Generic;
using NHibernate.Criterion;
using NHibernate.Type;

namespace GoldMantis.Service.Dal
{
    public interface IRepositoryAudit<T> where T : class
    {
        /// <summary>
        /// HQL ：Hibernate Query Language
        /// 根据Hibernate查询语言查询
        /// </summary>
        /// <param name="hql">Hibernate查询语言</param>
        /// <returns>实体集合</returns>
        IList<T> FindByHql(string hql);

        /// <summary>
        /// 保存纪录
        /// </summary>
        /// <param name="entity">实体</param>
        void Save(T entity);

        /// <summary>
        /// 更新纪录
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(T entity);

        /// <summary>
        /// 保存或者更新数据纪录，如果T的主键不为空，则保存,为空，则更新
        /// </summary>
        /// <param name="entity">实体</param>
        void SaveOrUpdate(T model);

        /// <summary>
        /// 删除纪录
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete(T entity);

        /// <summary>
        /// 根据Key进行删除
        /// </summary>
        /// <typeparam name="TInput">主键类型</typeparam>
        /// <param name="id">主键</param>
        /// <param name="nullableType"></param>
        void DeleteByKey<TInput>(TInput id, NullableType nullableType);

        /// <summary>
        /// 根据Keys进行删除
        /// </summary>
        /// <typeparam name="TInput">主键类型</typeparam>
        /// <param name="ids">主键数组</param>
        void DeleteByKeys<TInput>(TInput[] ids);

        /// <summary>
        /// 根据条件进行批量删除
        /// </summary>
        /// <param name="criteria"></param>
        void DeleteBy(DetachedCriteria criteria);

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Merge(T entity);

        /// <summary>
        /// 持久化
        /// </summary>
        /// <param name="entity"></param>
        void Persist(T entity);

    }
}