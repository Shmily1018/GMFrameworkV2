using System.Collections.Generic;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel.OA;

namespace GoldMantis.Service.Contract.ContractView{
    [ServiceContract]
    public interface IVW_SCBasicTypeService : IService
    {
        [OperationContract]
        IList<VW_SCBasicType> ListByParentID(int parentID);
    }
}