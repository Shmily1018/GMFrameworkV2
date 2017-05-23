using System; 
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text; 


namespace GoldMantis.Common
{
    [DataContract]
    public class SAMenu {
        
        [DataMember()]
        public virtual Int32 ID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual int SourceAppId
        {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String MenuName {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32 ParentID {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String MenuURL {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String MenuImage {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String BillNO {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int16 Sort {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String MenuPath {
            get;
            set;
        }
        
        [DataMember()]
        public virtual String Memo {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Boolean IsOn {
            get;
            set;
        }
        
        [DataMember()]
        public virtual Int32? OpenType {
            get;
            set;
        }
    }
}
