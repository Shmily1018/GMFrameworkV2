using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Common;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;

namespace GoldMantis.Service.Biz.Biz
{
    public class MenuOperationBiz : BaseTableBiz<MenuOperation>, IMenuOperationService
    {
        private MenuOperationDal _dal;

        protected override IRepository<MenuOperation> Repository
        {
            get { return _dal; }
            set { _dal = value.As<MenuOperationDal>(); }
        }
    }
}