using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace GoldMantis.Entity
{
    [DataContract]
    public class SCBasicType
    {
        [DataMember]
        public virtual int TypeID
        {
            get;
            set;
        }
        [DataMember]
        [StringLength(256)]
        [Required]
        public virtual string TypeCode
        {
            get;
            set;
        }
        [DataMember]
        [Required]
        [StringLength(512)]
        public virtual string TypeName
        {
            get;
            set;
        }
        [DataMember]
        [StringLength(512)]
        public virtual string Memo
        {
            get;
            set;
        }
        [DataMember]
        public virtual int? ParentID
        {
            get;
            set;
        }
        [DataMember]
        public virtual int? OrderID
        {
            get;
            set;
        }

        [Required]
        [DataMember]
        public virtual bool Enabled
        {
            get;
            set;
        }
        [DataMember]
        [Required]
        public virtual int CreateUserID
        {
            get;
            set;
        }
        [DataMember]
        [Required]
        public virtual DateTime CreateTime
        {
            get;
            set;
        }
        [DataMember]
        public virtual int? UpdateUserID
        {
            get;
            set;
        }
        [DataMember]
        public virtual DateTime? UpdateTime
        {
            get;
            set;
        }
        [DataMember]
        [StringLength(50)]
        public virtual string Attribute1
        {
            get;
            set;
        }
        [DataMember]
        [StringLength(50)]
        public virtual string Attribute4
        {
            get;
            set;
        }
        [DataMember]
        [StringLength(50)]
        public virtual string Attribute2
        {
            get;
            set;
        }
        [DataMember]
        [StringLength(50)]
        public virtual string Attribute3
        {
            get;
            set;
        }
        [DataMember]
        [StringLength(50)]
        public virtual string Attribute5
        {
            get;
            set;
        }
        [DataMember]
        [StringLength(50)]
        public virtual string Attribute6
        {
            get;
            set;
        }
        [DataMember]
        [StringLength(50)]
        public virtual string Attribute7
        {
            get;
            set;
        }
        [DataMember]
        public virtual int? RadTypeID
        {
            get;
            set;
        }
        [DataMember]
        public virtual int? DataDisplayType
        {
            get;
            set;
        }
    }
}
