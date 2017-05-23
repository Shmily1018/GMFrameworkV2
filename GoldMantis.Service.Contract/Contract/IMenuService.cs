using System.Collections.Generic;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;

namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface IMenuService : IService
    {
        [OperationContract]
        IList<Menu> List();
    }
}