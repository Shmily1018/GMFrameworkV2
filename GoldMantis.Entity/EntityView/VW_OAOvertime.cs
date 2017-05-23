using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using GoldMantis.Entity;


namespace GoldMantis.Entity
{

    [DataContract]
    public class VW_OAOvertime
    {

        [DataMember()]
        public virtual Int64 KeyID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Int32 ID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String OvertimeCode
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String OvertimeReason
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime OvertimerDate
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String OvertimeUser
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String UserDept
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String Maker
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime MakeDate
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Boolean Ispost
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Int32 Checkstatus
        {
            get;
            set;
        }
    }
}
