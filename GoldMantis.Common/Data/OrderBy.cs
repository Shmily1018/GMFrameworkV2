/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         OrderBy.cs
** Creator:          Joe
** CreateDate:       2015-03-27
** Changer:
** LastChangeDate:
** Description:      数据库排序
** VersionInfo:
*********************************************************/

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Serialize.Linq.Nodes;

namespace GoldMantis.Common
{
    public enum OrderBy
    {
        [EnumMember]
        ASC = 0,

        [EnumMember]
        DESC = 1
    }

    /// <summary>
    /// 排序
    /// </summary>
    [DataContract]
    public enum EnumOrder
    {
        [EnumMember]
        ASC = 0,
        [EnumMember]
        DESC = 1
    }

    [DataContract]
    public class LinqOrder<T>
    {
        [DataMember]
        public Expression<Func<T, object>> Path { get; set; }

        [DataMember]
        public EnumOrder Direction { get; set; }
    }

    [DataContract]
    public class LinqOrderSerialized<T>
    {
        [DataMember]
        public ExpressionNode Path { get; set; }

        [DataMember]
        public EnumOrder Direction { get; set; }
    }
}
