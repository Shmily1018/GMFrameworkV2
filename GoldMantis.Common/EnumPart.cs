using System.Runtime.Serialization;

namespace GoldMantis.Common
{
    [DataContract]
    public enum EnumPart
    {
        [EnumMember]
        AttachStringAndValue,
        [EnumMember]
        AttachStringAndText,
        [EnumMember]
        AttachInt,
        [EnumMember]
        AttachDecimal,
        [EnumMember]
        Text,
        [EnumMember]
        Value
    }
}