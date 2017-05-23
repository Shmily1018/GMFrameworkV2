/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         FormStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-20
** Changer:
** LastChangeDate:
** Description:      Bootstrap 表单
** VersionInfo:
*********************************************************/

namespace GoldMantis.Web.HtmlControl.Styles
{
    public class FormStyle
    {
        private static FormStyle _instance;

        /**
         *  创建基本表单的步骤：

            向父 <form> 元素添加 role="form"。
            把标签和控件放在一个带有 class .form-group 的 <div> 中。这是获取最佳间距所必需的。
            向所有的文本元素 <input>、<textarea> 和 <select> 添加 class .form-control。

         */
        public readonly string Form = "form";

        /**
         * 创建一个表单，它的所有元素是内联的，向左对齐的，标签是并排的，请向 <form> 标签添加 class .form-inline。
         * 
         * 默认情况下，Bootstrap 中的 input、select 和 textarea 有 100% 宽度。在使用内联表单时，您需要在表单控件上设置一个宽度。
         * 使用 class .sr-only，您可以隐藏内联表单的标签。
         */
        public readonly string FormInline = "form-inline";

        /**
         *  创建一个水平布局的表单，请按下面的几个步骤进行：

            向父 <form> 元素添加 class .form-horizontal。
            把标签和控件放在一个带有 class .form-group 的 <div> 中。
            向标签添加 class .control-label。

         */
        public readonly string FormHorizontal = "form-horizontal";

        public readonly string FormGroup = "form-group";

        public readonly string FormControl = "form-control";

        // 需要在一个水平表单内的表单标签后放置纯文本时，请在 <p> 上使用 class .form-control-static
        public readonly string FormControlStatic = "form-control-static";


        /**
         * 控制表单高度
         */
        // 普通高度
        public readonly string InputSm = "input-sm";

        // 比较高
        public readonly string InputLg = "input-lg";

        /**
        * 控制表单宽度
        */
        // 大约0.7cell宽度
        public readonly string InputMini = "input-mini";

        // 大约1.3cell宽度
        public readonly string InputXSmall = "input-xsmall";

        // 大约2cell宽度
        public readonly string InputSmall = "input-small";

        // 大约3cell宽度
        public readonly string InputMedium = "input-medium";

        // 大约4cell宽度
        public readonly string InputLarge = "input-large";

        // 大约5cell宽度
        public readonly string InputXLarge = "input-xlarge";

        // 12cell宽度
        public readonly string InputXxLarge = "input-xxlarge";

        /**
        * 圆角
        */
        // 圆角
        public readonly string InputCircle = "input-circle";

        // 下圆角
        public readonly string InputCircleBottom = "input-circle-bottom";

        // 上圆角
        public readonly string InputCircleTop = "input-circle-top";

        // 左圆角
        public readonly string InputCircleLeft = "input-circle-left";

        // 右圆角
        public readonly string InputCircleRight = "input-circle-right";

        /**
         * 不知道啥效果
         */
        public readonly string InputAppend = "input-append";

        public readonly string InputCont = "input-cont";

        public readonly string InputBlockLevel = "input-block-level";

        public readonly string InputDaterange = "input-daterange";

        public readonly string InputField = "input-field";

        public readonly string InputFixed = "input-fixed";

        public readonly string InputIcon = "input-icon";

        public readonly string InputIconLg = "input-icon-lg";

        public readonly string InputIconSm = "input-icon-sm";

        public readonly string InputInline = "input-inline";

        public readonly string InputPrepend = "input-prepend";

        /**
         * 输入框组 input-group
         */
        public readonly string InputGroup = "input-group";

        public readonly string InputGroupAddon = "input-group-addon";

        public readonly string InputGroupBtn = "input-group-btn";

        public readonly string InputGroupBtnVertical = "input-group-btn-vertical";

        public readonly string InputGroupControl = "input-group-control";

        public readonly string InputGroupLg = "input-group-lg";

        public readonly string InputGroupSm = "input-group-sm";


        // <input> 后说明文本使用 .help-block，可以自动换行
        public readonly string HelpBlock = "help-block";

        public readonly string CheckboxInline = "checkbox-inline ";

        public readonly string RadioInline  = "radio-inline ";

        static FormStyle()
        {
            _instance = new FormStyle();
        }

        private FormStyle()
        {
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static FormStyle Instance
        {
            get { return _instance; }
        } 
    }
}