using System.Runtime.Serialization;

namespace GoldMantis.Entity
{
    [DataContract]
    public class SystemModule
    {
        [DataMember(Order = 0)]
        public virtual int ID { get; set; }

        [DataMember(Order = 1)]
        public virtual string Code { get; set; }

        [DataMember(Order = 2)]
        public virtual string Name { get; set; }

        [DataMember(Order = 3)]
        public virtual string Description { get; set; }

        [DataMember(Order = 4)]
        public virtual bool IsDelete { get; set; }

        [DataMember(Order = 5)]
        public virtual bool IsOn { get; set; }
    }
}