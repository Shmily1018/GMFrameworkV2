using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Web.ViewModel.OA;

namespace GoldMantis.Service.Contract.ContractView
{
    [ServiceContract]
    public interface IVW_OAOvertimeService : IService
    {
        [OperationContract]
        ModelVW_OAOvertimeIndex GetModelVW_OAOvertimeIndex(ModelVW_OAOvertimeIndex model);
    }
}