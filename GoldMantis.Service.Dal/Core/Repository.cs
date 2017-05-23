/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         Repository.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类
** VersionInfo:
*********************************************************/

using GoldMantis.Common;

namespace GoldMantis.Service.Dal
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// NHibernate  Session
        /// </summary>
        public NHibernate.ISession Session
        {
            get { return NHibernateHelper.SessionFactory.GetCurrentSession(); }
        }
    }
}