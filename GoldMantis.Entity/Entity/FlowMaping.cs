using System;
using System.Runtime.Serialization;

namespace GoldMantis.Entity
{
    [DataContract]
    public class FlowMaping
    {
        [DataMember(Order = 0)]
        public virtual int ID { get; set; }

        [DataMember(Order = 1)]
        public virtual int MenuID { get; set; }

         [DataMember(Order = 2)]
        public virtual string Url { get; set; }

        [DataMember(Order = 3)]
        public virtual string WFName { get; set; }

        [DataMember(Order = 4)]
        public virtual int? DeptID { get; set; }

        [DataMember(Order = 5)]
        public virtual Guid WFPID { get; set; }

        [DataMember(Order = 6)]
        public virtual int? ParentID { get; set; }

        [DataMember(Order = 7)]
        public virtual bool AllowCustomFlow { get; set; }

        [DataMember(Order = 8)]
        public virtual bool IsOn { get; set; }

        [DataMember(Order = 9)]
        public virtual bool IsDelete { get; set; }
    }
}