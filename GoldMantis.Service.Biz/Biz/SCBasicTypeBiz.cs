using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.Common.CustomEnum;
using GoldMantis.DI;
using GoldMantis.Entity;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.Home;
using GoldMantis.Web.ViewModel.OA;
using GoldMantis.Web.ViewModel.User;
using GoldMantis.WorkFlow.WCFClient;
using NHibernate;
using NHibernate.Linq;

namespace GoldMantis.Service.Biz.Biz
{
    public class SCBasicTypeBiz : BaseTableBiz<SCBasicType>
    {
        private SCBasicTypeDal _dal;

        public SCBasicTypeBiz()
        {
            
        }

        protected override IRepository<SCBasicType> Repository
        {
            get { return _dal; }
            set { _dal = ObjectExtensions.As<SCBasicTypeDal>(value); }
        }

        public ModelSCBasicTypeCreate GetModelSCBasicTypeCreate(ModelSCBasicTypeCreate model)
        {
            model.DataDisplayTypeList = EnumHelper.GetSelectListWithFirstOption(typeof(EnumDataDisplayType), "--请选择--", "");
            
            if (model.SCBasicType != null && model.SCBasicType.TypeID != 0)
            {
                model.SCBasicType = _dal.GetByKey<Int32>(model.SCBasicType.TypeID);
            }

            //model的其他属性赋值
            return model;
        }

        public ModelSCBasicTypeCreate PostModelSCBasicTypeCreate(ModelSCBasicTypeCreate model)
        {
            ITransaction tx = DataAccess.Session.BeginTransaction();

            using (tx)
            {
                DataAccess.SaveOrUpdate(model.SCBasicType);

                tx.Commit();
            }

            return model;
        }


    }
}