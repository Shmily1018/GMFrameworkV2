using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GoldMantis.Web.ViewModel.Home
{

    [DataContract]
    public class LogOnModel
    {
        [DataMember]
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}