using System;
using System.Runtime.Serialization;
using GoldMantis.Common;
using GoldMantis.Common.Const;
using GoldMantis.Common.CustomAttribute;

namespace GoldMantis.Web.ViewModel
{

    [DataContract]
    public class BaseSearch
    {
        private int _pageSize;
        private int _pageIndex;
        private string _orderDirection;
        private string _commonSearchCondition;

        /// <summary>
        /// 分页记录条数
        /// </summary>
        [DataMember]
        public int PageSize
        {
            get { return _pageSize > 0 ? _pageSize : Consts.PAGE_SIZE; }
            set { _pageSize = value; }
        }

        /// <summary>
        /// 分页索引
        /// </summary>
        [DataMember]
        [PaginateNotCompose]
        public int PageIndex
        {
            get { return _pageIndex < 0 ? 0 : _pageIndex; }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// 排序列名称
        /// </summary>
        [DataMember]
        public string OrderName { get; set; }

        /// <summary>
        /// 排序方向
        /// </summary>
        [DataMember]
        public string OrderDirection
        {
            get
            {
                return _orderDirection == null ? ((int)OrderBy.ASC).ToString() : _orderDirection;
            }
            set
            {
                _orderDirection = value ?? ((int)OrderBy.ASC).ToString();
            }
        }

        /// <summary>
        /// 获取排序方向
        /// </summary>
        [PaginateNotCompose]
        [DataMember]
        public OrderBy EnumOrderDirection
        {
            get
            {
                return (OrderBy)Convert.ToInt32(OrderDirection);
            }
            set
            {
                OrderDirection = value.ToString();
            }
        }

        /// <summary>
        /// 页面右上角通用的查询条件
        /// </summary>
        [DataMember]
        public string CommonSearchCondition
        {
            get
            {
                return "请输入查询条件...".Equals(_commonSearchCondition) ? null : _commonSearchCondition;
            }
            set
            {
                _commonSearchCondition = value;
            }
        }
    }
}
