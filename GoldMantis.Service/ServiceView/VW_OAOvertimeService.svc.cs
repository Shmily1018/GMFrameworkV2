using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GoldMantis.Common;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Service.Contract.ContractView;
using GoldMantis.Web.ViewModel.OA;

namespace GoldMantis.Service.ServiceView
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“VW_OAOvertimeService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 VW_OAOvertimeService.svc 或 VW_OAOvertimeService.svc.cs，然后开始调试。
    
    [SessionPerCallServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class VW_OAOvertimeService :BaseService,IVW_OAOvertimeService
    {
        private VW_OAOvertimeBiz service;

        public ModelVW_OAOvertimeIndex GetModelVW_OAOvertimeIndex(ModelVW_OAOvertimeIndex model)
        {
            return service.GetModelVW_OAOvertimeIndex(model);
        }
    }
}
