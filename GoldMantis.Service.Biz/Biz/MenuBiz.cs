using System.Collections.Generic;
using System.Linq;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;

namespace GoldMantis.Service.Biz.Biz
{
    public class MenuBiz : BaseTableBiz<Menu>,IMenuService
    {
        private MenuDal _dal;

        protected override IRepository<Menu> Repository
        {
            get { return _dal; }
            set { _dal = value.As<MenuDal>(); }
        }

        public IList<Menu> List()
        {
            return base.List();
        }
    }
}