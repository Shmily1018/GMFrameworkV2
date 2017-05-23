/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         TypesettingStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-20
** Changer:
** LastChangeDate:
** Description:      Bootstrap 排版
** VersionInfo:
*********************************************************/

namespace GoldMantis.Web.HtmlControl.Styles
{
    public class TypesettingStyle
    {
        private static TypesettingStyle _instance;

        // 使段落突出显示
        public readonly string Lead = "lead";

        // 设定小文本 (设置为父文本的 85% 大小)
        public readonly string Small = "small";

        // 显示在 <abbr> 元素中的文本以小号字体展示
        public readonly string Initialism = "initialism";

        // 设定引用右对齐
        public readonly string BlockquoteReverse = "blockquote-reverse";

        /*
         * 移除默认的列表样式，列表项中左对齐 ( <ul> 和 <ol> 中)。 
         * 这个类仅适用于直接子列表项 (如果需要移除嵌套的列表项，你需要在嵌套的列表中使用该样式)
         */
        public readonly string ListUnstyled = "list-unstyled";

        // 将所有列表项放置同一行
        public readonly string ListInline = "list-inline";

        // 该类设置了浮动和偏移，应用于 <dl> 元素和 <dt> 元素中，具体实现可以查看实例
        public readonly string DlHorizontal = "dl-horizontal";

        // 使 <pre> 元素可滚动 scrollable
        public readonly string PreScrollable = "pre-scrollable";

        // 元素浮动到左边
        public readonly string PullLeft = "pull-left";

        // 元素浮动到右边
        public readonly string PullRight = "pull-right";

        // 设置元素为 display:block 并居中显示
        public readonly string CenterBlock = "center-block";

        // 清除浮动
        public readonly string Clearfix = "clearfix";

        // 强制元素显示
        public readonly string Show = "show";

        // 强制元素隐藏
        public readonly string Hidden = "hidden";

        // 除了屏幕阅读器外，其他设备上隐藏元素
        public readonly string SrOnly = "sr-only";

        // 与 .sr-only 类结合使用，在元素获取焦点时显示(如：键盘操作的用户)
        public readonly string SrOnlyFocusable = "sr-only-focusable";

        // 显示关闭按钮
        public readonly string Close = "close";

        // 显示下拉式功能
        public readonly string Caret = "caret";


        static TypesettingStyle()
        {
            _instance = new TypesettingStyle();
        }

        private TypesettingStyle()
        {
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static TypesettingStyle Instance
        {
            get { return _instance; }
        }
    }
}