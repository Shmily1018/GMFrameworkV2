using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldMantis.Common
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class AttachDataAttribute : Attribute
    {
        public AttachDataAttribute(object key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        public object Key { get; private set; }

        public object Value { get; private set; }

        public static string GetAttachedData<T>(string memeberName, AttachDataKey key)
        {
            var type = typeof(T);
            var memInfos = type.GetMember(memeberName);
            List<AttachDataAttribute> attributes = 
                memInfos[0].GetCustomAttributes(typeof(AttachDataAttribute), false).Select(o => o.As<AttachDataAttribute>()).ToList();

            if (!attributes.HasItems())
            {
                return null;
            }


            foreach (AttachDataAttribute item in attributes)
            {
                if (item.Key.ToString() == key.ToString())
                {
                    return item.Value.ToString();
                }
            }


            return attributes[0].Value.ToString();
        }
    }

    public enum AttachDataKey
    {
        String,
        Int,
        Decimal
    }
}