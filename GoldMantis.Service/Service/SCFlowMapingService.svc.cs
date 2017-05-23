using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GoldMantis.Common;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Service.Contract.Contract;

namespace GoldMantis.Service.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“SCFlowMapingService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 SCFlowMapingService.svc 或 SCFlowMapingService.svc.cs，然后开始调试。
    [SessionPerCallServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SCFlowMapingService :BaseService, ISCFlowMapingService
    {

        private SCFlowMapingBiz service=new SCFlowMapingBiz();
        //public ModelSCFlowMapingIndex GetModelChooseWorkFlowIndex(ModelSCFlowMapingIndex model)
        //{
        //    return service.GetModelChooseWorkFlowIndex(model);
        //}

        public List<SCFlowMaping> GetSCFlowMapingByMenuIDAndDeptID(int menuID, int deptID)
        {
            return service.GetSCFlowMapingByMenuIDAndDeptID(menuID, deptID);
        }

        public List<SCFlowMaping> GetMenuWFFromCache(int menuID, int deptID, int keyID, int checkstatus)
        {
            return service.GetMenuWFFromCache(menuID, deptID, keyID, checkstatus);
        }

        public IList<SCFlowMaping> GetSCFlowMapingByMenuID(int menuID)
        {
            return service.GetSCFlowMapingByMenuID(menuID);
        }

        public IList<SCFlowMaping> GetSCFlowMapingList()
        {
            return service.GetSCFlowMapingList();
        }

        public SCFlowMaping GetSCFlowMapingByID(int id)
        {
            return service.GetSCFlowMapingByID(id);
        }

        public IList<SCDepartment> GetSCDepartmentList()
        {
            return service.GetSCDepartmentList();
        }
    }
}
