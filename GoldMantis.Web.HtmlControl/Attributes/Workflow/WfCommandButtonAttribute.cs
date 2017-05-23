/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         WfCommandButtonAttribute.cs
** Creator:          Joe
** CreateDate:       2015-11-10
** Changer:
** LastChangeDate:
** Description:      WorkFlow命令按钮属性
** VersionInfo:
*********************************************************/
using System;

namespace GoldMantis.Web.HtmlControl
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class WfCommandButtonAttribute : Attribute
    {
        /// <summary>
        /// 按钮name属性
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// 按钮id属性
        /// </summary>
        public string CommandClientID { get; set; }

        /// <summary>
        /// 按钮title属性
        /// </summary>
        public string CommandTitle { get; set; }

        /// <summary>
        /// 命令脚本
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// 自定义的class
        /// </summary>
        public string ExtendClass { get; set; }

        /// <summary>
        /// 按钮icon 的 class
        /// </summary>
        public string IconClass { get; set; }
    }
}