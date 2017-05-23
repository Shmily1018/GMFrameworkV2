using System.Collections.Generic;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel.OA;

namespace GoldMantis.Service.Contract.ContractView
{
    [ServiceContract]
    public interface IVW_UserProfileService : IService
    {

        [OperationContract]
        IList<VW_UserProfile> ListByLoginID(string LoginID);

        [OperationContract]
        IList<VW_UserProfile> ListByKeyID(int keyID);

        [OperationContract]
        ModelVW_UserProfileIndex GetModelVW_OAKPILeavesIndex(ModelVW_UserProfileIndex model);
    }
}