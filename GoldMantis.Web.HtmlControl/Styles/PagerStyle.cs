/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         PagerStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 翻页
** VersionInfo:
*********************************************************/
namespace GoldMantis.Web.HtmlControl.Styles
{
    /**
     * .pager	添加该 class 来获得翻页链接。	

            <ul class="pager">
              <li><a href="#">Previous</a></li>
              <li><a href="#">Next</a></li>
            </ul>

        .previous, .next	使用 class .previous 把链接向左对齐，使用 .next 把链接向右对齐。	

            <ul class="pager">
              <li class="previous"><a href="#">&larr; Older</a></li>
              <li class="next"><a href="#">Newer &rarr;</a></li>
            </ul>

        .disabled	添加该 class 来获得一个颜色变淡的外观。	

            <ul class="pager">
              <li class="previous disabled"><a href="#">&larr; Older</a></li>
              <li class="next"><a href="#">Newer &rarr;</a></li>
            </ul>
     */

    public class PagerStyle
    {
        private static PagerStyle _instance;

        public readonly string Pager = "pager";

        public readonly string Previous = "previous";

        public readonly string Next = "next";

        public readonly string Disabled = "disabled";

        private PagerStyle()
        {
        }

        static PagerStyle()
        {
            _instance = new PagerStyle();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static PagerStyle Instance
        {
            get { return _instance; }
        }
    }
}