using System;
using System.Collections.Generic;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Entity;
using GoldMantis.Service.Biz.Biz;

namespace GoldMantis.Service.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“MenuService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 MenuService.svc 或 MenuService.svc.cs，然后开始调试。
    [SessionPerCallServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MenuService :BaseService, IMenuService
    {
        private MenuBiz menuBiz;

        public IList<Menu> List()
        {
            return menuBiz.List();
        }
    }
}
