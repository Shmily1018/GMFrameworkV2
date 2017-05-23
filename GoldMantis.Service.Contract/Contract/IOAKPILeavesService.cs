using System;
using System.Collections.Generic;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel.OA;

namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface IOAKPILeavesService : IService
    {
        [OperationContract]
        ModelOAKPILeavesCreate GetModelOAKPILeavesCreate(ModelOAKPILeavesCreate model);

        [OperationContract]
        ModelOAKPILeavesCreate PostModelOAKPILeavesCreate(ModelOAKPILeavesCreate model);

        [OperationContract]
        void DeleteByKeys(int[] ids);

        [OperationContract]
        bool Checked(int keyID, int checkStatus);

        [OperationContract]
        bool Post(int keyID, out string message);

        [OperationContract]
        bool UnSubmit(int keyID, out string message);
    }
}