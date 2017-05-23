using System.Collections.Generic;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel.User;

namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface ISCDepartmentService : IService
    {
        [OperationContract]
        Dictionary<int, SCDepartment> GetSCDepartment();

        [OperationContract]
        List<int> GetChildDeptmentsByDept(int deptID);
    }
}