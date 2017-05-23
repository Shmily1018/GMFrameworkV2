using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Common;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;

namespace GoldMantis.Service.Biz.Biz
{
    public class RoleMenuBiz : BaseTableBiz<RoleMenu>, IRoleMenuService
    {
        private RoleMenuDal _dal;

        protected override IRepository<RoleMenu> Repository
        {
            get { return _dal; }
            set { _dal = value.As<RoleMenuDal>(); }
        }
    }
}