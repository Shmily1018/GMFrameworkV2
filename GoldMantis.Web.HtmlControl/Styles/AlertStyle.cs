/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         AlertStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 警告
** VersionInfo:
*********************************************************/
namespace GoldMantis.Web.HtmlControl.Styles
{
    public class AlertStyle
    {
        private static AlertStyle _instance;

        public readonly string Alert = "alert";

        public readonly string AlertBlock = "alert-block";

        public readonly string AlertBorderless = "alert-borderless";

        public readonly string AlertDanger = "alert-danger";

        public readonly string AlertDismissable = "alert-dismissable";

        public readonly string AlertDismissible = "alert-dismissible";

        public readonly string AlertError = "alert-error";

        public readonly string AlertInfo = "alert-info";

        public readonly string AlertLink = "alert-link";

        public readonly string AlertWarning = "alert-warning";

        public readonly string AlertSuccess = "alert-success";

        private AlertStyle()
        {
        }

        static AlertStyle()
        {
            _instance = new AlertStyle();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static AlertStyle Instance
        {
            get { return _instance; }
        }
    }
}