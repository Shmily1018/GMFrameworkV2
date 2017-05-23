/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         TableStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-20
** Changer:
** LastChangeDate:
** Description:      Bootstrap 表格
** VersionInfo:
*********************************************************/

namespace GoldMantis.Web.HtmlControl.Styles
{
    public class TableStyle
    {
        private static TableStyle _instance;

        /*
         * 表格类
         */

        // 为任意 <table> 添加基本样式 (只有横向分隔线) 
        public readonly string Table = "table";

        // 在 <tbody> 内添加斑马线形式的条纹 ( IE8 不支持)
        public readonly string TableStriped = "table-striped";

        // 为所有表格的单元格添加边框    
        public readonly string TableBordered = "table-bordered";

        // 在 <tbody> 内的任一行启用鼠标悬停状态
        public readonly string TableHover = "table-hover";

        // 让表格更加紧凑
        public readonly string TableCondensed = "table-condensed";


        public readonly string TableActionsWrapper = "table-actions-wrapper";

        public readonly string TableAdvance = "table-advance";

        public readonly string TableCell = "table-cell";

        public readonly string TableCheckbox = "table-checkbox";

        public readonly string TableContainer = "table-container";

        public readonly string TableFullWidth = "table-full-width";

        public readonly string TableLight = "table-light";

        public readonly string TableResponsive = "table-responsive";

        public readonly string TableScrollable = "table-scrollable";

        public readonly string TableScrollableBorderless = "table-scrollable-borderless";

        public readonly string TableToolbar = "table-toolbar";

        public readonly string TableWrapperResponsive = "table-wrapper-responsive";

        /*
         * <tr>, <th> 和 <td> 类
         */

        // 将悬停的颜色应用在行或者单元格上
        public readonly string Active = "active";

        // 表示成功的操作
        public readonly string Success = "success";

        // 表示信息变化的操作
        public readonly string Info = "info";

        // 表示一个警告的操作
        public readonly string Warning = "warning";

        // 表示一个危险的操作
        public readonly string Danger = "danger";

        static TableStyle()
        {
            _instance = new TableStyle();
        }

        private TableStyle()
        {
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static TableStyle Instance
        {
            get { return _instance; }
        }
    }
}