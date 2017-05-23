/*********************************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         CookieHelper.cs
** Creator:          Joe
** CreateDate:       2015-04-02
** Changer:
** LastChangeDate:
** Description:      HttpCookie的帮助类
** VersionInfo:
*********************************************************************/

using System;
using System.Web;

namespace GoldMantis.Common
{
    public static class CookieHelper
    {
        /// <summary>
        /// 获取HttpCookie
        /// </summary>
        /// <param name="cookieName">键名</param>
        /// <returns>HttpCookie</returns>
        public static HttpCookie GetCookie(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName];
        }

        /// <summary>
        /// 获取HttpCookie的值
        /// </summary>
        /// <param name="cookieName">键名</param>
        /// <returns>Cookie的值</returns>
        public static string GetCookieValue(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];

            return cookie.IsNull() ? null : cookie.Value;
        }

        /// <summary>
        /// 清除所有HttpCookie
        /// </summary>
        public static void ClearAll()
        {
            foreach (HttpCookie cookie in HttpContext.Current.Request.Cookies)
            {
                cookie.Expires = DateTime.Now.AddMinutes(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 清楚指定HttpContext
        /// </summary>
        /// <param name="cookieName">键名</param>
        public static void ClearCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];

            if (cookie.IsNotNull())
            {
                cookie.Expires = DateTime.Now.AddMinutes(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="name">Cookie键值</param>
        /// <param name="value">Cookie值</param>
        public static void SetCookie(string name, string value)
        {
            SetCookie(name, value, string.Empty, null);
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="name">Cookie键值</param>
        /// <param name="value">Cookie值</param>
        /// <param name="path">Cookie路径</param>
        public static void SetCookie(string name, string value, string path)
        {
            SetCookie(name, value, path, null);
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="name">Cookie键值</param>
        /// <param name="value">Cookie值</param>
        /// <param name="expires">Cookie过期时间</param>
        public static void SetCookie(string name, string value, DateTime expires)
        {
            SetCookie(name, value, string.Empty, expires);
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="name">Cookie键值</param>
        /// <param name="value">Cookie值</param>
        /// <param name="path">Cookie路径</param>
        /// <param name="expires">Cookie过期时间</param>
        public static void SetCookie(string name, string value, string path, DateTime? expires)
        {
            HttpCookie cookie = new HttpCookie(name, value);

            if (!path.IsNullOrEmptyOrWhiteSpace())
            {
                cookie.Path = path;
            }


            if (expires.HasValue)
            {
                cookie.Expires = expires.Value;
            }


            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
