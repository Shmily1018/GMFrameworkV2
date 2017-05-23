using System.Runtime.Serialization;

namespace GoldMantis.Entity
{
    [DataContract]
    public class Operation
    {
        [DataMember(Order = 0)]
        public virtual int ID { get; set; }

        [DataMember(Order = 1)]
        public virtual string Name { get; set; }

        [DataMember(Order = 2)]
        public virtual string Description { get; set; }

        [DataMember(Order = 3)]
        public virtual bool IsDelete { get; set; }
    }
}