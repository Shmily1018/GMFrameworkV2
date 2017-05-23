/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         ButtonStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 按钮
** VersionInfo:
*********************************************************/

namespace GoldMantis.Web.HtmlControl.Styles
{
    public class ButtonStyle
    {
        private static ButtonStyle _instance;

        // 为按钮添加基本样式
        public readonly string Btn = "btn";

        // 制作一个超小按钮
        public readonly string BtnXs = "btn-xs";

        //制作一个小按钮
        public readonly string BtnSm = "btn-sm";

        // 制作一个中等按钮
        public readonly string BtnMd = "btn-md";

        // 制作一个大按钮
        public readonly string BtnLg = "btn-lg";

        public readonly string BtnMini = "btn-mini";

        public readonly string BtnSmall = "btn-small";

        public readonly string BtnLarge = "btn-large";

        // 块级按钮(拉伸至父元素100%的宽度)
        public readonly string BtnBlock = "btn-block";

        // 按钮被点击
        public readonly string Active = "active";

        // 禁用按钮
        public readonly string Disabled = "disabled";

        // 默认/标准按钮
        public readonly string BtnDefault = "btn-default";

        // 原始按钮样式（未被操作） 
        public readonly string BtnPrimary = "btn-primary";

        // 表示成功的动作
        public readonly string BtnSuccess = "btn-success";

        // 该样式可用于要弹出信息的按钮
        public readonly string BtnInfo = "btn-info";

        // 表示需要谨慎操作的按钮
        public readonly string BtnWarning = "btn-warning";

        // 表示一个危险动作的按钮操作
        public readonly string BtnDanger = "btn-danger";

        // 让按钮看起来像个链接 (仍然保留按钮行为)
        public readonly string BtnLink = "btn-link";

        // 圆角
        public readonly string BtnCircle = "btn-circle";

        // 圆角
        public readonly string BtnCircleBottom = "btn-circle-bottom";

        // 圆角
        public readonly string BtnCircleLeft = "btn-circle-left";

        // 圆角
        public readonly string BtnCircleRight = "btn-circle-right";

        // 圆角
        public readonly string BtnCircleTop = "btn-circle-top";

        // 背景透明
        public readonly string BtnTransparent = "btn-transparent";

        public readonly string BtnArrowLink = "btn-arrow-link";

        public readonly string BtnArrowLinkLg = "btn-arrow-link-lg";

        public readonly string BtnBrdDanger = "btn-brd-danger";

        public readonly string BtnBrdPrimary = "btn-brd-primary";

        public readonly string BtnBrdWhite = "btn-brd-white";

        public readonly string BtnCont = "btn-cont";

        public readonly string BtnDashboardDaterange = "btn-dashboard-daterange";

        public readonly string BtnDefaultFocus = "btn-default-focus";

        public readonly string BtnFile = "btn-file";

        public readonly string BtnFitHeight = "btn-fit-height";

        public readonly string BtnIconOnly = "btn-icon-only";

        public readonly string BtnInverse = "btn-inverse";

        public readonly string BtnLeft = "btn-left";

        public readonly string BtnNavbar = "btn-navbar";

        public readonly string BtnRed = "btn-red";

        public readonly string BtnRight = "btn-right";

        public readonly string BtnSet = "btn-set";

        public readonly string BtnSubscribe = "btn-subscribe";

        public readonly string BtnThemePanel = "btn-theme-panel";

        public readonly string BtnToolbar = "btn-toolbar";

        public readonly string BtnClose = "btnClose";

        public readonly string BtnDisabled = "btnDisabled";

        public readonly string BtnNext = "btnNext";

        public readonly string BtnPlay = "btnPlay";

        public readonly string BtnPlayOn = "btnPlayOn";

        public readonly string BtnPrev = "btnPrev";

        public readonly string BtnToggle = "btnToggle";

        public readonly string BtnToggleOn = "btnToggleOn";

        /**
         *     Class	                                 描述	
         *     
            .btn-group	    该 class 用于形成基本的按钮组。在 .btn-group 中放置一系列带有 class .btn 的按钮。	

                <div class="btn-group">
                  <button type="button" class="btn btn-default">Button1</button>
                   <button type="button" class="btn btn-default">Button2</button>
                </div>

            .btn-toolbar	该 class 有助于把几组 <div class="btn-group"> 结合到一个 <div class="btn-toolbar"> 中，一般获得更复杂的组件。	

                <div class="btn-toolbar" role="toolbar">
                  <div class="btn-group">...</div>
                  <div class="btn-group">...</div>
                </div>

            .btn-group-lg, .btn-group-sm, .btn-group-xs	这些 class 可应用到整个按钮组的大小调整，而不需要对每个按钮进行大小调整。	

                <div class="btn-group btn-group-lg">...</div>
                <div class="btn-group btn-group-sm">...</div>
                <div class="btn-group btn-group-xs">...</div>

            .btn-group-vertical	该 class 让一组按钮垂直堆叠显示，而不是水平堆叠显示。 	

                <div class="btn-group-vertical">
                  ...
                </div>
         */

        public readonly string BtnGroup = "btn-group";

        public readonly string BtnGroupCircle = "btn-group-circle";

        public readonly string BtnGroupDevided = "btn-group-devided";

        public readonly string BtnGroupImg = "btn-group-img";

        public readonly string BtnGroupJustified = "btn-group-justified";

        public readonly string BtnGroupLg = "btn-group-lg";

        public readonly string BtnGroupNotification = "btn-group-notification";

        public readonly string BtnGroupRed = "btn-group-red";

        public readonly string BtnGroupSm = "btn-group-sm";

        public readonly string BtnGroupVertical = "btn-group-vertical";

        public readonly string BtnGroupVerticalCircle = "btn-group-vertical-circle";

        public readonly string BtnGroupXs = "btn-group-xs";

        static ButtonStyle()
        {
            _instance = new ButtonStyle();
        }

        private ButtonStyle()
        {
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static ButtonStyle Instance
        {
            get { return _instance; }
        }
    }
}