using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Common.CustomAttribute;

namespace GoldMantis.Common.CustomEnum
{

    public enum EnumLeaveType
    {
        病假 = 294,
        事假 = 295,
        婚假 = 296,
        产假 = 297,
        陪产假 = 298,
        丧假 =299,
        其他=300
    }

    public enum EnumDataDisplayType
    {
        数值 = 1,
        百分比 = 2
        
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperaType
    {
        /// <summary>
        /// 新增
        /// </summary>
        [EnumCode("（新增）")]
        Insert,
        /// <summary>
        /// 修改
        /// </summary>
        [EnumCode("（修改）")]
        Update,
        /// <summary>
        /// 删除
        /// </summary>
        [EnumCode("（删除）")]
        Delete,
        /// <summary>
        /// 导入
        /// </summary>
        [EnumCode("（导入）")]
        Import,
        /// <summary>
        /// 过账
        /// </summary>
        [EnumCode("（过账）")]
        Post,
        /// <summary>
        /// 反审核
        /// </summary>
        [EnumCode("（反审核）")]
        UnPost,
        /// <summary>
        /// 流程审批
        /// </summary>
        [EnumCode("（流程审批）")]
        Workflow,
        /// <summary>
        /// 导出
        /// </summary>
        [EnumCode("（导出）")]
        Export,
        /// <summary>
        /// 查看
        /// </summary>
        [EnumCode("（查看）")]
        View,
        /// <summary>
        /// 跟踪
        /// </summary>
        [EnumCode("（跟踪）")]
        Trace,

        /// <summary>
        /// 查询
        /// </summary>
        [EnumCode("（查询）")]
        Query
    }

}
