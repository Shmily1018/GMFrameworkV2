/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         WfCommandButtonTypeEnum.cs
** Creator:          Joe
** CreateDate:       2015-11-10
** Changer:
** LastChangeDate:
** Description:      WorkFlow命令枚举
** VersionInfo:
*********************************************************/
using System.Runtime.Serialization;

namespace GoldMantis.Web.HtmlControl
{
    public enum WfCommandButtonTypeEnum
    {
        [EnumMember]
        [WfCommandButton(CommandName = "提交", CommandClientID = "gm_wf_submit", CommandTitle = "提交", IconClass = "fa fa-hand-o-up", Script = "")]
        Commit = 1,

        [EnumMember]
        [WfCommandButton(CommandName = "同意", CommandClientID = "gm_wf_agree", CommandTitle = "同意", IconClass = "fa fa-level-down", Script = "")]
        Agree = 2,

        [EnumMember]
        [WfCommandButton(CommandName = "不同意", CommandClientID = "gm_wf_deprecate", CommandTitle = "不同意", IconClass = "fa fa-level-up", Script = "")]
        Disagree = 3,

        [EnumMember]
        [WfCommandButton(CommandName = "弃权", CommandClientID = "gm_wf_cancel", CommandTitle = "弃权", IconClass = "fa fa-share", Script = "")]
        Cancel = 4,

        [EnumMember]
        [WfCommandButton(CommandName = "直送", CommandClientID = "gm_wf_through", CommandTitle = "直送", IconClass = "fa fa-rocket", Script = "")]
        Through = 5,

        [EnumMember]
        [WfCommandButton(CommandName = "委托", CommandClientID = "gm_wf_assign", CommandTitle = "委托", IconClass = "fa fa-random", Script = "")]
        Assign = 6,

        [EnumMember]
        [WfCommandButton(CommandName = "退回", CommandClientID = "gm_wf_refuse", CommandTitle = "退回", IconClass = "fa fa-reply", Script = "")]
        Refuse = 7,

        [EnumMember]
        [WfCommandButton(CommandName = "终止", CommandClientID = "gm_wf_stop", CommandTitle = "终止", IconClass = "fa fa-power-off", Script = "")]
        Stop = 8,

        [EnumMember]
        [WfCommandButton(CommandName = "已阅", CommandClientID = "gm_wf_view", CommandTitle = "已阅", IconClass = "fa fa-eye", Script = "")]
        View = 9,

        [EnumMember]
        [WfCommandButton(CommandName = "抄送", CommandClientID = "gm_wf_cc", CommandTitle = "抄送", IconClass = "fa fa-send-o", Script = "")]
        CC = 12,

        [EnumMember]
        [WfCommandButton(CommandName = "挂起", CommandClientID = "gm_wf_suspend", CommandTitle = "挂起", IconClass = "fa fa-anchor", Script = "")]
        Suspend = 13,

        [EnumMember]
        [WfCommandButton(CommandName = "恢复", CommandClientID = "gm_wf_resume", CommandTitle = "恢复", IconClass = "fa fa-refresh", Script = "")]
        Resume = 14,

        [EnumMember]
        [WfCommandButton(CommandName = "审批意见", CommandClientID = "gm_wf_viewOpinion", CommandTitle = "审批意见", IconClass = "fa fa-sitemap", Script = "")]
        ViewOpinion = 15,

        [EnumMember]
        [WfCommandButton(CommandName = "流程图", CommandClientID = "gm_wf_viewDiagram", CommandTitle = "流程图", IconClass = "fa  fa-picture-o", Script = "")]
        ViewDiagram = 16,

        [EnumMember]
        [WfCommandButton(CommandName = "自定义", CommandClientID = "gm_wf_customFlow", CommandTitle = "自定义", IconClass = "fa fa-user", Script = "")]
        CustomFlow = 17
    }
}