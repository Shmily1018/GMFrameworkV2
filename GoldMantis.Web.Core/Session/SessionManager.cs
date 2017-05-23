using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel;

namespace GoldMantis.Web.Core.Session
{
    public class SessionManager
    {
        private const string SESSION_KEY_UploadedFiles = "SESSION_KEY_UploadedFiles";
        private const string SESSION_KEY_USERPROFLE = "SESSION_KEY_USERPROFLE";

        public static UserInfo UserInfo
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[SESSION_KEY_USERPROFLE] != null)
                    return HttpContext.Current.Session[SESSION_KEY_USERPROFLE] as UserInfo;
                return (UserInfo)null;
            }
            set
            {
                HttpContext.Current.Session[SESSION_KEY_USERPROFLE] = value;
            }
        }

        public static IList<UploadedFile> UploadedFiles
        {
            get
            {
                if (HttpContext.Current.Session == null)
                    return null;
                if (HttpContext.Current.Session[SESSION_KEY_UploadedFiles] == null)
                    HttpContext.Current.Session[SESSION_KEY_UploadedFiles] = new List<UploadedFile>();
                return (HttpContext.Current.Session[SESSION_KEY_UploadedFiles] as List<UploadedFile>);
            }
            set
            {
                HttpContext.Current.Session[SESSION_KEY_UploadedFiles] = (object)value;
            }
        }


    }


    
}
