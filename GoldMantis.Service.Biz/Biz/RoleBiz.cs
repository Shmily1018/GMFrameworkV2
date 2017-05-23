using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Common;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;

namespace GoldMantis.Service.Biz.Biz
{
    public class RoleBiz : BaseTableBiz<Role>,IRoleService
    {
        private RoleDal _dal;

        protected override IRepository<Role> Repository
        {
            get { return _dal; }
            set { _dal = value.As<RoleDal>(); }
        }
    }
}