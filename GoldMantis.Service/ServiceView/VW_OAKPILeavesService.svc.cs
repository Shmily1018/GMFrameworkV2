using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Service.Contract.ContractView;
using GoldMantis.Web.ViewModel.OA;

namespace GoldMantis.Service.ServiceView
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“VW_OAKPILeavesService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 VW_OAKPILeavesService.svc 或 VW_OAKPILeavesService.svc.cs，然后开始调试。
    [SessionPerCallServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class VW_OAKPILeavesService :BaseService, IVW_OAKPILeavesService
    {

        protected VW_OAKPILeavesBiz service;

        public ModelVW_OAKPILeavesIndex GetModelVW_OAKPILeavesIndex(ModelVW_OAKPILeavesIndex model)
        {
            return service.GetModelVW_OAKPILeavesIndex(model);
        }

        public void DeleteByKeys(int[] ids)
        {
            service.DeleteByKeys(ids);
        }
    }
}
