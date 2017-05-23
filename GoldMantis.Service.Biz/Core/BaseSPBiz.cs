/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         BaseSPBiz.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类--存储过程操作
** VersionInfo:
*********************************************************/

using System.Collections.Generic;
using GoldMantis.Service.Dal;

namespace GoldMantis.Service.Biz
{
    public abstract class BaseSPBiz
    {

    }

    public abstract class BaseSPBiz<T, P> : BaseBiz<T>
        where T : class
        where P : class
    {
        /// <summary>
        /// 无用的属性
        /// </summary>
        protected override IRepository<T> Repository
        {
            get { return null; }
            set {  }
        }

        /// <summary>
        /// DAL实例
        /// </summary>
        protected abstract RepositorySP<T, P> DataAccess { get; set; }

        /// <summary>
        /// 查询的结果集
        /// </summary>
        /// <returns>查询的结果集</returns>
        public IList<T> Invoke()
        {
            return DataAccess.Invoke();
        }

        /// <summary>
        /// 根据参数查询的结果集
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>查询的结果集</returns>
        public IList<T> Invoke(P param)
        {
            return DataAccess.Invoke(param);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="param">参数</param>
        public void Excute(P param)
        {
            DataAccess.Excute(param);
        }
    }
}