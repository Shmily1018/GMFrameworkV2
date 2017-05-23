using System.Runtime.Serialization;

namespace GoldMantis.Entity
{
    [DataContract]
    public class Menu
    {
        [DataMember(Order = 0)]
        public virtual int ID { get; set; }

        [DataMember(Order = 1)]
        public virtual int ParentID { get; set; }

        [DataMember(Order = 2)]
        public virtual int ModuleID { get; set; }

        [DataMember(Order = 3)]
        public virtual string Name { get; set; }

        [DataMember(Order = 4)]
        public virtual string Area { get; set; }

        [DataMember(Order = 5)]
        public virtual string Controller { get; set; }

        [DataMember(Order = 6)]
        public virtual string Action { get; set; }

        [DataMember(Order = 7)]
        public virtual string IconClass { get; set; }

        [DataMember(Order = 8)]
        public virtual string MenuOrder { get; set; }

        [DataMember(Order = 9)]
        public virtual int MenuLevel { get; set; }

        [DataMember(Order = 10)]
        public virtual string NamePath { get; set; }

        [DataMember(Order = 11)]
        public virtual string KeyPath { get; set; }

        [DataMember(Order = 12)]
        public virtual string Description { get; set; }

        [DataMember(Order = 13)]
        public virtual bool IsDelete { get; set; }

        [DataMember(Order = 14)]
        public virtual bool IsOn { get; set; }
    }
}