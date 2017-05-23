using System; 
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text; 
using GoldMantis.Entity;


namespace GoldMantis.Entity {
    
    
    [DataContract]
    public class VW_SCBasicType {
        
        [DataMember()]
        public virtual Int64 KeyID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 TypeID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String TypeCode {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String TypeName {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Memo {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 ParentID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 OrderID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean Enabled {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 CreateUserID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime CreateTime {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 UpdateUserID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual DateTime UpdateTime {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Attribute1 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Attribute4 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Attribute2 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Attribute3 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Attribute5 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Attribute7 {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String UserName {
            get;
            set;
        }

         [DataMember()]
        public virtual string state { get; set; }
    }
}
