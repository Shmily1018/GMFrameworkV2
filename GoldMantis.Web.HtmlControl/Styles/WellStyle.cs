/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         WellStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 一种会引起内容凹陷显示或插图效果的容器
** VersionInfo:
*********************************************************/
namespace GoldMantis.Web.HtmlControl.Styles
{
    public class WellStyle
    {
        private static WellStyle _instance;

        public readonly string Well = "well";

        public readonly string WellLarge = "well-large";

        public readonly string WellLg = "well-lg";

        public readonly string WellSm = "well-sm";

        public readonly string WellSmall = "well-small";

        private WellStyle()
        {
        }

        static WellStyle()
        {
            _instance = new WellStyle();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static WellStyle Instance
        {
            get { return _instance; }
        } 
    }
}