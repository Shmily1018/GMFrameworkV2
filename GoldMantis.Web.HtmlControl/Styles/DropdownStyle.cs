/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         DropdownStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 下拉菜单
** VersionInfo:
*********************************************************/

namespace GoldMantis.Web.HtmlControl.Styles
{
    public class DropdownStyle
    {
        private static DropdownStyle _instance;

        public readonly  string DropdownToggle = "dropdown-toggle";

        public readonly string DropdownMenu = "dropdown-menu";

        // role属性，不是类
        public readonly string Role = "menu";

        // data-toggle 属性，不是类
        public readonly string DataToggle = "dropdown";

        private DropdownStyle()
        {
        }

        static DropdownStyle()
        {
            _instance = new DropdownStyle();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static DropdownStyle Instance
        {
            get { return _instance; }
        }
    }
}