/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         RepositorySP.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类--存储过程操作
** VersionInfo:
*********************************************************/

using System.Collections.Generic;
using GoldMantis.Common;
using NHibernate;

namespace GoldMantis.Service.Dal
{
    /// <summary>
    /// 存储过程操作
    /// </summary>
    /// <typeparam name="T">返回的对象实体</typeparam>
    /// <typeparam name="P">查询参数</typeparam>
    public abstract class RepositorySP<T, P> : IRepository<T>, IRepositorySP<T, P>
        where T : class
        where P : class
    {
        /// <summary>
        /// NHibernate  Session
        /// </summary>
        public ISession Session
        {
            get { return NHibernateHelper.SessionFactory.GetCurrentSession(); }
        }

        /// <summary>
        /// 查询的结果集
        /// </summary>
        /// <returns>查询的结果集</returns>
        public abstract IList<T> Invoke();

        /// <summary>
        /// 根据参数查询的结果集
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>查询的结果集</returns>
        public abstract IList<T> Invoke(P param);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="param">参数</param>
        public abstract void Excute(P param);
    }
}