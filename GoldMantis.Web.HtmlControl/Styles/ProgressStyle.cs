/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         ProgressStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 进度条
** VersionInfo:
*********************************************************/
namespace GoldMantis.Web.HtmlControl.Styles
{
    /**
     * 创建一个基本的进度条的步骤如下：

        添加一个带有 class .progress 的 <div>。
        接着，在上面的 <div> 内，添加一个带有 class .progress-bar 的空的 <div>。
        添加一个带有百分比表示的宽度的 style 属性，例如 style="60%"; 表示进度条在 60% 的位置。

     * 
     * 
     * 创建不同样式的进度条的步骤如下：

        添加一个带有 class .progress 的 <div>。
        接着，在上面的 <div> 内，添加一个带有 class .progress-bar 和 class progress-bar-* 的空的 <div>。其中，* 可以是 success、info、warning、danger。
        添加一个带有百分比表示的宽度的 style 属性，例如 style="60%"; 表示进度条在 60% 的位置。

     * 
     */

    public class ProgressStyle
    {
        private static ProgressStyle _instance;

        public readonly string Progress = "progress";

        public readonly string ProgressAnimated = "progress-animated";

        public readonly string ProgressDanger = "progress-danger";

        public readonly string ProgressExtended = "progress-extended";

        public readonly string ProgressInfo = "progress-info";

        public readonly string ProgressSm = "progress-sm";

        public readonly string ProgressStriped = "progress-striped";

        public readonly string ProgressSuccess = "progress-success";

        public readonly string ProgressWarning = "progress-warning";

        public readonly string ProgressBar = "progress-bar";

        public readonly string ProgressBarDanger = "progress-bar-danger";

        public readonly string ProgressBarDefault = "progress-bar-default";

        public readonly string ProgressBarInfo = "progress-bar-info";

        public readonly string ProgressBarStriped = "progress-bar-striped";

        public readonly string ProgressBarSuccess = "progress-bar-success";

        public readonly string ProgressBarWarning = "progress-bar-warning";

        // 属性。不是class
        public readonly string Role = "progressbar";
        public readonly string AriaValuenow = "aria-valuenow";
        public readonly string AriaValuemin = "aria-valuemin";
        public readonly string AriaValuemax = "aria-valuemax";

        private ProgressStyle()
        {
        }

        static ProgressStyle()
        {
            _instance = new ProgressStyle();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static ProgressStyle Instance
        {
            get { return _instance; }
        }
    }
}