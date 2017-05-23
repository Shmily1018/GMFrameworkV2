using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GoldMantis.Common;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Web.ViewModel.OA;

namespace GoldMantis.Service.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“OAKPILeavesService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 OAKPILeavesService.svc 或 OAKPILeavesService.svc.cs，然后开始调试。
    [SessionPerCallServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class OAKPILeavesService :BaseService, IOAKPILeavesService
    {
        private OAKPILeavesBiz service;

        public ModelOAKPILeavesCreate GetModelOAKPILeavesCreate(ModelOAKPILeavesCreate model)
        {
            return service.GetModelOAKPILeavesCreate(model);
        }

        public ModelOAKPILeavesCreate PostModelOAKPILeavesCreate(ModelOAKPILeavesCreate model)
        {
            return service.PostModelOAKPILeavesCreate(model);
        }

        public void DeleteByKeys(int[] ids)
        {
            service.DeleteByKeys(ids);
        }

        public bool Checked(int keyID, int checkStatus)
        {
            return service.Checked(keyID, checkStatus);
        }

        public bool Post(int keyID, out string message)
        {
            return service.Post(keyID, out message);
        }

        public bool UnSubmit(int keyID, out string message)
        {
            return service.UnSubmit(keyID, out message);
        }
    }
}
