using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GoldMantis.Web.ViewModel.SystemManage
{
    /// <summary>
    /// 列表页模型
    /// </summary>
    [DataContract]
    public class ModelImitationAccountIndex : BaseModel
    {
        [DataMember]
        [Required]
        [Display(Name = "模拟用户帐号")]
        public int ImitationAccountUserID { get; set; }

        [DataMember]
        public string ImitationAccountUserName { get; set; }
    }


}