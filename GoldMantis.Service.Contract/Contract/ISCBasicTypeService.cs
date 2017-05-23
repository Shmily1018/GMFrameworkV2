using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel.Home;
using GoldMantis.Web.ViewModel.OA;
using GoldMantis.Web.ViewModel.User;


namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface ISCBasicTypeService : IService
    {
        [OperationContract]
        ModelSCBasicTypeCreate GetModelSCBasicTypeCreate(ModelSCBasicTypeCreate model);

        [OperationContract]
        ModelSCBasicTypeCreate PostModelSCBasicTypeCreate(ModelSCBasicTypeCreate model);
    }
}