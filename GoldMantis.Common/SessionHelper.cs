/*********************************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         CookieHelper.cs
** Creator:          Joe
** CreateDate:       2015-04-03
** Changer:
** LastChangeDate:
** Description:      Session的帮助类
** VersionInfo:
*********************************************************************/

using System;
using System.Web;
using System.Web.SessionState;

namespace GoldMantis.Common
{
    public static class SessionHelper
    {
        /// <summary>
        /// 删除会话状态集合中的项
        /// </summary>
        /// <param name="name">要从会话状态集合中删除的项的名称</param>
        public static void Remove(string name)
        {
            CurrentSession.Remove(name);
        }

        /// <summary>
        /// 从会话状态集合中移除所有的键和值
        /// </summary>
        public static void RemoveAll()
        {
            CurrentSession.RemoveAll();
        }

        /// <summary>
        /// 向会话状态集合添加一个新项
        /// </summary>
        /// <param name="name">要添加到会话状态集合的项的名称</param>
        /// <param name="value">要添加到会话状态集合的项的值</param>
        public static void Add(string name, Object value)
        {
            //CurrentSession.Add(name, value);
            CurrentSession[name] = value;
        }

        /// <summary>
        /// 获取会话状态集合中的项
        /// </summary>
        /// <param name="name">要从会话状态集合中获取的项的名称</param>
        /// <returns>获取结果</returns>
        public static object Get(string name)
        {
            return CurrentSession[name];
        }

        /// <summary>
        /// 获取会话状态集合中的项
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="name">要从会话状态集合中获取的项的名称</param>
        /// <returns>获取结果</returns>
        public static T Get<T>(string name) where T : class
        {
            object value = Get(name);

            if (value.IsNull())
            {
                return null;
            }


            return value.As<T>();
        }

        /// <summary>
        /// 当前Session
        /// </summary>
        public static HttpSessionState CurrentSession
        {
            get { return HttpContext.Current.Session; }
        }
    }
}
