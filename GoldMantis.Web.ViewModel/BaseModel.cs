using System.Runtime.Serialization;
using System.Web.Mvc;
using GoldMantis.Common;

namespace GoldMantis.Web.ViewModel
{
    [DataContract]
    public abstract class BaseModel
    {
        //private TempDataDictionary _tempDataDictionary = new TempDataDictionary();

        /// <summary>
        /// 保存操作的临时信息
        /// </summary>
        [DataMember]
        public TempDataDictionary TempData { get; set; }
       
        /// <summary>
        /// 分页对象
        /// </summary>
        [DataMember]
        public PaginateAttribute PaginateHelperObject { get; set; }

        /// <summary>
        /// 上传附件对象
        /// </summary>
        [DataMember]
        public ModelFileUpload FileUploadHelperObject { get; set; }


        [DataMember]
        public EnumPageMode EnumPageMode { get; set; }

        [DataMember]
        public EnumExportType? ExportType
        {
            get;
            set;
        }
        
    }
}
