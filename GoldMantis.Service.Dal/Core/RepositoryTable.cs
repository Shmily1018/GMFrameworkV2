/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         RepositoryTable.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类--Table基类
** VersionInfo:
*********************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using GoldMantis.Common;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Engine;
using NHibernate.Type;

namespace GoldMantis.Service.Dal
{
    public class RepositoryTable<T> : RepositoryView<T>, IRepositoryAudit<T> where T : class
    {
        protected string _keyId = "ID";

        public virtual string KeyID
        {
            get { return _keyId; }
            set { _keyId = value; }
        }

        /// <summary>
        /// 保存纪录
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Save(T entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// 更新纪录
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Update(T entity)
        {
            Session.Update(entity);
        }

        /// <summary>
        /// 保存或者更新数据纪录，如果T的主键不为空，则保存,为空，则更新
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void SaveOrUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);
        }

        /// <summary>
        /// 删除纪录
        /// </summary>
        /// <param name="entity">实体</param>
        public virtual void Delete(T entity)
        {
            Session.Delete(entity);
        }

        /// <summary>
        /// 根据ID进行删除
        /// </summary>
        /// <typeparam name="TInput">主键类型</typeparam>
        /// <param name="id">主键</param>
        /// <param name="nullableType"></param>
        public virtual void DeleteByKey<TInput>(TInput id, NullableType nullableType)
        {
            Session.Delete(string.Format(" from {0} t where t.{1}= ?", type.Name, KeyID), id, nullableType);
        }

        /// <summary>
        /// 根据IDs进行删除
        /// </summary>
        /// <typeparam name="TInput">主键类型</typeparam>
        /// <param name="ids">主键数组</param>
        public virtual void DeleteByKeys<TInput>(TInput[] ids)
        {
            string strids = string.Join(",", ids.ConvertAll(x => string.Format("'{0}'", x.ToString())));

            ISQLQuery query = Session.CreateSQLQuery(string.Format("delete from {0} where {2} in ({1})",
                    NHibernateHelper.NHConguration.GetClassMapping(type).Table.Name, strids, KeyID));

            query.ExecuteUpdate();

        }

        /// <summary>
        /// 根据条件进行批量删除
        /// </summary>
        /// <param name="criteria"></param>
        public virtual void DeleteBy(DetachedCriteria criteria)
        {
            criteria.SetProjection(Projections.Property(KeyID));
            long[] listGuids = criteria.GetExecutableCriteria(Session).List<long>().ToArray();
            string ids = string.Join(",", listGuids.ConvertAll(x => string.Format("'{0}'", x.ToString())));

            ISQLQuery query = Session.CreateSQLQuery(string.Format("delete from {0} where {2} in ({1})",
                    NHibernateHelper.NHConguration.GetClassMapping(type).Table.Name, ids, KeyID));

            query.ExecuteUpdate();

        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Merge(T entity)
        {
            return (T) Session.Merge(entity);
        }

        /// <summary>
        /// 持久化
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Persist(T entity)
        {
            Session.Persist(entity);
        }

        /// <summary>
        /// 保存重要字段的历次修改过程
        /// </summary>
        /// <param name="entity"></param>
        public void SaveKeyColumn(T entity)
        {
            Type operateLogAttributeType = typeof (OperatePropertyAttribute);
            string tableName = string.Empty;
            string keyColumnName = string.Empty;
            object[] operateLogAttributes = typeof (T).GetCustomAttributes(operateLogAttributeType, false);

            if (operateLogAttributes.Length != 0)
            {
                OperatePropertyAttribute operateLogAttribute = operateLogAttributes[0] as OperatePropertyAttribute;

                if (operateLogAttribute.IsNotNull())
                {
                    tableName = operateLogAttribute.TableName;
                    keyColumnName = operateLogAttribute.KeyColumnName;

                    NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration();
                    ISessionFactoryImplementor m_SessionFactoryImplementor = cfg.BuildSessionFactory().As<ISessionFactoryImplementor>();

                    IDbConnection m_Conn = m_SessionFactoryImplementor.ConnectionProvider.GetConnection();
                    NHibernate.SqlCommand.SqlString m_SqlString = new NHibernate.SqlCommand.SqlString("");
                    NHibernate.SqlTypes.SqlType[] arrTypes = new NHibernate.SqlTypes.SqlType[] {};
                    IDbCommand m_Cmd = m_SessionFactoryImplementor.ConnectionProvider.Driver.GenerateCommand(
                            CommandType.StoredProcedure, m_SqlString, arrTypes);
                    try
                    {
                        m_Cmd.CommandText = "UP_SYS_CreateOperateLog";
                        m_Cmd.Connection = m_Conn;

                        IDbDataParameter m_Param_1 = m_Cmd.CreateParameter();
                        m_Param_1.ParameterName = "@UserID";
                        m_Param_1.Value = "1";
                        m_Cmd.Parameters.Add(m_Param_1);

                        IDbDataParameter m_Param_2 = m_Cmd.CreateParameter();
                        m_Param_2.ParameterName = "@TableName";
                        m_Param_2.Value = tableName;
                        m_Cmd.Parameters.Add(m_Param_2);

                        IDbDataParameter m_Param_3 = m_Cmd.CreateParameter();
                        m_Param_3.ParameterName = "@KeyColumn";
                        m_Param_3.Value = keyColumnName;
                        m_Cmd.Parameters.Add(m_Param_3);



                        PropertyInfo propertyInfo = typeof (T).GetProperty(keyColumnName);
                        object propertyValue = propertyInfo.GetValue(entity, null);

                        IDbDataParameter m_Param_4 = m_Cmd.CreateParameter();
                        m_Param_4.ParameterName = "@KeyValue";
                        m_Param_4.Value = propertyValue;

                        m_Cmd.Parameters.Add(m_Param_4);

                        m_Cmd.ExecuteNonQuery();
                    }
                    finally
                    {
                        m_SessionFactoryImplementor.Close();
                    }
                }
            }
        }
    }
}