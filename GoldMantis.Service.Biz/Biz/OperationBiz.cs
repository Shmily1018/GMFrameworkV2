using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Common;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;

namespace GoldMantis.Service.Biz.Biz
{
    public class OperationBiz : BaseTableBiz<Operation>, IOperationService
    {
        private OperationDal _dal;

        protected override IRepository<Operation> Repository
        {
            get { return _dal; }
            set { _dal = value.As<OperationDal>(); }
        }
    }
}