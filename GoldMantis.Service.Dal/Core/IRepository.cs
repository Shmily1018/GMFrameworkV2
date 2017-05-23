/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         IRepository.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类
** VersionInfo:
*********************************************************/

using GoldMantis.Common;
using NHibernate;

namespace GoldMantis.Service.Dal
{
    public interface IRepository<T> : IDal where T : class
    {
        /// <summary>
        /// NHibernate  Session
        /// </summary>
        ISession Session { get; }
    }
}