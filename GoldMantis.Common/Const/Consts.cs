using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldMantis.Common.Const
{
    /// <summary>
    /// 常量类
    /// </summary>
    public class Consts
    {
        public const int PAGE_SIZE = 10;
        public const string CONTENT_TYPE = "text/html;";

        /// <summary>
        /// 用户账户用到的Cookie(内网)
        /// </summary>
        public const string SYS_USER_COOKIE_KEY = "SYS_USER_COOKIE_KEY";

        /// <summary>
        /// 用户账户用到的Cookie(外网)
        /// </summary>
        public const string SYS_USER_OUT_COOKIE_KEY = "SYS_USER_OUT_COOKIE_KEY";

        public const string SYS_USER_SESSION_KEY = "SYS_USER_SESSION_KEY";

        public const string SAVE_AND_GENERATE = "保存后自动生成";

        /// <summary>
        /// 权限数据库链接
        /// </summary>
        public static readonly string PERMISSION_DB_LINK = ConfigurationManager.AppSettings["PermissionConnection"];
    }
}
