/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         Repository.cs
** Creator:          Joe
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      对象仓储，T为仓储的对象类型
** VersionInfo:
*********************************************************/
using System.Collections.Generic;
using System.Reflection;
using GoldMantis.Common;

namespace GoldMantis.DI
{
    public class Repository<T> where T : class
    {
        // 键：T类型全名，值：T对象
        private Dictionary<string, T> _mapping;
        private object _lockHelper;

        public Repository()
        {
            _mapping = new Dictionary<string, T>();
            _lockHelper = new object();
        }

        public void Add(string key, T fields)
        {
            if (!_mapping.ContainsKey(key))
            {
                lock (_lockHelper)
                {
                    if (!_mapping.ContainsKey(key))
                    {
                        _mapping.Add(key, fields);
                    }
                }
            }
        }

        public T this[string key]
        {
            get
            {
                T value = null;
                _mapping.TryGetValue(key, out value);

                return value;
            }
        }

        public K Get<K>(string key) where K : class, T
        {
            return this[key].As<K>();
        }
    }
}