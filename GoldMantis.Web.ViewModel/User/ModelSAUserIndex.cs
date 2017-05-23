using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.WorkFlow.WCFClient;

namespace GoldMantis.Web.ViewModel.User
{
    [DataContract]
    public class ModelSAUserIndex : BaseModel
    {
        private SearchSAUser _searchEntity = new SearchSAUser();

        /// <summary>
        /// 列表数据源
        /// </summary>
        [DataMember]
        public IList<SAUser> GridDataSources { get; set; }

        /// <summary>
        /// 查询实体
        /// </summary>
        [DataMember]
        public SearchSAUser SearchEntity
        {
            get { return _searchEntity; }
            set { _searchEntity = value; }
        }

        [DataMember]
        public int[] IDs { get; set; }

        [DataMember]
        public string Pdata { get; set; }

    }

    [DataContract]
    public class SearchSAUser : BaseSearch
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "UserName")]
        [DataMember(Order = 1)]
        public virtual string UserName { get; set; }

        [Display(Name = "JobCode")]
        [DataMember(Order = 2)]
        public virtual string JobCode { get; set; }

        [Display(Name = "Mobile")]
        [DataMember(Order = 3)]
        public virtual string Mobile { get; set; }

        [Display(Name = "Email")]
        [DataMember(Order = 4)]
        public virtual string Email { get; set; }
    }

    [DataContract]
    public class ModelNodeIndex : BaseModel
    {
        private SearchNode _searchEntity = new SearchNode();

        /// <summary>
        /// 列表数据源
        /// </summary>
        [DataMember]
        public IList<NodeModel> GridDataSources { get; set; }

        /// <summary>
        /// 查询实体
        /// </summary>
        [DataMember]
        public SearchNode SearchEntity
        {
            get { return _searchEntity; }
            set { _searchEntity = value; }
        }

        [DataMember]
        public string Pdata { get; set; }

    }

    [DataContract]
    public class SearchNode : BaseSearch
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Node")]
        [DataMember(Order = 1)]
        public virtual string NodeName { get; set; }
    }


    [DataContract]
    public class ModelSelectNextIndex : BaseModel
    {
        private SearchSelectNext _searchEntity = new SearchSelectNext();

        /// <summary>
        /// 列表数据源
        /// </summary>
        [DataMember]
        public IList<ModelSelectNext> GridDataSources { get; set; }

        /// <summary>
        /// 查询实体
        /// </summary>
        [DataMember]
        public SearchSelectNext SearchEntity
        {
            get { return _searchEntity; }
            set { _searchEntity = value; }
        }

        [DataMember]
        public string Pdata { get; set; }

    }

    [DataContract]
    public class ModelCommonFlowEntityCreate : BaseModel
    {
        [DataMember]
        public CommonFlowEntity CommonFlowEntity { get; set; }

        [DataMember]
        public Guid PublishID { get; set; }

        [DataMember]
        public bool AddResult { get; set; }

        [DataMember]
        public String AddResultMessage { get; set; }

        [DataMember]
        public string NodesValue { get; set; }

        [DataMember]
        public int MenuID { get; set; }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public String UserName { get; set; }

        [DataMember]
        public List<CustomFlowNode> CustomFlowNodes { get; set; }

        [DataMember]
        public String SelfWFName { get; set; }

        [DataMember]
        public int NodeAuditUserID { get; set; }

        [DataMember]
        public string NodeAuditUserName { get; set; }

        [DataMember]
        public int Hour { get; set; }

        [DataMember]
        public bool IsRemind { get; set; }
        
    }

    [DataContract]
    public class ModelCustomFlowIndex : BaseModel
    {
        private SearchCustomFlow _searchEntity = new SearchCustomFlow();

        /// <summary>
        /// 列表数据源
        /// </summary>
        [DataMember]
        public IList<FlowEntityChild> GridDataSources { get; set; }

        /// <summary>
        /// 查询实体
        /// </summary>
        [DataMember]
        public SearchCustomFlow SearchEntity
        {
            get { return _searchEntity; }
            set { _searchEntity = value; }
        }

        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public int MenuID { get; set; }

        [DataMember]
        public int DeptID { get; set; }

    }


    [DataContract]
    public class FlowEntityChild : FlowEntity
    {
        [DataMember]
        public string CreateUserName { get; set; }

        public FlowEntityChild AutoCopy(FlowEntity parent)
        {
            FlowEntityChild child = new FlowEntityChild();


            var ParentType = typeof(FlowEntity);


            var Properties = ParentType.GetProperties();


            foreach (var Propertie in Properties)
            {
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }


            return child;
        }
    }

    [DataContract]
    public class SearchSelectNext : BaseSearch
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Node")]
        [DataMember(Order = 1)]
        public virtual string NodeName { get; set; }
    }

    [DataContract]
    public class SearchCustomFlow : BaseSearch
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Order = 1)]
        public virtual string CreateUser { get; set; }

        [DataMember(Order = 2)]
        public virtual string WFNodeAuditor { get; set; }

        [DataMember(Order = 3)]
        public virtual string WFName { get; set; }

        [DataMember(Order = 4)]
        public virtual DateTime? WFCreateTimeBegin { get; set; }

        [DataMember(Order = 5)]
        public virtual DateTime? WFCreateTimeEnd { get; set; }
    }

    public class ModelSelectNext
    {
        public string NodeId
        {
            get;
            set;

        }

        public string NodeName
        {
            get;
            set;
        }
    }

    public class NodeModel
    {
        public string ActorTokenId
        {
            get;
            set;

        }

        public string NodeName
        {
            get;
            set;
        }

        public string DoTime
        {
            get;
            set;
        }

        public string DoUserName
        {
            get;
            set;
        }

    }

    [Serializable]
    public class CustomFlowNode
    {
        public bool IsRemind
        {
            get;
            set;
        }
        public int Hour
        {
            get;
            set;
        }
        public int NoID
        {
            get;
            set;
        }

        public string UserID
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }

        public int Sort
        {
            get;
            set;
        }
    }
}
