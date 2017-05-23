using System.Runtime.Serialization;

namespace GoldMantis.Entity
{
    [DataContract]
    public class RoleMenu
    {
        [DataMember(Order = 0)]
        public virtual int ID { get; set; }

        [DataMember(Order = 1)]
        public virtual int RoleID { get; set; }

        [DataMember(Order = 2)]
        public virtual int MenuOperationID { get; set; }
    }
}