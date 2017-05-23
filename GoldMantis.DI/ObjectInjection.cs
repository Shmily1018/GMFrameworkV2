/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         ObjectInjection.cs
** Creator:          Joe
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      对象注入工具
** VersionInfo:
*********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using GoldMantis.Common;

namespace GoldMantis.DI
{
    public static class ObjectInjection
    {
        /// <summary>
        /// 注入对象
        /// </summary>
        /// <typeparam name="T">被注入对象的类型</typeparam>
        /// <typeparam name="K">被注入字段的类型</typeparam>
        /// <param name="source">被注入的对象</param>
        public static void Inject<T, K>(T source) 
            where T :class 
            where K : class 
        {
            if (source.IsNull())
            {
                return;
            }

            // 获取要注入的字段,当前类的字段
            FieldMapping mapping = GetFieldMapping<K>();

            if (mapping[source.GetType().FullName].IsNull())
            {
                string kFullName = typeof (K).FullName;
                List<FieldInfo> fs = source.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                        .Where(f => f.FieldType.GetInterface(kFullName).IsNotNull()).ToList();
                 
                mapping.Add(source.GetType().FullName, fs);
            }

            var fields = mapping[source.GetType().FullName];

            //添加到缓存列表，并从缓存列表取对象注入字段,
            Repository<K> repository = GetRepository<K>();

            foreach (FieldInfo field in fields)
            {
                
                if (repository[field.FieldType.FullName].IsNull())
                {
                    object value = field.FieldType.Assembly.CreateInstance(field.FieldType.FullName, false,
                        BindingFlags.Default | BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                        null, null, null, null);

                    
                    repository.Add(field.FieldType.FullName, value.As<K>());
                }

                field.SetValue(source, repository[field.FieldType.FullName]);
            }
        }

        private static FieldMapping GetFieldMapping<K>() where K : class
        {
            Type kType = typeof(K);
            if (kType == typeof(IDal))
            {
                return BizDalMapping.Instance;
            }
            else if (kType == typeof(IBiz))
            {
                return ServiceBizMapping.Instance;
            }
            else if (kType == typeof(IService))
            {
                return ControllerServiceMapping.Instance;
            }

            throw new ApplicationException("类型异常...");
        }

        private static Repository<K> GetRepository<K>() where K : class
        {
            Type kType = typeof (K);

            if (kType == typeof(IBiz))
            {
                return BizRepository.Instance.As<Repository<K>>();
            }
            else if (kType == typeof (IDal))
            {
                return DalRepository.Instance.As<Repository<K>>();
            }
            else
            {
                throw new ApplicationException("类型异常...");
            }
        }
    }
}