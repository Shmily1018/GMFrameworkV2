using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GoldMantis.Common;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.User;

namespace GoldMantis.Service.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“WorkFlowService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 WorkFlowService.svc 或 WorkFlowService.svc.cs，然后开始调试。
    public class WorkFlowService:IWorkFlowService
    {
        private WorkFlowBiz service = new WorkFlowBiz();


        public SAMenu GetSAMenuByID(int id)
        {
            return service.GetSAMenuByID(id);
        }

        public ModelCustomFlowIndex GetModelCustomFlowIndex(ModelCustomFlowIndex model)
        {
            return service.GetModelCustomFlowIndex(model);
        }

        public ModelCommonFlowEntityCreate GetModelCommonFlowEntityCreate(ModelCommonFlowEntityCreate model)
        {
            return service.GetModelCommonFlowEntityCreate(model);
        }

        public ModelCommonFlowEntityCreate PostModelCommonFlowEntityCreate(ModelCommonFlowEntityCreate model)
        {
            return service.PostModelCommonFlowEntityCreate(model);
        }

        public ModelSAUserIndex GetSAUsersByIDs(ModelSAUserIndex model)
        {
            return service.GetSAUsersByIDs(model);
        }

        public ModelNodeIndex MutualSelectNodeIndex(ModelNodeIndex model)
        {
            return service.MutualSelectNodeIndex(model);
        }

        public ModelSelectNextIndex MutualSelectNextIndex(ModelSelectNextIndex model)
        {
            return service.MutualSelectNextIndex(model);
        }

        public List<SCFlowMaping> GetSCFlowMapingByMenuIDAndDeptID(int menuID, int deptID)
        {
            return service.GetSCFlowMapingByMenuIDAndDeptID(menuID, deptID);
        }

        public ModelSCFlowMapingIndex GetModelChooseWorkFlowIndex(ModelSCFlowMapingIndex model)
        {
            return service.GetModelChooseWorkFlowIndex(model);
        }

        public ModelSAUserIndex GetSAUsers(ModelSAUserIndex model)
        {
            return service.GetSAUsers(model);
        }
    }
}
