using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Common;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;

namespace GoldMantis.Service.Biz.Biz
{
    public class RoleTypeBiz : BaseTableBiz<RoleType>, IRoleTypeService
    {
        private RoleTypeDal _dal;

        protected override IRepository<RoleType> Repository
        {
            get { return _dal; }
            set { _dal = value.As<RoleTypeDal>(); }
        }
    }
}