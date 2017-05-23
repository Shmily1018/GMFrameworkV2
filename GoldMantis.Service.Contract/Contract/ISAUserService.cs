using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel.Home;
using GoldMantis.Web.ViewModel.User;


namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface ISAUserService : IService
    {
        [OperationContract]
        SAUser Login(LogOnModel model);

        //[OperationContract]
        //ModelSAUserIndex GetSaUsersByIDs(ModelSAUserIndex model);

        //[OperationContract]
        //ModelNodeIndex MutualSelectNodeIndex(ModelNodeIndex model);

        //[OperationContract]
        //ModelSelectNextIndex MutualSelectNextIndex(ModelSelectNextIndex model);

        [OperationContract]
        List<string> FilterUserByFunction(List<string> sourceUser, int menuID);

        [OperationContract]
        SAUser ListByID(int id);

        [OperationContract]
        IList<SAUser> List();


    }
}