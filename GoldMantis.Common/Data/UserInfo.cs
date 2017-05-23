using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GoldMantis.Common
{
    [DataContract]
    [Serializable]
    public class UserInfo
    {
        [DataMember]
        public bool LoginResult
        {
            get;
            set;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public int UserID
        {
            get;
            private set;
        }

        /// <summary>
        /// 用户帐号
        /// </summary>
        [DataMember]
        public string Account
        {
            get;
            private set;
        }

        [DataMember]
        public string Password
        {
            get;
            set;
        }

        [DataMember]
        public bool CanLogin
        {
            get;
            set;
        }

        [DataMember]
        public bool IsOnJob
        {
            get;
            set;
        }

        [DataMember]
        public bool IsOn
        {
            get;
            set;
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [DataMember]
        public string UserName
        {
            get;
            private set;
        }

        [DataMember]
        public string Email
        {
            get;
            private set;
        }


        /// <summary>
        /// HR员工工号
        /// </summary>
        [DataMember]
        public string JobCode
        {
            get;
            set;
        }



        [DataMember]
        public bool IsSync
        {
            get;
            set;
        }

        [DataMember]
        public string CurrenUploadUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 确认银行账号无误
        /// </summary>
        [DataMember]
        public bool ConfirmAccountCorrect
        {
            get;
            set;
        }

        [DataMember]
        public int HrDeptID
        {
            get;
            set;
        }

        [DataMember]
        public string DisplayDepartMent { get; set; }


        [DataMember]
        public string DisplayPosition { get; set; }

        /// <summary>
        /// 显示登录名
        /// </summary>
        [DataMember]
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为模拟账户
        /// </summary>
        [DataMember]
        public bool IsImitation
        {
            get;
            set;
        }

    }
}
