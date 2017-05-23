using System.Collections.Generic;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;

namespace GoldMantis.Service.Biz.Biz
{
    public class FlowMapingBiz : BaseTableBiz<FlowMaping>, IFlowMapingService
    {
        private FlowMapingDal _dal;

        protected override IRepository<FlowMaping> Repository
        {
            get { return _dal; }
            set { _dal = value.As<FlowMapingDal>(); }
        }

        public IList<FlowMaping> List(int menuId, int deptId = 0)
        {
            return List(m => m.MenuID == menuId);
        }
    }
}