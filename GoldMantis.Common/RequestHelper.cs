/*********************************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         RequestHelper.cs
** Creator:          Joe
** CreateDate:       2015-03-27
** Changer:
** LastChangeDate:
** Description:      HttpContext.Current.Request 请求信息的帮助类
** VersionInfo:
*********************************************************************/

using System;
using System.Web;

namespace GoldMantis.Common
{
    public static class RequestHelper
    {
        //浏览器列表
        private static readonly string[] BrowserList = new string[] { "ie", "chrome", "mozilla", "netscape", "firefox", "opera", "konqueror" };
        //搜索引擎列表
        private static readonly string[] SearchEngineList = new string[] { "baidu", "google", "360", "sogou", "bing", "msn", "sohu", "soso", "sina", "163", "yahoo", "jikeu" };


        #region 1.0 请求类型

        /// <summary>
        /// 获取请求类型
        /// </summary>
        /// <returns></returns>
        public static string GetHttpMethod()
        {
            return CurrentRequest.HttpMethod;
        }

        /// <summary>
        /// 是否是get请求
        /// </summary>
        /// <returns></returns>
        public static bool IsGet()
        {
            return GetHttpMethod().Equals("GET");
        }

        /// <summary>
        /// 是否是post请求
        /// </summary>
        /// <returns></returns>
        public static bool IsPost()
        {
            return GetHttpMethod().Equals("POST");
        }

        /// <summary>
        /// 是否是Ajax请求
        /// </summary>
        /// <returns></returns>
        public static bool IsAjax()
        {
            string header = CurrentRequest.Headers["X-Requested-With"];

            return !header.IsNullOrEmpty() && header.Equals("XMLHttpRequest");
        }

        #endregion

        #region 2.0 获取客户端的相关信息

        /// <summary>
        /// 获得上次请求的url
        /// </summary>
        /// <returns></returns>
        public static string GetUrlReferrer()
        {
            string uriStr = string.Empty;
            Uri uri = CurrentRequest.UrlReferrer;

            if (uri != null)
            {
                uriStr = uri.ToString();
            }


            return uriStr;
        }

        /// <summary>
        /// 获得请求的主机部分
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return CurrentRequest.Url.Host;
        }

        /// <summary>
        /// 获得请求的url
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            return CurrentRequest.Url.ToString();
        }

        /// <summary>
        /// 获得请求的原始url
        /// </summary>
        /// <returns></returns>
        public static string GetRawUrl()
        {
            return CurrentRequest.RawUrl;
        }

        /// <summary>
        /// 获取请求的虚拟路径
        /// </summary>
        /// <returns></returns>
        public static string GetVirtualPath()
        {
            return string.Format("~{0}", GetRawUrl());
        }

        /// <summary>
        /// 获得请求的ip
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string ip = CurrentRequest.ServerVariables["HTTP_VIA"] != null
                ? CurrentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"]
                : CurrentRequest.ServerVariables["REMOTE_ADDR"];

            if (ip.IsNullOrEmptyOrWhiteSpace() || !ValidateHelper.IsIP(ip))
            {
                ip = "127.0.0.1";
            }


            return ip;
        }

        /// <summary>
        /// 获得请求的浏览器类型
        /// </summary>
        /// <returns></returns>
        public static string GetBrowserType()
        {
            string type = CurrentRequest.Browser.Type;

            if (type.IsNullOrEmpty() || type.Equals("unknown"))
            {
                return ExceptionMessage.UnKnown;
            }


            return type.ToLower();
        }

        /// <summary>
        /// 获得请求的浏览器名称
        /// </summary>
        /// <returns></returns>
        public static string GetBrowserName()
        {
            string name = CurrentRequest.Browser.Browser;

            if (name.IsNullOrEmpty() || name.Equals("unknown"))
            {
                return ExceptionMessage.UnKnown;
            }


            return name.ToLower();
        }

        /// <summary>
        /// 获得请求的浏览器版本
        /// </summary>
        /// <returns></returns>
        public static string GetBrowserVersion()
        {
            string version = CurrentRequest.Browser.Version;

            if (version.IsNullOrEmpty() || version.Equals("unknown"))
            {
                return ExceptionMessage.UnKnown;
            }


            return version;
        }

        /// <summary>
        /// 获得请求客户端的操作系统类型
        /// </summary>
        /// <returns></returns>
        public static string GetOSType()
        {
            string type = ExceptionMessage.UnKnown;
            string userAgent = CurrentRequest.UserAgent;

            if (userAgent.Contains("NT 6.1"))
            {
                type = "Windows 7";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                type = "Windows XP";
            }
            else if (userAgent.Contains("NT 6.2"))
            {
                type = "Windows 8";
            }
            else if (userAgent.Contains("android"))
            {
                type = "Android";
            }
            else if (userAgent.Contains("iphone"))
            {
                type = "IPhone";
            }
            else if (userAgent.Contains("Mac"))
            {
                type = "Mac";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                type = "Windows Vista";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                type = "Windows 2003";
            }
            else if (userAgent.Contains("NT 5.0"))
            {
                type = "Windows 2000";
            }
            else if (userAgent.Contains("98"))
            {
                type = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                type = "Windows 95";
            }
            else if (userAgent.Contains("Me"))
            {
                type = "Windows Me";
            }
            else if (userAgent.Contains("NT 4"))
            {
                type = "Windows NT4";
            }
            else if (userAgent.Contains("Unix"))
            {
                type = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                type = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                type = "SunOS";
            }


            return type;
        }

        /// <summary>
        /// 获得请求客户端的操作系统名称
        /// </summary>
        /// <returns></returns>
        public static string GetOSName()
        {
            string name = CurrentRequest.Browser.Platform;

            if (name.IsNullOrEmpty())
            {
                return ExceptionMessage.UnKnown;
            }


            return name;
        }

        #endregion

        #region 3.0 获取请求平台

        /// <summary>
        /// 判断是否是浏览器请求
        /// </summary>
        /// <returns></returns>
        public static bool IsBrowser()
        {
            string name = GetBrowserName();

            foreach (string item in BrowserList)
            {
                if (name.Contains(item))
                {
                    return true;
                }
            }


            return false;
        }


        /// <summary>
        /// 是否是移动设备请求
        /// </summary>
        /// <returns></returns>
        public static bool IsMobile()
        {
            if (CurrentRequest.Browser.IsMobileDevice)
            {
                return true;
            }


            bool isTablet;
            bool.TryParse(CurrentRequest.Browser["IsTablet"], out isTablet);

            return isTablet;
        }


        /// <summary>
        /// 判断是否是搜索引擎爬虫请求
        /// </summary>
        /// <returns></returns>
        public static bool IsCrawler()
        {
            bool result = CurrentRequest.Browser.Crawler;

            if (!result)
            {
                string referrer = GetUrlReferrer();

                if (referrer.Length > 0)
                {
                    foreach (string item in SearchEngineList)
                    {
                        if (referrer.Contains(item))
                        {
                            return true;
                        }
                    }
                }
            }


            return result;
        }

        #endregion

        #region 4.0 从QueryString中获取值

        /// <summary>
        /// 获取string类型的QueryString
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public static string GetQueryString(string key)
        {
            return CurrentRequest.QueryString[key];
        }

        /// <summary>
        /// 获取int类型的QueryString
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="defaultValue">当不包含key的时候返回值</param>
        /// <returns></returns>
        public static int GetQueryStringInt(string key, int defaultValue = 0)
        {
            return GetQueryString(key).StringToDefaultInt32(defaultValue);
        }

        /// <summary>
        /// 获取int?类型的QueryString
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static int? GetQueryStringNullableInt(string key)
        {
            return GetQueryString(key).StringToNullableInt32();
        }

        /// <summary>
        /// 获取decimal类型的QueryString
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetQueryStringDecimal(string key, decimal defaultValue = 0)
        {
            return GetQueryString(key).StringToDefaultDecimal(defaultValue);
        }

        /// <summary>
        /// 获取decimal?类型的QueryString
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal? GetQueryStringNullableDecimal(string key)
        {
            return GetQueryString(key).StringToNullableDecimal();
        }

        /// <summary>
        /// 获取double类型的QueryString
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetQueryStringDouble(string key, double defaultValue = 0)
        {
            return GetQueryString(key).StringToDefaultDouble(defaultValue);
        }

        /// <summary>
        /// 获取double?类型的QueryString
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static double? GetQueryStringNullableDouble(string key)
        {
            return GetQueryString(key).StringToNullableDouble();
        }

        /// <summary>
        /// 获取bool类型的QueryString
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetQueryStringBoolean(string key, bool defaultValue = false)
        {
            return GetQueryString(key).StringToDefaultBoolean(defaultValue);
        }

        /// <summary>
        /// 获取bool?类型的QueryString
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool? GetQueryStringNullableBoolean(string key)
        {
            return GetQueryString(key).StringToNullableBoolean();
        }

        /// <summary>
        /// 获取DateTime类型的QueryString
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime GetQueryStringDateTime(string key)
        {
            return GetQueryString(key).StringToDateTime();
        }

        /// <summary>
        /// 获取DateTime?类型的QueryString
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime? GetQueryStringNullableDateTime(string key)
        {
            return GetQueryString(key).StringToNullableDateTime();
        }

        /// <summary>
        /// 获取Guid类型的QueryString
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Guid GetQueryStringGuid(string key)
        {
            return GetQueryString(key).StringToDefaultGuid(Guid.Empty);
        }


        /// <summary>
        /// 获取ID
        /// </summary>
        /// <returns></returns>
        public static int GetQueryStringID()
        {
            return GetQueryStringInt("ID");
        }

        #endregion

        /// <summary>
        /// 当前请求对象
        /// </summary>
        public static HttpRequest CurrentRequest
        {
            get { return HttpContext.Current.Request; }
        }
    }
}
