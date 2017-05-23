using System;
using System.Collections.Generic;
using System.Linq;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Entity;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;
using GoldMantis.Service.Dal.DalView;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.OA;
using GoldMantis.Web.ViewModel.User;
using NHibernate;
using NHibernate.Linq;

namespace GoldMantis.Service.Biz.Biz
{
    public class VW_SCBasicTypeBiz : BaseTableBiz<VW_SCBasicType>
    {
        private VW_SCBasicTypeDal _dal;

        protected override IRepository<VW_SCBasicType> Repository
        {
            get { return _dal; }
            set { _dal = ObjectExtensions.As<VW_SCBasicTypeDal>(value); }
        }

        public IList<VW_SCBasicType> ListByParentID(int parentID)
        {
            return _dal.List(x => x.ParentID == parentID);
        }
    }
}
