using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace GoldMantis.Common
{
    [DataContract]
    public class SCDepartment
    {

        [DataMember]
        public virtual Int32 ID
        {
            get;
            set;
        }

        [DataMember]
        public virtual String Code
        {
            get;
            set;
        }

        [DataMember]
        public virtual String DeptName
        {
            get;
            set;
        }

        [DataMember]
        public virtual Int32 ParentID
        {
            get;
            set;
        }

        [DataMember]
        public virtual int DeptTypeID { get; set; }

        [DataMember]
        public virtual int? ManagerID { get; set; }

        [DataMember]
        public virtual int? LeaderID { get; set; }

        [DataMember]
        public virtual Decimal PersonNumber
        {
            get;
            set;
        }

        [DataMember]
        public virtual String Tel
        {
            get;
            set;
        }

        [DataMember]
        public virtual String Address
        {
            get;
            set;
        }

        [DataMember]
        public virtual Boolean IsCostCenter
        {
            get;
            set;
        }

        [DataMember]
        public virtual String Memo
        {
            get;
            set;
        }

        [DataMember]
        public virtual Boolean IsOn
        {
            get;
            set;
        }

        [DataMember]
        public virtual String RootPath
        {
            get;
            set;
        }

        [DataMember]
        public virtual Int32 CompanyID
        {
            get;
            set;
        }

        [DataMember]
        public virtual String OrderCode
        {
            get;
            set;
        }

        [DataMember]
        public virtual String DeptShortName
        {
            get;
            set;
        }

        [DataMember]
        public virtual Int32 Area
        {
            get;
            set;
        }

        [DataMember]
        public virtual Double SortNum
        {
            get;
            set;
        }

        [DataMember]
        public virtual Int32 DeptLevels
        {
            get;
            set;
        }

        [DataMember]
        public virtual String HRID
        {
            get;
            set;
        }
    }
}
