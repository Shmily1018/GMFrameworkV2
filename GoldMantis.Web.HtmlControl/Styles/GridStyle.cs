/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         GridStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-20
** Changer:
** LastChangeDate:
** Description:      Bootstrap 网格系统
** VersionInfo:
*********************************************************/

using System;

namespace GoldMantis.Web.HtmlControl.Styles
{
    /*
     *  Bootstrap 网格系统（Grid System）的工作原理

        网格系统通过一系列包含内容的行和列来创建页面布局。下面列出了 Bootstrap 网格系统是如何工作的：

        行必须放置在 .container class 内，以便获得适当的对齐（alignment）和内边距（padding）。
        使用行来创建列的水平组。
        内容应该放置在列内，且唯有列可以是行的直接子元素。
        预定义的网格类，比如 .row 和 .col-xs-4，可用于快速创建网格布局。LESS 混合类可用于更多语义布局。
        列通过内边距（padding）来创建列内容之间的间隙。该内边距是通过 .rows 上的外边距（margin）取负，表示第一列和最后一列的行偏移。
        网格系统是通过指定您想要横跨的十二个可用的列来创建的。例如，要创建三个相等的列，则使用三个 .col-xs-4。

     */
    public class GridStyle
    {
        private static GridStyle _instance;

        // 行样式
        public readonly string Row = "row";

        // 超小设备手机 样式前缀
        private readonly string _xsPrefix = "col-xs-";

        // 小型设备平板电脑 样式前缀
        private readonly string _smPrefix = "col-sm-";

        // 中型设备台式电脑 样式前缀
        private readonly string _mdPrefix = "col-md-";

        // 大型设备台式电脑 样式前缀
        private readonly string _lgPrefix = "col-lg-";

        // 左偏移 样式前缀
        private readonly string _offsetPrefix = "offset-";

        private GridStyle()
        {
        }

        static GridStyle()
        {
            _instance = new GridStyle();
        }

        /// <summary>
        /// 超小设备手机
        /// </summary>
        /// <param name="cellNumber">网格列数，取值范围1到12之间</param>
        /// <returns>样式</returns>
        public string ExtraSmall(int cellNumber)
        {
            ValidateCellNumber(cellNumber);

            return string.Format("{0}{1}", _xsPrefix, cellNumber);
        }

        /// <summary>
        /// 小型设备平板电脑
        /// </summary>
        /// <param name="cellNumber">网格列数，取值范围1到12之间</param>
        /// <returns>样式</returns>
        public string Small(int cellNumber)
        {
            ValidateCellNumber(cellNumber);

            return string.Format("{0}{1}", _smPrefix, cellNumber);
        }

        /// <summary>
        /// 中型设备台式电脑
        /// </summary>
        /// <param name="cellNumber">网格列数，取值范围1到12之间</param>
        /// <returns>样式</returns>
        public string Medium(int cellNumber)
        {
            ValidateCellNumber(cellNumber);

            return string.Format("{0}{1}", _mdPrefix, cellNumber);
        }

        /// <summary>
        /// 大型设备台式电脑
        /// </summary>
        /// <param name="cellNumber">网格列数，取值范围1到12之间</param>
        /// <returns>样式</returns>
        public string Large(int cellNumber)
        {
            ValidateCellNumber(cellNumber);

            return string.Format("{0}{1}", _lgPrefix, cellNumber);
        }

        /*
         * 偏移列

           偏移是一个用于更专业的布局的有用功能。它们可用来给列腾出更多的空间。
         * 例如，.col-xs=* 类不支持偏移，但是它们可以简单地通过使用一个空的单元格来实现该效果。

           为了在大屏幕显示器上使用偏移，请使用 .col-md-offset-* 类。
         * 这些类会把一个列的左外边距（margin）增加 * 列，其中 * 范围是从 1 到 11。
         */

        /// <summary>
        /// 超小设备手机（左偏移）
        /// </summary>
        /// <param name="cellNumber">网格列数，取值范围1到12之间</param>
        /// <param name="offset">左偏移网格列数，取值范围1到11之间</param>
        /// <returns>样式</returns>
        public string ExtraSmall(int cellNumber, int offset)
        {
            ValidateCellNumber(cellNumber);
            ValidateOffset(offset);

            return string.Format("{0}{1} {0}{2}{3}", _xsPrefix, cellNumber, _offsetPrefix, offset);
        }

        /// <summary>
        /// 小型设备平板电脑（左偏移）
        /// </summary>
        /// <param name="cellNumber">网格列数，取值范围1到12之间</param>
        /// <param name="offset">左偏移网格列数，取值范围1到11之间</param>
        /// <returns>样式</returns>
        public string Small(int cellNumber, int offset)
        {
            ValidateCellNumber(cellNumber);
            ValidateOffset(offset);

            return string.Format("{0}{1} {0}{2}{3}", _smPrefix, cellNumber, _offsetPrefix, offset);
        }

        /// <summary>
        /// 中型设备台式电脑（左偏移）
        /// </summary>
        /// <param name="cellNumber">网格列数，取值范围1到12之间</param>
        /// <param name="offset">左偏移网格列数，取值范围1到11之间</param>
        /// <returns>样式</returns>
        public string Medium(int cellNumber, int offset)
        {
            ValidateCellNumber(cellNumber);
            ValidateOffset(offset);

            return string.Format("{0}{1} {0}{2}{3}", _mdPrefix, cellNumber, _offsetPrefix, offset);
        }

        /// <summary>
        /// 大型设备台式电脑（左偏移）
        /// </summary>
        /// <param name="cellNumber">网格列数，取值范围1到12之间</param>
        /// <param name="offset">左偏移网格列数，取值范围1到11之间</param>
        /// <returns>样式</returns>
        public string Large(int cellNumber, int offset)
        {
            ValidateCellNumber(cellNumber);
            ValidateOffset(offset);

            return string.Format("{0}{1} {0}{2}{3}", _lgPrefix, cellNumber, _offsetPrefix, offset);
        }

        /// <summary>
        /// 验证网格列数
        /// </summary>
        /// <param name="cellNumber">网格列数</param>
        private void ValidateCellNumber(int cellNumber)
        {
            if (cellNumber < 1 || cellNumber > 12)
            {
                throw new ApplicationException("网格宽度超出范围");
            }
        }

        /// <summary>
        /// 验证左偏移网格列数
        /// </summary>
        /// <param name="offset">左偏移网格列数</param>
        private void ValidateOffset(int offset)
        {
            if (offset < 1 || offset > 11)
            {
                throw new ApplicationException("网格偏移量超出范围");
            }
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static GridStyle Instance
        {
            get { return _instance; }
        }
    }
}