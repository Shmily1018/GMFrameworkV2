/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         TextStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 文本
** VersionInfo:
*********************************************************/

namespace GoldMantis.Web.HtmlControl.Styles
{
    public class TextStyle
    {
        private static TextStyle _instance;

        public readonly string Text = "text";

        // 设定文本左对齐
        public readonly string TextLeft = "text-left";

        // 设定文本居中对齐
        public readonly string TextCenter = "text-center";

        // 设定文本右对齐
        public readonly string TextRight = "text-right";

        // 设定文本对齐,段落中超出屏幕部分文字自动换行
        public readonly string TextJustify = "text-justify";

        // 段落中超出屏幕部分不换行
        public readonly string TextNowrap = "text-nowrap";

        // 设定文本小写
        public readonly string TextLowercase = "text-lowercase";

        // 设定文本大写
        public readonly string TextUppercase = "text-uppercase";

        // 设定单词首字母大写
        public readonly string TextCapitalize = "text-capitalize";

        // 相反的对齐
        public readonly string TextAlignReverse = "text-align-reverse";

        // 红色
        public readonly string TextDanger = "text-danger";

        // 默认半透明
        public readonly string TextDefault = "text-default";

        public readonly string TextEmp = "text-emp";

        public readonly string TextError = "text-error";

        // 不显示
        public readonly string TextHide = "text-hide";

        // 深蓝色
        public readonly string TextInfo = "text-info";

        public readonly string TextLogo = "text-logo";

        // 灰色
        public readonly string TextMuted = "text-muted";

        // 天蓝色
        public readonly string TextPrimary = "text-primary";

        public readonly string TextStat = "text-stat";

        // 绿色
        public readonly string TextSuccess = "text-success";

        // 橘黄色
        public readonly string TextWarning = "text-warning";

        static TextStyle()
        {
            _instance = new TextStyle();
        }

        private TextStyle()
        {
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static TextStyle Instance
        {
            get { return _instance; }
        }
    }
}