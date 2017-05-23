using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Service.Contract.Contract;
using SCFlowMaping = GoldMantis.Common.SCFlowMaping;

namespace GoldMantis.Service.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“PermissionService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 PermissionService.svc 或 PermissionService.svc.cs，然后开始调试。

    [SessionPerCallServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class PermissionService : IPermissionService
    {
        private readonly PermissionBiz service=new PermissionBiz();

        public UserInfo GetLoginInfo(string loginID, string password, bool isImitation, out string message)
        {
            return service.GetLoginInfo(loginID, password, isImitation, out message);
        }

        public UserInfo ImitationAccount(int userID, int currentUserID, out string message)
        {
            return service.ImitationAccount(userID, currentUserID, out message);
        }

        public IList<SAMenu> GetUserMenuInfo(int userID, int sourceAppId)
        {
            return service.GetUserMenuInfo(userID, sourceAppId);
        }

        public SAUser GetSAUserByID(int id)
        {
            return service.GetSAUserByID(id);
        }

        public IList<SAUser> GetSAUserByIDs(int[] ids)
        {
            return service.GetSAUserByIDs(ids);
        }

        public SAMenu GetSAMenuByID(int id)
        {
            return service.GetSAMenuByID(id);
        }

        public IList<SAMenu> GetSAMenuList()
        {
            return service.GetSAMenuList();
        }
    }
}
