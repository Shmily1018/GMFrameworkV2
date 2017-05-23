using System.Collections.Generic;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel.OA;
using GoldMantis.Web.ViewModel.User;

namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface IOAOvertimeService : IService
    {


        [OperationContract]
        ModelOAOvertimeCreate GetModelOAOvertimeCreate(ModelOAOvertimeCreate model);

        [OperationContract]
        ModelOAOvertimeCreate PostModelOAOvertimeCreate(ModelOAOvertimeCreate model);

        [OperationContract]
        void DeleteByKeys(int[] ids);

        [OperationContract]
        bool Checked(int keyID, int checkStatus);

        [OperationContract]
        bool Post(int keyID, out string message);
    }
}