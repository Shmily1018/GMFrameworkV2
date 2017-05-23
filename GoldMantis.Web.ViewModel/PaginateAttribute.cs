using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using GoldMantis.Common;
using GoldMantis.Common.CustomAttribute;

namespace GoldMantis.Web.ViewModel
{
    /// <summary>
    /// 分页控件
    /// </summary>
    [DataContract]
    public class PaginateAttribute
    {
        private string _pageSizeName;
        private string _pageIndexName;


        [DataMember]
        public int PageSize { get; private set; }

        [DataMember]
        public int PageIndex { get; private set; }

        [DataMember]
        public int TotalCount { get; private set; }

        [DataMember]
        public int TotalPages { get; private set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string EmptyTip { get; set; }

        /// <summary>
        /// 搜索对象名称
        /// </summary>
        [DataMember]
        public string SearchClassObjName { get; set; }

        [DataMember]
        public string PageSizeName
        {
            get
            {
                if (string.IsNullOrEmpty(_pageSizeName))
                {
                    return string.IsNullOrEmpty(this.SearchClassObjName)
                        ? "PageSize"
                        : this.SearchClassObjName + ".PageSize";
                }
                else
                {
                    return _pageSizeName;
                }
            }
            set { _pageSizeName = value; }
        }

        [DataMember]
        public string PageIndexName
        {
            get
            {
                if (string.IsNullOrEmpty(_pageIndexName))
                {
                    return string.IsNullOrEmpty(this.SearchClassObjName)
                        ? "PageIndex"
                        : this.SearchClassObjName + ".PageIndex";
                }
                else
                {
                    return _pageIndexName;
                }
            }
            set { _pageIndexName = value; }
        }

        public PaginateAttribute(int pageSize, int totalCount, int pageIndex, string url)
            : this(pageSize, totalCount, pageIndex, 5, url)
        {
        }

        private PaginateAttribute(int pageSize, int totalCount, int pageIndex, int pixCount, string url)
        {
            Url = url;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int) Math.Ceiling(TotalCount/(double) pageSize);
            TotalCount = totalCount;
        }

        public bool HasPreviousPage
        {
            get { return PageIndex > 0; }
        }

        public bool HasNextPage
        {
            get { return PageIndex + 1 < TotalPages; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize">每一页记录总数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="searchClassObj">查询实体</param>
        /// <param name="searchClassObjName">查询实体名称</param>
        /// <param name="url">前缀URL</param>
        /// <returns></returns>
        public static PaginateAttribute ConstructPaginate(int pageSize, int totalCount, int pageIndex, object searchClassObj, string searchClassObjName, string url)
        {
            PaginateAttribute pHelper = new PaginateAttribute(pageSize, totalCount, pageIndex, url);
            pHelper.SearchClassObjName = searchClassObjName;
            url = url.IsNullOrEmpty() ? string.Empty : url;
            url = url.Contains("?") ? url + "&" : url + "?";
            url = RemoveUrlPageIndex(searchClassObjName, url);
            string contructPart = pHelper.ConstructSearchCondition(searchClassObj, ref url);
            pHelper.Url = url + contructPart;

            return pHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize">每一页记录总数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="searchClassObj">查询实体</param>
        /// <param name="searchClassObjName">查询实体名称</param>
        /// <returns></returns>
        public static PaginateAttribute ConstructPaginate(int pageSize, int totalCount, int pageIndex, object searchClassObj, string searchClassObjName)
        {
            return ConstructPaginate(pageSize, totalCount, pageIndex, searchClassObj, searchClassObjName, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize">每一页记录总数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="url">前缀URL</param>
        /// <returns></returns>
        public static PaginateAttribute ConstructPaginate(int pageSize, int totalCount, int pageIndex, string url)
        {
            return ConstructPaginate(pageSize, totalCount, pageIndex, null, string.Empty, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize">每一页记录总数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public static PaginateAttribute ConstructPaginate(int pageSize, int totalCount, int pageIndex)
        {
            return ConstructPaginate(pageSize, totalCount, pageIndex, string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// remove pageIndex in URL
        /// </summary>
        /// <param name="searchClassObjName"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string RemoveUrlPageIndex(string searchClassObjName, string url)
        {
            //replace url pageIndex
            string pattern = string.IsNullOrEmpty(searchClassObjName)
                ? string.Format(@"{0}=[^&]*&", "PageIndex")
                : string.Format(@"{0}.{1}=[^&]*&", searchClassObjName, "PageIndex");
            Regex regex = new Regex(pattern);
            url = regex.Replace(url, string.Empty);

            return url;
        }

        private string ConstructSearchCondition(object searchClassObj, ref string url)
        {
            if (searchClassObj == null)
            {
                return string.Empty;
            }


            SearchClassObjName = string.IsNullOrEmpty(SearchClassObjName) ? "" : SearchClassObjName;
            StringBuilder sb = new StringBuilder();
            Type searchClassType = searchClassObj.GetType();
            PropertyInfo[] propertyInfos = null;
            propertyInfos = searchClassType.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            foreach (var item in propertyInfos)
            {
                if (Attribute.GetCustomAttribute(item, typeof (PaginateNotComposeAttribute)) != null)
                {
                    continue;
                }

                object propertyValue = item.GetValue(searchClassObj, null);
                //移除url 中之前拼的条件
                string patttern = string.Format(@"{0}.{1}=[^&]*&", SearchClassObjName, item.Name);
                Regex regex = new Regex(patttern);
                url = regex.Replace(url, string.Empty);

                if (propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString()))
                {
                    continue;
                }


                if (propertyValue.GetType() == typeof (DateTime))
                {
                    propertyValue = ((DateTime) propertyValue).ToString("yyyy-MM-dd");
                }

                string parameter = string.Format("{0}.{1}={2}", SearchClassObjName, item.Name, HttpUtility.UrlEncode(propertyValue.ToString()));
                sb.Append(parameter);
                sb.Append("&");
            }

            string temp = sb.ToString();

            if (string.IsNullOrEmpty(temp))
            {
                return string.Empty;
            }

            return temp;
        }

    }
}
