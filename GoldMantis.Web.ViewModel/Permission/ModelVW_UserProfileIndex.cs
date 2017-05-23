using System.Collections.Generic;
using System.Runtime.Serialization;
using GoldMantis.Entity;

namespace GoldMantis.Web.ViewModel.Permission
{
    /// <summary>
    /// 列表页模型
    /// </summary>
    [DataContract]
    public class ModelVW_UserProfileIndex : BaseModel
    {
        private SearchUserProfile _searchEntity = new SearchUserProfile();

        /// <summary>
        /// 列表数据源
        /// </summary>
        [DataMember]
        public IList<VW_UserProfile> GridDataSources
        { get; set; }

        /// <summary>
        /// 查询实体
        /// </summary>
        [DataMember]
        public SearchUserProfile SearchEntity
        {
            get { return _searchEntity; }
            set { _searchEntity = value; }
        }
    }

    /// <summary>
    /// 查询面板模型
    /// </summary>
    [DataContract]
    public class SearchUserProfile : BaseSearch
    {
        [DataMember(Order = 1)]
        public virtual string JobCode { get; set; }

        [DataMember(Order = 2)]
        public virtual string UserName { get; set; }

        [DataMember(Order = 3)]
        public virtual string PositionName { get; set; }

        [DataMember(Order = 4)]
        public virtual string DeptName { get; set; }

        
    }
}