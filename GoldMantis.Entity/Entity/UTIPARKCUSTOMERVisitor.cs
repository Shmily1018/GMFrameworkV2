using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 
using System.Runtime.Serialization; 
using System.Text; 

namespace GoldMantis.Entity
{
    /// <summary>
    /// ��������Ϣ����
    /// </summary>
    [DataContract]
    public class UTIPARKCUSTOMERVisitor
    {
        /// <summary>
        /// ����
        /// Length : 8
        /// </summary>
        [DataMember(Order = 0)]
        public virtual Int64	VisitorID{get;set;}
        
        /// <summary>
        /// �����߱��
        /// Length : 200
        /// </summary>
        [Display(Name="�����߱��")]
        [StringLength(200)]
        [DataMember(Order = 1)]
        public virtual String VisitorCode{ get;set; }
        
        /// <summary>
        /// ����������
        /// Length : 200
        /// </summary>
        [Required]
        [Display(Name="����������")]
        [StringLength(200)]
        [DataMember(Order = 2)]
        public virtual String VisitorName{ get;set; }
        
        /// <summary>
        /// ��ϵ��
        /// Length : 200
        /// </summary>
        [Display(Name="��ϵ��")]
        [StringLength(200)]
        [DataMember(Order = 3)]
        public virtual String Connector{ get;set; }
        
        /// <summary>
        /// ��ϵ�绰
        /// Length : 50
        /// </summary>
        [Display(Name="��ϵ�绰")]
        [StringLength(50)]
        [DataMember(Order = 4)]
        public virtual String Telephone{ get;set; }
        
        /// <summary>
        /// ҵ̬
        /// Length : 50
        /// </summary>
        [Display(Name="ҵ̬")]
        [StringLength(50)]
        [DataMember(Order = 5)]
        public virtual String BusinessStatus{ get;set; }
        
        /// <summary>
        /// �ؿ�ID
        /// Length : 8
        /// </summary>
        [Required]
        [Display(Name="�ؿ�ID")]
        [DataMember(Order = 6)]
        public virtual Int64 BlockID{ get;set; }
        
        /// <summary>
        /// ��ʼ��������
        /// Length : 8
        /// </summary>
        [Display(Name="��ʼ��������")]
        [DataMember(Order = 7)]
        public virtual System.Nullable<DateTime> RentDateStart{ get;set; }
        
        /// <summary>
        /// ������������
        /// Length : 8
        /// </summary>
        [Display(Name="������������")]
        [DataMember(Order = 8)]
        public virtual System.Nullable<DateTime> RentDateEnd{ get;set; }
        
        /// <summary>
        /// ���޼۸�
        /// Length : 13
        /// </summary>
        [Display(Name="���޼۸�")]
        [DataMember(Order = 9)]
        public virtual System.Nullable<Decimal> RentPrice{ get;set; }
        
        /// <summary>
        /// �������
        /// Length : 13
        /// </summary>
        [Display(Name="�������")]
        [DataMember(Order = 10)]
        public virtual System.Nullable<Decimal> RentArea{ get;set; }
        
        /// <summary>
        /// ��ע
        /// Length : 500
        /// </summary>
        [Display(Name="��ע")]
        [StringLength(500)]
        [DataMember(Order = 11)]
        public virtual String Remark{ get;set; }
        
        /// <summary>
        /// �����
        /// Length : 8
        /// </summary>
        [Display(Name="�����")]
        [DataMember(Order = 12)]
        public virtual System.Nullable<Int64> AddPeople{ get;set; }
        
        /// <summary>
        /// ���ʱ��
        /// Length : 8
        /// </summary>
        [Display(Name="���ʱ��")]
        [DataMember(Order = 13)]
        public virtual System.Nullable<DateTime> AddDate{ get;set; }
        
        /// <summary>
        /// ������
        /// Length : 8
        /// </summary>
        [Display(Name="������")]
        [DataMember(Order = 14)]
        public virtual System.Nullable<Int64> UpdatePeople{ get;set; }
        
        /// <summary>
        /// ����ʱ��
        /// Length : 8
        /// </summary>
        [Display(Name="����ʱ��")]
        [DataMember(Order = 15)]
        public virtual System.Nullable<DateTime> UpdateDate{ get;set; }
        
        /// <summary>
        /// �߼�ɾ�����
        /// Length : 1
        /// </summary>
        [Required]
        [Display(Name="�߼�ɾ�����")]
        [DataMember(Order = 16)]
        public virtual Boolean IsDeleted{ get;set; }
        
        /// <summary>
        /// �ͻ�����
        /// Length : 50
        /// </summary>
        [Display(Name="�ͻ�����")]
        [StringLength(50)]
        [DataMember(Order = 17)]
        public virtual String Manager{ get;set; }

    }
}
