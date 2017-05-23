/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         PaginationStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 分页
** VersionInfo:
*********************************************************/

namespace GoldMantis.Web.HtmlControl.Styles
{
    /**
     * .pagination	添加该 class 来在页面上显示分页。	

            <ul class="pagination">
              <li><a href="#">&laquo;</a></li>
              <li><a href="#">1</a></li>
              .......
            </ul>

        .disabled, .active	您可以自定义链接，通过使用 .disabled 来定义不可点击的链接，通过使用 .active 来指示当前的页面。	

            <ul class="pagination">
              <li class="disabled"><a href="#">&laquo;</a></li>
              <li class="active"><a href="#">1<span class="sr-only">(current)</span></a></li>
              .......
            </ul>

        .pagination-lg, .pagination-sm	使用这些 class 来获取不同大小的项。	

            <ul class="pagination pagination-lg">...</ul>
            <ul class="pagination">...</ul>
            <ul class="pagination pagination-sm">...</ul>
     */

    public class PaginationStyle
    {
        private static PaginationStyle _instance;

        public readonly string Pagination = "pagination";

        public readonly string Disabled = "disabled";

        public readonly string Active = "active";

        public readonly string PaginationLg = "pagination-lg";

        public readonly string PaginationSm = "pagination-sm";

        private PaginationStyle()
        {
        }

        static PaginationStyle()
        {
            _instance = new PaginationStyle();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static PaginationStyle Instance
        {
            get { return _instance; }
        }
    }
}