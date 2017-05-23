using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Framework;
using GoldMantis.Framework.CryptogramService;
using GoldMantis.Web.Core;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel;
using Newtonsoft.Json;

namespace GoldMantis.Web.Areas.UploadFileManage.Controllers
{


    public class UploadfileNewController : BaseController
    {

        #region 上传的全部代码

        [HttpPost]
        public ActionResult UploadFile()
        {
            try
            {
                return (ActionResult) this.Content(AddFile(this.Request.Form["FileKey"], this.Request.Files[0]));
            }
            catch
            {
                return (ActionResult) this.Content("上传失败");
            }
        }

        public string AddFile(string fileKey, HttpPostedFileBase fileData)
        {
            string fileName = fileData.FileName;

            FileInfo file = new FileInfo(fileName);
            string ext = file.Extension;
            Stream stream = fileData.InputStream;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            string baseFileString = Convert.ToBase64String(bytes);

            String remoteResult = GetUploadImgUrl(ext, fileName, fileData.ContentType, baseFileString);

            var attachment = JsonConvert.DeserializeObject<IList<Attachment>>(remoteResult).FirstOrDefault();
            if (attachment != null) attachment.Path = attachment.KeyID;


            SessionManager.UploadedFiles.Add(new UploadedFile()
            {
                FileKey = Guid.Parse(fileKey),
                RemoteFileMessage = attachment
            });
            return remoteResult;
        }

        /// <summary>
        /// 根据相应返回字符串
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public string ResponseToString(WebResponse response)
        {
            try
            {
                Encoding encoding = Encoding.Default;
                string ContentType = response.ContentType.Trim();
                if (ContentType.IndexOf("utf-8") != -1)
                    encoding = Encoding.UTF8;
                else if (ContentType.IndexOf("utf-7") != -1)
                    encoding = Encoding.UTF7;
                else if (ContentType.IndexOf("unicode") != -1)
                    encoding = Encoding.Unicode;

                StreamReader stream = new StreamReader(response.GetResponseStream(), encoding);
                string code = stream.ReadToEnd();
                stream.Close();
                response.Close();
                return code;
            }
            catch (Exception ce)
            {
                throw ce;
            }
        }

        public string GetUploadImgUrl(string fileExt, string fileName, string contentType, string fileString)
        {
            string result = string.Empty;
            try
            {
                string postData = string.Format("fileExt={0}&fileName={1}&contentType={2}&fileData={3}", fileExt,
                    HttpUtility.UrlEncode(fileName), contentType, fileString.Replace("+", "%2B"));
                result = ResponseToString(doPost(UploadfileConfiguration.FileUploadRemoteUrl, postData));
            }
            catch (Exception ex)
            {
                result = "err:" + ex.Message;
            }
            return result;
        }

        public WebResponse doPost(string url, string postData)
        {
            try
            {
                Thread.Sleep(1000);
                byte[] paramByte = Encoding.UTF8.GetBytes(postData); // 转化
                HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(url);

                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Referer = "http://bulo.hjenglish.com/app/app.aspx?aid=1040";
                webRequest.Accept =
                    "application/x-shockwave-flash, image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-silverlight, */*";
                webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; CIBA)";
                webRequest.ContentLength = paramByte.Length;
                webRequest.CookieContainer = new CookieContainer();
                //webRequest.Timeout = 5000;
                Stream newStream = webRequest.GetRequestStream();
                newStream.Write(paramByte, 0, paramByte.Length); //写入参数
                newStream.Close();
                return webRequest.GetResponse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion




        /// <summary>
        /// 获取归档文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FilePathResult GetFileByID(string path, string name)
        {
            string filePath =
                (UploadfileConfiguration.AttachmentDirectory + "\\" + path.Substring(0, path.LastIndexOf('\\'))).Replace
                    (@"\\", @"\");
            string downUrl = string.Format("{0}?Url={1})", UploadfileConfiguration.FileDownloadUrl,
                DESEncrypt.Encrypt(StringService.ConvertFilePathToUrl(path)));
            string localFileName = filePath + "\\" + path.Substring(path.LastIndexOf('\\') + 1);
            var downloadLoaclPath = path.Substring(0, path.LastIndexOf('\\')) + "\\" +
                                    path.Substring(path.LastIndexOf('\\') + 1);

            CheckDirectory(filePath);
            bool result = DownloadToLocal(downUrl, localFileName);
            if (result)
            {
                FileInfo fileInfo = new FileInfo(localFileName);
                return File(localFileName, GetContentTypeName(localFileName),
                    HttpUtility.UrlEncode(fileInfo.Name, System.Text.Encoding.UTF8));
            }
            else
            {
                return null;
            }
        }

        private static void CheckDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private bool DownloadToLocal(string url, string localPath)
        {
            bool flag = false;
            try
            {
                if (System.IO.File.Exists(localPath))
                {
                    return true;
                }
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                long totalBytes = resp.ContentLength;

                using (Stream sResp = resp.GetResponseStream())
                {
                    if (!Directory.Exists(localPath.Substring(0, localPath.LastIndexOf('\\'))))
                    {
                        Directory.CreateDirectory(localPath.Substring(0, localPath.LastIndexOf('\\')));
                    }

                    using (Stream sFile = new FileStream(localPath, FileMode.Create))
                    {
                        long totalDownloadBytes = 0;
                        byte[] bs = new byte[1024];
                        for (int size = sResp.Read(bs, 0, bs.Length); size > 0; size = sResp.Read(bs, 0, bs.Length))
                        {
                            totalDownloadBytes += size;
                            sFile.Write(bs, 0, size);
                        }
                    }
                }
                flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }


        /// <summary>
        /// 删除一个指定文件
        /// </summary>
        public ActionResult DeleteFile(string fileName)
        {
            var uploadFile = SessionManager.UploadedFiles.FirstOrDefault(x => x.RemoteFileMessage.KeyID.Replace(@"\\",@"\") == fileName);

            SessionManager.UploadedFiles.Remove(uploadFile);

            return Content("success");


        }

        /// <summary>
        /// 获取文件的类型
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        private string GetContentTypeName(string FileName)
        {
            string strExtendName = FileName.Substring(FileName.LastIndexOf('.'), FileName.Length - FileName.LastIndexOf('.'));

            string strContentType = "application/octet-stream";

            switch (strExtendName.ToLower().Trim())
            {
                case ".doc":
                case ".docx":
                    {
                        strContentType = "application/msword";
                        break;
                    }
                case ".xls":
                case ".xlsx":
                    {
                        strContentType = "application/x-excel";
                        break;
                    }
                case ".pdf":
                    {
                        strContentType = "application/pdf";
                        break;
                    }
                case ".ppt":
                    {
                        strContentType = "application/vnd.ms-powerpoint";
                        break;
                    }
                case ".jpeg":
                case ".jpg":
                    {
                        strContentType = "image/jpeg";
                        break;
                    }
                case ".gif":
                    {
                        strContentType = "image/gif";
                        break;
                    }
                case ".png":
                    {
                        strContentType = "image/png";
                        break;
                    }
                case ".bmp":
                    {
                        strContentType = "image/bmp";
                        break;
                    }
                case ".zip":
                    {
                        strContentType = "application/zip";
                        break;
                    }
            }

            return strContentType;
        }


    }
}