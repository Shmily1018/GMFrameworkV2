using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.DI;
using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.Home;
using GoldMantis.Web.ViewModel.User;
using GoldMantis.WorkFlow.WCFClient;
using NHibernate;
using NHibernate.Linq;

namespace GoldMantis.Service.Biz.Biz
{
    public class SAUserDeptBiz : BaseTableBiz<SAUserDept>, ISAUserDeptService
    {
        private SAUserDeptDal _dal;

        private DalSAAttachment dalSAAttachment;

        protected override IRepository<SAUserDept> Repository
        {
            get { return _dal; }
            set { _dal = ObjectExtensions.As<SAUserDeptDal>(value); }
        }

        public SAUserDept GetByUserID(int userID)
        {
            var session = DataAccess.Session;

            var SAUserDept = session
                .Query<SAUserDept>().FirstOrDefault(x => x.UserID == userID);

            return SAUserDept;
        }


    }
}