/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         GridVisibleStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 响应式实用工具
** VersionInfo:
*********************************************************/

namespace GoldMantis.Web.HtmlControl.Styles
{
    /*
     *                      超小屏幕           小屏幕            中等屏幕          大屏幕
                        手机 (<768px)    平板 (≥768px)    桌面 (≥992px)    桌面 (≥1200px)
        .visible-xs-* 	    可见 	            隐藏 	        隐藏 	        隐藏
        .visible-sm-* 	    隐藏 	            可见 	        隐藏 	        隐藏
        .visible-md-* 	    隐藏 	            隐藏 	        可见 	        隐藏
        .visible-lg-* 	    隐藏 	            隐藏 	        隐藏 	        可见
        .hidden-xs 	        隐藏 	            可见 	        可见 	        可见
        .hidden-sm 	        可见 	            隐藏 	        可见 	        可见
        .hidden-md 	        可见 	            可见 	        隐藏 	        可见
        .hidden-lg 	        可见 	            可见 	        可见 	        隐藏

     */
    public class GridVisibleStyle
    {
        private static GridVisibleStyle _instance;

        public readonly string VisibleLg = "visible-lg";

        public readonly string VisibleLgBlock = "visible-lg-block";

        public readonly string VisibleLgInline = "visible-lg-inline";

        public readonly string VisibleLgInlineBlock = "visible-lg-inline-block";

        public readonly string VisibleMd = "visible-md";

        public readonly string VisibleMdBlock = "visible-md-block";

        public readonly string VisibleMdInline = "visible-md-inline";

        public readonly string VisibleMdInlineBlock = "visible-md-inline-block";

        public readonly string VisibleSm = "visible-sm";

        public readonly string VisibleSmBlock = "visible-sm-block";

        public readonly string VisibleSmInline = "visible-sm-inline";

        public readonly string VisibleSmInlineBlock = "visible-sm-inline-block";

        public readonly string VisibleXs = "visible-xs";

        public readonly string VisibleXsBlock = "visible-xs-block";

        public readonly string VisibleXsInline = "visible-xs-inline";

        public readonly string VisibleXsInlineBlock = "visible-xs-inline-block";

        public readonly string HiddenLg = "hidden-lg";

        public readonly string HiddenMd = "hidden-md";

        public readonly string HiddenSm = "hidden-sm";


        private GridVisibleStyle()
        {
        }

        static GridVisibleStyle()
        {
            _instance = new GridVisibleStyle();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static GridVisibleStyle Instance
        {
            get { return _instance; }
        }
    }
}