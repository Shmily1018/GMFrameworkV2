using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel.User;


namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface IDemoService : IService
    {
        [OperationContract]
        IList<Demo> List();

        [OperationContract]
        IList<Demo> ListTopX(int i);

        [OperationContract]
        ModelDemoIndex GetModelDemoIndex(ModelDemoIndex model);

        [OperationContract]
        ModelDemoCreate GetModelDemoCreate(ModelDemoCreate model);

        [OperationContract]
        ModelDemoCreate PostModelDemoCreate(ModelDemoCreate model);

        [OperationContract]
        void DeleteByKey(int id);

        [OperationContract]
        void DeleteByKeys(int[] ids);


        [OperationContract]
        String GetDigramUrl(Guid guid);

        [OperationContract]
        bool Checked(int keyID, int checkStatus);

        [OperationContract]
        bool Post(int keyID, out string message);
    }
}