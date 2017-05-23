/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         LabelStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 标签
** VersionInfo:
*********************************************************/
namespace GoldMantis.Web.HtmlControl.Styles
{
    public class LabelStyle
    {
        private static LabelStyle _instance;

        public readonly string Label = "label";

        public readonly string LabelDanger = "label-danger";

        public readonly string LabelDefault = "label-default";

        public readonly string LabelIcon = "label-icon";

        public readonly string LabelImportant = "label-important";

        public readonly string LabelInfo = "label-info";

        public readonly string LabelInverse = "label-inverse";

        public readonly string LabelPrimary = "label-primary";

        public readonly string LabelSm = "label-sm";

        public readonly string LabelSuccess = "label-success";

        public readonly string LabelWarning = "label-warning";

        private LabelStyle()
        {
        }

        static LabelStyle()
        {
            _instance = new LabelStyle();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static LabelStyle Instance
        {
            get { return _instance; }
        }
    }
}