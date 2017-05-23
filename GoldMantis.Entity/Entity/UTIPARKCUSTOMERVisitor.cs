using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Runtime.Serialization; 
using System.Text; 

namespace GoldMantis.Entity
{
    /// <summary>
    /// 看房者信息管理
    /// </summary>
    [DataContract]
    public class UTIPARKCUSTOMERVisitor
    {
        /// <summary>
        /// 主键
        /// Length : 8
        /// </summary>
        [DataMember(Order = 0)]
        public virtual Int64	VisitorID{get;set;}
        
        /// <summary>
        /// 看房者编号
        /// Length : 200
        /// </summary>
        [Display(Name="看房者编号")]
        [StringLength(200)]
        [DataMember(Order = 1)]
        public virtual String VisitorCode{ get;set; }
        
        /// <summary>
        /// 看房者名称
        /// Length : 200
        /// </summary>
        [Required]
        [Display(Name="看房者名称")]
        [StringLength(200)]
        [DataMember(Order = 2)]
        public virtual String VisitorName{ get;set; }
        
        /// <summary>
        /// 联系人
        /// Length : 200
        /// </summary>
        [Display(Name="联系人")]
        [StringLength(200)]
        [DataMember(Order = 3)]
        public virtual String Connector{ get;set; }
        
        /// <summary>
        /// 联系电话
        /// Length : 50
        /// </summary>
        [Display(Name="联系电话")]
        [StringLength(50)]
        [DataMember(Order = 4)]
        public virtual String Telephone{ get;set; }
        
        /// <summary>
        /// 业态
        /// Length : 50
        /// </summary>
        [Display(Name="业态")]
        [StringLength(50)]
        [DataMember(Order = 5)]
        public virtual String BusinessStatus{ get;set; }
        
        /// <summary>
        /// 地块ID
        /// Length : 8
        /// </summary>
        [Required]
        [Display(Name="地块ID")]
        [DataMember(Order = 6)]
        public virtual Int64 BlockID{ get;set; }
        
        /// <summary>
        /// 开始租赁日期
        /// Length : 8
        /// </summary>
        [Display(Name="开始租赁日期")]
        [DataMember(Order = 7)]
        public virtual System.Nullable<DateTime> RentDateStart{ get;set; }
        
        /// <summary>
        /// 结束租赁日期
        /// Length : 8
        /// </summary>
        [Display(Name="结束租赁日期")]
        [DataMember(Order = 8)]
        public virtual System.Nullable<DateTime> RentDateEnd{ get;set; }
        
        /// <summary>
        /// 租赁价格
        /// Length : 13
        /// </summary>
        [Display(Name="租赁价格")]
        [DataMember(Order = 9)]
        public virtual System.Nullable<Decimal> RentPrice{ get;set; }
        
        /// <summary>
        /// 租赁面积
        /// Length : 13
        /// </summary>
        [Display(Name="租赁面积")]
        [DataMember(Order = 10)]
        public virtual System.Nullable<Decimal> RentArea{ get;set; }
        
        /// <summary>
        /// 备注
        /// Length : 500
        /// </summary>
        [Display(Name="备注")]
        [StringLength(500)]
        [DataMember(Order = 11)]
        public virtual String Remark{ get;set; }
        
        /// <summary>
        /// 添加人
        /// Length : 8
        /// </summary>
        [Display(Name="添加人")]
        [DataMember(Order = 12)]
        public virtual System.Nullable<Int64> AddPeople{ get;set; }
        
        /// <summary>
        /// 添加时间
        /// Length : 8
        /// </summary>
        [Display(Name="添加时间")]
        [DataMember(Order = 13)]
        public virtual System.Nullable<DateTime> AddDate{ get;set; }
        
        /// <summary>
        /// 更新人
        /// Length : 8
        /// </summary>
        [Display(Name="更新人")]
        [DataMember(Order = 14)]
        public virtual System.Nullable<Int64> UpdatePeople{ get;set; }
        
        /// <summary>
        /// 更新时间
        /// Length : 8
        /// </summary>
        [Display(Name="更新时间")]
        [DataMember(Order = 15)]
        public virtual System.Nullable<DateTime> UpdateDate{ get;set; }
        
        /// <summary>
        /// 逻辑删除标记
        /// Length : 1
        /// </summary>
        [Required]
        [Display(Name="逻辑删除标记")]
        [DataMember(Order = 16)]
        public virtual Boolean IsDeleted{ get;set; }
        
        /// <summary>
        /// 客户经理
        /// Length : 50
        /// </summary>
        [Display(Name="客户经理")]
        [StringLength(50)]
        [DataMember(Order = 17)]
        public virtual String Manager{ get;set; }

    }
}
