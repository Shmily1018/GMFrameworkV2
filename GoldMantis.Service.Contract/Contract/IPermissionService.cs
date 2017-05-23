using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Common;
using GoldMantis.Entity;
using SCDepartment = GoldMantis.Common.SCDepartment;

namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface IPermissionService : IService
    {
        [OperationContract]
        UserInfo GetLoginInfo(string loginID, string password, bool isImitation, out string message);

        [OperationContract]
        UserInfo ImitationAccount(int userID, int currentUserID, out string message);

        [OperationContract]
        IList<SAMenu> GetUserMenuInfo(int userID, int sourceAppId);

        [OperationContract]
        SAUser GetSAUserByID(int id);

        [OperationContract]
        IList<SAUser> GetSAUserByIDs(int[] ids);

        [OperationContract]
        SAMenu GetSAMenuByID(int id);

        [OperationContract]
        IList<SAMenu> GetSAMenuList();

    }
}
