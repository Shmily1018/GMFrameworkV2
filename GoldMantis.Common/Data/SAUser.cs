using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace GoldMantis.Common
{


    [DataContract]
    public class SAUser
    {

        [DataMember()]
        public virtual Int32 ID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String LoginID
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String Password
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
        public virtual String Email
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String Mobile
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String SMobile
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String Telephone
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String JobCode
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String IdentityCard
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Int32 UserType
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime CreateTime
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Boolean IsOn
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime IsOnTime
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Boolean CanLogin
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime CanLoginTime
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Boolean IsOnJob
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime ExpireTime
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Boolean IsSync
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String ExtensionNumber
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String DirectNummer
        {
            get;
            set;
        }

        [DataMember()]
        public virtual String Fax
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Boolean IsSimple
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Int32 SaleInfoNumEnabled
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Int32 SortNo
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime AccDisabledTime
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Int32 CreateUser
        {
            get;
            set;
        }

        [DataMember()]
        public virtual DateTime LastUpdateDate
        {
            get;
            set;
        }

        [DataMember()]
        public virtual Boolean ConfirmAccountCorrect
        {
            get;
            set;
        }
    }

}
