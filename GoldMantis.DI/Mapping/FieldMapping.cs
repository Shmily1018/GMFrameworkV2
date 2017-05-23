/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         FieldMapping.cs
** Creator:          Joe
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      字段存储器
** VersionInfo:
*********************************************************/
using System.Collections.Generic;
using System.Reflection;

namespace GoldMantis.DI
{
    public class FieldMapping
    {
        public static readonly List<FieldInfo> EmptyList = new List<FieldInfo>(); 
        private Dictionary<string, List<FieldInfo>> _mapping;
        private object _lockHelper;

        public FieldMapping()
        {
            _mapping = new Dictionary<string, List<FieldInfo>>();
            _lockHelper = new object();
        }

        public void Add(string key, List<FieldInfo> fields)
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

        public List<FieldInfo> this[string key]
        {
            get
            {
                List<FieldInfo> fields = null;
                _mapping.TryGetValue(key, out fields);

                return fields;
            }
        }
    }
}