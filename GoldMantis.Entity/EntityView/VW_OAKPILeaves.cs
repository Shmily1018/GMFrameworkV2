using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using GoldMantis.Entity;


namespace GoldMantis.Entity
{


    [DataContract]
    public class VW_OAKPILeaves
    {

        [DataMember()]
        public virtual Int32 KeyID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String Code
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Int32 DeptId
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Int32 EmpId
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime BeginDate
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime EndDate
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Decimal Days
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Int32 LeaveType
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String Reason
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Boolean IsPost
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime PostDate
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Int32 CheckStatus
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Int32 MakerID
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
        public virtual Int32 WorkAgentID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String UserName
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String WorkAgentName
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String DeptName
        {
            get;
            set;
        }
    }
}
