using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Entity;

namespace GoldMantis.Web.ViewModel
{
    /// <summary>
    /// 系统附件表
    /// </summary>
    [DataContract]
    public class Attachment
    {
        /// <summary>
        /// ID
        /// Length : 8
        /// </summary>
        [DataMember]
        public virtual System.Int32 ID
        {
            get; 
            set;
        }

        [DataMember]
        public int BillID
        {
            get;
            set;
        }

        [DataMember]
        public string TableName
        {
            get;
            set;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string Path
        {
            get;
            set;
        }

        [DataMember]
        public int FileSize
        {
            get;
            set;
        }

        [DataMember]
        public string Expand
        {
            get;
            set;
        }

        [DataMember]
        public string KeyID
        {
            get;
            set;
        }

    }


    /// <summary>
    /// 与Model有关，可能存在多个文件上传
    /// </summary>
    [DataContract]
    public class ModelFileUpload
    {
        [DataMember]
        public Guid FileKey { get; set; }

        [DataMember]
        public IList<Attachment> AttachmentList { get; set; }
        /// <summary>
        /// 是否只读
        /// Length : 1
        /// </summary>
        [Display(Name = "是否只读")]
        [DataMember]
        public virtual bool IsReadOnly { get; set; }
    }



    /// <summary>
    /// 与SessionManage有关，只存放关于文件服务器返回的相关信息
    /// 单次上传后返回的信息
    /// </summary>
    [Serializable]
    public class UploadedFile
    {
        public Guid FileKey { get; set; }

        /// <summary>
        /// 服务器文件信息
        /// </summary>
        public Attachment RemoteFileMessage { get; set; }

    }
}
