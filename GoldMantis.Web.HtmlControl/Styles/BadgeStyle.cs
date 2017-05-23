/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         BadgeStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 徽章
** VersionInfo:
*********************************************************/
namespace GoldMantis.Web.HtmlControl.Styles
{
    public class BadgeStyle
    {
        private static BadgeStyle _instance;

        public readonly string Badge = "badge";

        private BadgeStyle()
        {
        }

        static BadgeStyle()
        {
            _instance = new BadgeStyle();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static BadgeStyle Instance
        {
            get { return _instance; }
        }
    }
}