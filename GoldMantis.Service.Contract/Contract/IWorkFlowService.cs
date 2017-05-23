using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.Home;
using GoldMantis.Web.ViewModel.User;
using SCFlowMaping = GoldMantis.Common.SCFlowMaping;


namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface IWorkFlowService : IService
    {
        [OperationContract]
        SAMenu GetSAMenuByID(int id);

        [OperationContract]
        ModelCustomFlowIndex GetModelCustomFlowIndex(ModelCustomFlowIndex model);

        [OperationContract]
        ModelCommonFlowEntityCreate GetModelCommonFlowEntityCreate(ModelCommonFlowEntityCreate model);

        [OperationContract]
        ModelCommonFlowEntityCreate PostModelCommonFlowEntityCreate(ModelCommonFlowEntityCreate model);

        [OperationContract]
        ModelSAUserIndex GetSAUsersByIDs(ModelSAUserIndex model);

        [OperationContract]
        ModelNodeIndex MutualSelectNodeIndex(ModelNodeIndex model);

        [OperationContract]
        ModelSelectNextIndex MutualSelectNextIndex(ModelSelectNextIndex model);

        [OperationContract]
        List<SCFlowMaping> GetSCFlowMapingByMenuIDAndDeptID(int menuID, int deptID);

        [OperationContract]
        ModelSCFlowMapingIndex GetModelChooseWorkFlowIndex(ModelSCFlowMapingIndex model);

        [OperationContract]
        ModelSAUserIndex GetSAUsers(ModelSAUserIndex model);

    }
}