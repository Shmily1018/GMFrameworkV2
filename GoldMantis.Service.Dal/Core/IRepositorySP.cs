/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         IRepositorySP.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类--存储过程操作
** VersionInfo:
*********************************************************/

using System.Collections.Generic;

namespace GoldMantis.Service.Dal
{
    /// <summary>
    /// 存储过程操作
    /// </summary>
    /// <typeparam name="T">返回的对象实体</typeparam>
    /// <typeparam name="P">查询参数</typeparam>
    public interface IRepositorySP<T, P> 
        where T : class 
        where P : class
    {
        /// <summary>
        /// 查询的结果集
        /// </summary>
        /// <returns>查询的结果集</returns>
        IList<T> Invoke();

        /// <summary>
        /// 根据参数查询的结果集
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>查询的结果集</returns>
        IList<T> Invoke(P param);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="param">参数</param>
        void Excute(P param);
    }
}