using System.Collections.Generic;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;

namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface IFlowMapingService : IService
    {
        [OperationContract]
        IList<FlowMaping> List(int menuId, int deptId = 0);
    }
}