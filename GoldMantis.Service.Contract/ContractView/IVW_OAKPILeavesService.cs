using System.Collections.Generic;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel.OA;

namespace GoldMantis.Service.Contract.ContractView{
    [ServiceContract]
    public interface IVW_OAKPILeavesService : IService
    {
        [OperationContract]
        ModelVW_OAKPILeavesIndex GetModelVW_OAKPILeavesIndex(ModelVW_OAKPILeavesIndex model);

        [OperationContract]
        void DeleteByKeys(int[] ids);
    }
}