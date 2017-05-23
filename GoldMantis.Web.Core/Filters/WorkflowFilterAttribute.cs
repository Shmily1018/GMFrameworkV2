using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.DI;
using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel;
using GoldMantis.WorkFlow.WCFClient;

namespace GoldMantis.Web.Core
{
    public class WorkflowFilterAttribute : FilterAttribute,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            WorkflowController controller = filterContext.Controller.As<WorkflowController>();

            if (controller.IsNull())
            {
                throw new ApplicationException("筛选器WorkflowFilterAttribute只能修饰WorkflowController及其子类的Action...");
            }

            controller.WorkflowInfo = GetWorkflowInfo();
            FlowCheck();
            controller.CommandButtons = GetCommandButtons(controller);
        }

        private WorkflowModel GetWorkflowInfo()
        {
            IFlowMapingService mfmService = ServiceFactory.GetService<IFlowMapingService>();
            /***待补充***/
            IList<FlowMaping> mappings = mfmService.List(2386, 0);

            if (!mappings.HasItems())
            {
                throw new ApplicationException("当前功能没有绑定工作流，请不要使用工作流控制器，避免消耗系统性能...");
            }


            WorkflowModel workflowInfo = new WorkflowModel();

            foreach (FlowMaping mapping in mappings)
            {
                workflowInfo.AllowCustomFlow = workflowInfo.AllowCustomFlow || mapping.AllowCustomFlow;

                if (workflowInfo.AllowCustomFlow)
                {
                    break;
                }
            }

            
            workflowInfo.ActorTokenId = RequestHelper.GetQueryStringGuid("OPID");
            workflowInfo.WfPublishId = RequestHelper.GetQueryStringGuid("WFPID");
            workflowInfo.WfName = RequestHelper.GetQueryString("WFName");

            return workflowInfo;
        }

        private void FlowCheck()
        {

        }

        private List<WfCommandButton> GetCommandButtons(WorkflowController controller)
        {
            WorkflowModel workflowInfo = controller.WorkflowInfo;
            List<WfCommandButton> buttons = new List<WfCommandButton>();

            if (workflowInfo.ActorTokenId.Equals(Guid.Empty))
            {
                buttons.Add(WfCommandButtonCollection.Find(WfCommandButtonTypeEnum.Commit));
            }
            else
            {
                string nodeName;
                TokenServiceClient wf = new TokenServiceClient();
                IList<CommandModel> cmds = wf.GetNodeCommands(workflowInfo.ActorTokenId, out nodeName);
                workflowInfo.NodeName = nodeName;
                workflowInfo.IsEmergency = wf.IsEmergency(workflowInfo.ActorTokenId);

                if (cmds.HasItems())
                {
                    foreach (CommandModel cmd in cmds)
                    {
                        WfCommandButtonTypeEnum buttonType; 

                        if (Enum.TryParse(cmd.Command.ToString(), out buttonType))
                        {
                            buttons.Add(WfCommandButtonCollection.Find(buttonType));
                        }
                    }
                }
            }

            if (!buttons.Contains(WfCommandButtonCollection.Find(WfCommandButtonTypeEnum.Commit)))
            {
                buttons.Add(WfCommandButtonCollection.Find(WfCommandButtonTypeEnum.ViewOpinion));
            }

            buttons.Add(WfCommandButtonCollection.Find(WfCommandButtonTypeEnum.ViewDiagram));

            if (buttons.Contains(WfCommandButtonCollection.Find(WfCommandButtonTypeEnum.Commit)) &&
                workflowInfo.AllowCustomFlow)
            {
                buttons.Add(WfCommandButtonCollection.Find(WfCommandButtonTypeEnum.CustomFlow));
            }

            return buttons;
        }
    }
}