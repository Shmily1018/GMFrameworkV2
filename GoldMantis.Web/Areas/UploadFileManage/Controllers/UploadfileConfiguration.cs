using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.ViewModel;

namespace GoldMantis.Web.Areas.UploadFileManage.Controllers
{
    public class UploadfileConfiguration
    {

        /// <summary>
        /// 最终存放的文件夹目录
        /// </summary>
        public static string FileUploadRemoteUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["FileUploadRemoteUrl"];
            }
        }

        public static string AttachmentDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["AttachmentDirectory"];
            }
        }

        public static string FileDownloadUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["FileDownloadUrl"];
            }
        }

    }

}
