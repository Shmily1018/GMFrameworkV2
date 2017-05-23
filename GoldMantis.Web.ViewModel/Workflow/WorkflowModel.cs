using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using GoldMantis.Common;
using GoldMantis.WorkFlow.WCFClient;

namespace GoldMantis.Web.ViewModel
{
    public class WorkflowModel
    {
        public Guid ActorTokenId { get; set; }

        public Guid WfPublishId { get; set; }

        public string ReStartCommand { get; set; }

        public string TokenExtendInfo { get; set; }

        public string WfTokenName { get; set; }

        public string Idea { get; set; }

        public string WfName { get; set; }

        public bool IsCustomFlow { get; set; }

        public bool IsEmergency { get; set; }

        public string NodeName { get; set; }

        public bool AllowCustomFlow { get; set; }
    }

    public class ModelSCFlowMapingIndex : BaseModel
    {
        private SearchSCFlowMaping _searchEntity = new SearchSCFlowMaping();

        /// <summary>
        /// 列表数据源
        /// </summary>
        [DataMember]
        public IList<SCFlowMaping> GridDataSources
        { get; set; }

        /// <summary>
        /// 查询实体
        /// </summary>
        [DataMember]
        public SearchSCFlowMaping SearchEntity
        {
            get
            {
                return _searchEntity;
            }
            set
            {
                _searchEntity = value;
            }
        }

        [DataMember]
        public int MenuID { get; set; }

        [DataMember]
        public int DeptID { get; set; }
    }

    /// <summary>
    /// 查询面板模型
    /// </summary>
    [DataContract]
    public class SearchSCFlowMaping : BaseSearch
    {
        [Display(Name = "WFName")]
        [DataMember(Order = 0)]
        public virtual string WFName { get; set; }

    }


    [DataContract]
    public class ModelWorkFlowAdvice : BaseModel
    {

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string WorkflowName { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime? EndTime { get; set; }

        [DataMember]
        public IList<CheckedTokensModel> DataSource { get; set; }

    }
}