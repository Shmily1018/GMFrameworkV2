using System.Text;
using System.Web.Mvc;
using GoldMantis.Web.ViewModel;

namespace GoldMantis.Web.HtmlControl
{
    public static class PagerExtensions
    {
        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, string name, PaginateAttribute attribute)
        {
            return MvcHtmlString.Create(htmlHelper.PagerString(name, attribute));
        }

        public static string PagerString(this HtmlHelper htmlHelper, string name, PaginateAttribute attribute)
        {
            StringBuilder pagerBuilder = new StringBuilder();

            pagerBuilder.AppendFormat("<div id='{0}_gmPagination_container' class='pagination-container' style='margin:0px;'>", name)
                .Append("<div class='paging' style='text-align: right; margin-top: 0px;'>")
                .AppendFormat("<ul class='pagination' style='visibility: visible; margin: 0px;' id='{0}_gmPagination' >", name)
                .Append("<li>")
                .Append("<span style='padding: 11px;'>")
                .AppendFormat("第{0}/{1}页", attribute.PageIndex + 1, attribute.TotalPages)
                .Append("</span></li>")
                .AppendFormat("<li class='prev{0}'>", attribute.PageIndex + 1 != 1 ? string.Empty : " disabled")
                .AppendFormat("<a class='page-first' href='{0}{1}=0' title='第一页' style='padding: 11px;' id='{2}_gmPagination_first' onclick='return GmUIControl.PagerControl(\"{2}\").navigation(this);'>", attribute.Url, attribute.PageIndexName, name)
                .Append("<i class='fa fa-angle-double-left'></i></a></li>")
                .AppendFormat("<li class='prev{0}'>", attribute.PageIndex != 0 ? string.Empty : " disabled")
                .AppendFormat("<a class='page-prev' href='{0}{1}={2}' title='上一页' style='padding: 11px;' id='{3}_gmPagination_prev' onclick='return GmUIControl.PagerControl(\"{3}\").navigation(this);'>", attribute.Url, attribute.PageIndexName, attribute.PageIndex - 1, name)
                .Append("<i class='fa fa-angle-left'></i></a></li>")
                .AppendFormat("<li class='next{0}'>", attribute.PageIndex < (attribute.TotalPages - 1) ? string.Empty : " disabled")
                .AppendFormat("<a class='page-next' href='{0}{1}={2}' title='下一页' style='padding: 11px;' id='{3}_gmPagination_next' onclick='return GmUIControl.PagerControl(\"{3}\").navigation(this);'>", attribute.Url, attribute.PageIndexName, attribute.PageIndex + 1, name)
                .Append("<i class='fa fa-angle-right'></i></a></li>")
                .AppendFormat("<li class='next{0}'>", (attribute.PageIndex + 1) < attribute.TotalPages ? string.Empty : " disabled")
                .AppendFormat("<a class='page-last' href='{0}{1}={2}' title='最后一页' style='padding: 11px;' id='{3}_gmPagination_last' onclick='return GmUIControl.PagerControl(\"{3}\").navigation(this);'>", attribute.Url, attribute.PageIndexName, attribute.TotalPages, name)
                .Append("<i class='fa fa-angle-double-right'></i></a></li><li>")
                .AppendFormat("<input type='hidden' name='{0}' id='{1}_gmPagination_pageIndex' value='{2}' />", attribute.PageIndexName, name, attribute.PageIndex)
                .Append("<span class='pageNum'>")
                .AppendFormat("共 {0} 条&nbsp;&nbsp;&nbsp;&nbsp;每页 ", attribute.TotalCount)
                .AppendFormat("<select title='每页显示多少条' name='{0}' id='pageSize' onchange='GmUIControl.PagerControl(\"{1}\").pageSizeChange();' class='form-control input-sm input-inline' id='{1}_gmPagination_pageSize'>", attribute.PageSizeName, name)
                .AppendFormat("<option value='10'{0}>10</option>", attribute.PageSize == 10?" selected":string.Empty)
                .AppendFormat("<option value='20'{0}>20</option>", attribute.PageSize == 20 ? " selected" : string.Empty)
                .AppendFormat("<option value='50'{0}>50</option>", attribute.PageSize == 50 ? " selected" : string.Empty)
                .AppendFormat("<option value='100'{0}>100</option>", attribute.PageSize == 100 ? " selected" : string.Empty)
                .AppendFormat("<option value='200'{0}>200</option>", attribute.PageSize == 200 ? " selected" : string.Empty)
                .Append("</select> 条&nbsp;&nbsp;第 ")
                .AppendFormat("<input type='text' style='width: 30px;' maxlength='4' value='{0}' id='{1}_gmPagination_jumpto' class='jumpto' name='pageIndexShow' title='指定页码' onkeypress='GmUIControl.PagerControl(\"{1}\").keypress(event)' />", attribute.PageIndex + 1, name)
                .AppendFormat(" 页 <input type='button' style='width: 30px;' value='GO' id='{0}_gmPagination_getPage' title='跳转' onclick='GmUIControl.PagerControl(\"{0}\").getPage()' />", name)
                .Append("</span> </li></ul></div>")
                .AppendFormat("<input type='hidden' id='{1}_gmPagination_paginTotalCount' value='{0}'/>", attribute.TotalCount, name)
                .AppendFormat("<input type='hidden' id='{1}_gmPagination_paginTotalPages' value='{0}'/>", attribute.TotalPages, name)
                .AppendFormat("<input type='hidden' id='{1}_gmPagination_paginPageSize' value='{0}'/>", attribute.PageSize, name);

            return pagerBuilder.ToString();
        }
    }
}