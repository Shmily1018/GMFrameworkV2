using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

using GoldMantis.Entity;

namespace GoldMantis.Web.ViewModel.OA
{
    /// <summary>
    /// 看房者信息管理
    /// </summary>
    [DataContract]
    public  class  ModelUTIPARKCUSTOMERVisitorCreate: BaseModel
    {
        /// <summary>
        /// 看房者信息管理
        /// </summary>
        [DataMember]
        public UTIPARKCUSTOMERVisitor UTIPARKCUSTOMERVisitor{get;set;}
        

        /// <summary>
        /// 看房者信息管理
        /// </summary>
        [DataMember]
        public IList<SelectListItem> BlockIDList{get;set;}
        

    }

    /// <summary>
    /// 看房者信息管理
    /// </summary>
    [DataContract]
    public  class  ModelUTIPARKCUSTOMERVisitorIndex: BaseModel
    {
        SearchUTIPARKCUSTOMERVisitor searchEntity=new SearchUTIPARKCUSTOMERVisitor();

        /// <summary>
        /// 查询实体
        /// </summary>
        [DataMember]
        public SearchUTIPARKCUSTOMERVisitor SearchEntity
        {
            get
            {
                return searchEntity;
            }
            set
            {
                searchEntity = value;
            }
        }

        /// <summary>
        /// UT_IPARK_CUSTOMER_Visitor
        /// </summary>
        [DataMember]
        public IList<UTIPARKCUSTOMERVisitor> GridDataSources {get;set;}
        

        /// <summary>
        /// 看房者信息管理
        /// </summary>
        [DataMember]
        public IList<SelectListItem> BlockIDList {get;set;} 
        

    }

    /// <summary>
    /// 看房者信息管理
    /// </summary>
    [DataContract]
    public class SearchUTIPARKCUSTOMERVisitor: BaseSearch
    {
        /// <summary>
        /// VisitorCode
        /// Length : 200
        /// </summary>
        [Display(Name="VisitorCode")]
        [StringLength(200)]
        [DataMember(Order = 0)]
        public virtual String  VisitorCode{get;set;}

        /// <summary>
        /// VisitorName
        /// Length : 200
        /// </summary>
        [Display(Name="VisitorName")]
        [StringLength(200)]
        [DataMember(Order = 1)]
        public virtual String  VisitorName{get;set;}

        /// <summary>
        /// Connector
        /// Length : 200
        /// </summary>
        [Display(Name="Connector")]
        [StringLength(200)]
        [DataMember(Order = 2)]
        public virtual String  Connector{get;set;}

        /// <summary>
        /// Telephone
        /// Length : 50
        /// </summary>
        [Display(Name="Telephone")]
        [StringLength(50)]
        [DataMember(Order = 3)]
        public virtual String  Telephone{get;set;}

        /// <summary>
        /// RentDateStart
        /// Length : 8
        /// </summary>
        [Display(Name="RentDateStart")]
        [DataMember(Order = 4)]
        public virtual System.Nullable<DateTime>  RentDateStart{get;set;}

        /// <summary>
        /// RentDateEnd
        /// Length : 8
        /// </summary>
        [Display(Name="RentDateEnd")]
        [DataMember(Order = 5)]
        public virtual System.Nullable<DateTime>  RentDateEnd{get;set;}

    }
}
