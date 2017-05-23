using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Policy;

namespace GoldMantis.Entity
{
    [DataContract]
    public class Demo
    {
        [DataMember(Order = 0)]
        public virtual int ID { get; set; }

        [DataMember(Order = 1)]
        [Display(Name = "姓名")]
        [Required]
        [StringLength(10)]
        public virtual string Name { get; set; }

        [DataMember(Order = 2)]
        [Display(Name = "年龄")]
        [Required]
        public virtual int Age { get; set; }

        [DataMember(Order = 3)]
        public virtual int? CheckStatus { get; set; }

        [DataMember(Order = 4)]
        public virtual DateTime? PostDate { get; set; }

        [DataMember(Order = 5)]
        public virtual bool IsPost { get; set; }
    }
}