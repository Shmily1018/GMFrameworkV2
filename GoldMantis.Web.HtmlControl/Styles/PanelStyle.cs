/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         PanelStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 面板
** VersionInfo:
*********************************************************/
namespace GoldMantis.Web.HtmlControl.Styles
{
    /**
     * 创建一个基本的面板，只需要向 <div> 元素添加 class .panel 和 class .panel-default 即可
     */
    public class PanelStyle
    {
        private static PanelStyle _instance;

        public readonly string Panel = "panel";

        public readonly string PanelBody = "panel-body";

        public readonly string PanelCollapse = "panel-collapse";

        public readonly string PanelDanger = "panel-danger";

        public readonly string PanelDefault = "panel-default";

        public readonly string PanelFooter = "panel-footer";

        public readonly string PanelGroup = "panel-group";

        public readonly string PanelHeading = "panel-heading";

        public readonly string PanelInfo = "panel-info";

        public readonly string PanelPrimary = "panel-primary";

        public readonly string PanelSuccess = "panel-success";

        public readonly string PanelTitle = "panel-title";

        public readonly string PanelWarning = "panel-warning";

        private PanelStyle()
        {
        }

        static PanelStyle()
        {
            _instance = new PanelStyle();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static PanelStyle Instance
        {
            get { return _instance; }
        }
    }
}