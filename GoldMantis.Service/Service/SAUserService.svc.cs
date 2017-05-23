using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Web.ViewModel.Home;
using GoldMantis.Web.ViewModel.User;

namespace GoldMantis.Service.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“SAUserService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 SAUserService.svc 或 SAUserService.svc.cs，然后开始调试。

    [SessionPerCallServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SAUserService :BaseService, ISAUserService
    {
        private SAUserBiz service;

        public SAUser Login(LogOnModel model)
        {
            return service.Login(model);
        }

        //public ModelSAUserIndex GetSaUsersByIDs(ModelSAUserIndex model)
        //{
        //    return service.GetSaUsersByIDs(model);
        //}

        //public ModelSelectNextIndex MutualSelectNextIndex(ModelSelectNextIndex model)
        //{
        //    return service.MutualSelectNextIndex(model);
        //}

        public List<string> FilterUserByFunction(List<string> sourceUser, int menuID)
        {
            return service.FilterUserByFunction(sourceUser, menuID);
        }

        public SAUser ListByID(int id)
        {
            return service.ListByID(id);
        }

        public IList<SAUser> List()
        {
            return service.List();
        }

        //public ModelNodeIndex MutualSelectNodeIndex(ModelNodeIndex model)
        //{
        //    return service.MutualSelectNodeIndex(model);
        //}
    }
}
