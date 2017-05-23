using System.Runtime.Serialization;

namespace GoldMantis.Entity
{
    [DataContract]
    public class MenuOperation
    {
        [DataMember(Order = 0)]
        public virtual int ID { get; set; }

        [DataMember(Order = 1)]
        public virtual int MenuID { get; set; }

        [DataMember(Order = 2)]
        public virtual int OperationID { get; set; }
    }
}