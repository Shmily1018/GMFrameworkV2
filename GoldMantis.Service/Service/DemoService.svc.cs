using System;
using System.Collections.Generic;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Web.ViewModel.User;

namespace GoldMantis.Service.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“DemoService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 DemoService.svc 或 DemoService.svc.cs，然后开始调试。
    [SessionPerCallServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DemoService : BaseService, IDemoService
    {
        private DemoBiz service;

        public IList<Demo> List()
        {
            return service.List();
        }

        public IList<Demo> ListTopX(int i)
        {
            return service.ListTopX(i);
        }

        public ModelDemoIndex GetModelDemoIndex(ModelDemoIndex model)
        {
            return service.GetModelDemoIndex(model);
        }

        public ModelDemoCreate GetModelDemoCreate(ModelDemoCreate model)
        {
            return service.GetModelDemoCreate(model);
        }

        public ModelDemoCreate PostModelDemoCreate(ModelDemoCreate model)
        {
            return service.PostModelDemoCreate(model);
        }

        public void DeleteByKey(int id)
        {
            service.DeleteByKey(id);
        }

        public void DeleteByKeys(int[] ids)
        {
            service.DeleteByKeys(ids);
        }

        public string GetDigramUrl(Guid guid)
        {
            return service.GetDigramUrl(guid);
        }

        public bool Checked(int keyID, int checkStatus)
        {
            return service.Checked(keyID, checkStatus);
        }

        public bool Post(int keyID, out string message)
        {
            return service.Post(keyID, out message);
        }
    }
}
