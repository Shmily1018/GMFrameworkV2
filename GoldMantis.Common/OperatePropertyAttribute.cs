using System;

namespace GoldMantis.Common
{
    public class OperatePropertyAttribute : Attribute
    {
        /// <summary>
        /// 操作某个模块时，对应表的名称。
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 主键对应的列名
        /// </summary>
        public string KeyColumnName { get; set; }
    }
}