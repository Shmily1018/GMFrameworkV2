using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldMantis.Common
{
    [DataContract]
    public class SCFlowMaping
    {
        [DataMember()]
        public virtual Int32 ID { get; set; }

        [DataMember()]
        public virtual Int32 MenuID { get; set; }

        [DataMember()]
        public virtual String Url { get; set; }

        [DataMember()]
        public virtual String WFName { get; set; }

        [DataMember()]
        public virtual Int32 DeptID { get; set; }

        [DataMember()]
        public virtual Guid WFPID { get; set; }

        [DataMember()]
        public virtual Int32 ParentID { get; set; }

        [DataMember()]
        public virtual Boolean AllowCustomFlow { get; set; }

        [DataMember()]
        public virtual Boolean IsOn { get; set; }

        [DataMember()]
        public virtual Boolean IsDelete { get; set; }
    }
}
