using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.Mvc;
using GoldMantis.Entity;

namespace GoldMantis.Web.ViewModel.OA
{


    [DataContract]
    public class ModelSCBasicTypeCreate : BaseModel
    {
        /// <summary>
        /// 用户实体
        /// </summary>
        [DataMember]
        public SCBasicType SCBasicType { get; set; }

        [DataMember]
        public IList<SelectListItem> DataDisplayTypeList { get; set; }

    }
}