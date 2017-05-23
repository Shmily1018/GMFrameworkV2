/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         Bootstrap.cs
** Creator:          Joe
** CreateDate:       2015-08-20
** Changer:
** LastChangeDate:
** Description:      Bootstrap
** VersionInfo:
*********************************************************/

namespace GoldMantis.Web.HtmlControl.Styles
{
    public static class Bootstrap
    {
        /// <summary>
        /// Bootstrap 网格系统
        /// </summary>
        public static GridStyle Grid
        {
            get { return GridStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 排版
        /// </summary>
        public static TypesettingStyle Typesetting
        {
            get { return TypesettingStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 表格
        /// </summary>
        public static TableStyle Table
        {
            get { return TableStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 表单
        /// </summary>
        public static FormStyle Form
        {
            get { return FormStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 按钮
        /// </summary>
        public static ButtonStyle Button
        {
            get { return ButtonStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 图片
        /// </summary>
        public static ImageStyle Image
        {
            get { return ImageStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 文本
        /// </summary>
        public static TextStyle Text
        {
            get { return TextStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 背景
        /// </summary>
        public static BackgroundStyle Background
        {
            get { return BackgroundStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 响应式实用工具
        /// </summary>
        public static GridVisibleStyle GridVisible
        {
            get { return GridVisibleStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 字体图标
        /// </summary>
        public static GlyphiconStyle Glyphicon
        {
            get { return GlyphiconStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 下拉菜单
        /// </summary>
        public static DropdownStyle Dropdown
        {
            get { return DropdownStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 分页
        /// </summary>
        public static PaginationStyle Pagination
        {
            get { return PaginationStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 翻页
        /// </summary>
        public static PagerStyle Pager
        {
            get { return PagerStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 标签
        /// </summary>
        public static LabelStyle Label
        {
            get { return LabelStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 标签
        /// </summary>
        public static BadgeStyle Badge
        {
            get { return BadgeStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 警告
        /// </summary>
        public static AlertStyle Alert
        {
            get { return AlertStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 进度条
        /// </summary>
        public static ProgressStyle Progress
        {
            get { return ProgressStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 列表组
        /// </summary>
        public static ListGroupStyle ListGroup
        {
            get { return ListGroupStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 面板
        /// </summary>
        public static PanelStyle Panel
        {
            get { return PanelStyle.Instance; }
        }

        /// <summary>
        /// Bootstrap 一种会引起内容凹陷显示或插图效果的容器
        /// </summary>
        public static WellStyle Well
        {
            get { return WellStyle.Instance; }
        }
    }
}