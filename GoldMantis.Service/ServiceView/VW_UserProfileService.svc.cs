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
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“VW_UserProfileService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 VW_UserProfileService.svc 或 VW_UserProfileService.svc.cs，然后开始调试。
    [SessionPerCallServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class VW_UserProfileService : BaseService, IVW_UserProfileService
    {

        private VW_UserProfileBiz service;

        public IList<VW_UserProfile> ListByLoginID(string LoginID)
        {
            return service.ListByLoginID(LoginID);
        }

        public IList<VW_UserProfile> ListByKeyID(int keyID)
        {
            return service.ListByKeyID(keyID);
        }

        public ModelVW_UserProfileIndex GetModelVW_OAKPILeavesIndex(ModelVW_UserProfileIndex model)
        {
            return service.GetModelVW_OAKPILeavesIndex(model);
        }
    }
}
