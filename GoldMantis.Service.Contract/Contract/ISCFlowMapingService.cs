using System.Collections.Generic;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.User;

namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface ISCFlowMapingService : IService
    {
        //[OperationContract]
        //ModelSCFlowMapingIndex GetModelChooseWorkFlowIndex(ModelSCFlowMapingIndex model);

        [OperationContract]
        List<SCFlowMaping> GetSCFlowMapingByMenuIDAndDeptID(int menuID, int deptID);

        [OperationContract]
        List<SCFlowMaping> GetMenuWFFromCache(int menuID, int deptID, int keyID, int checkstatus);

        [OperationContract]
        IList<SCFlowMaping> GetSCFlowMapingByMenuID(int menuID);

        [OperationContract]
        IList<SCFlowMaping> GetSCFlowMapingList();

        [OperationContract]
        SCFlowMaping GetSCFlowMapingByID(int id);

        [OperationContract]
        IList<SCDepartment> GetSCDepartmentList();

    }
}