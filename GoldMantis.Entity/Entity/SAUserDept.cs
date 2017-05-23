using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using GoldMantis.Entity;


namespace GoldMantis.Entity
{
    [DataContract]
    public class SAUserDept
    {

        [DataMember]
        public virtual Int32 ID
        {
            get;
            set;
        }

        [DataMember]
        public virtual int UserID
        {
            get;
            set;
        }

        [DataMember]
        public virtual int DeptID
        {
            get;
            set;
        }
    }
}
