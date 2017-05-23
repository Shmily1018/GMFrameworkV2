using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Common;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;

namespace GoldMantis.Service.Biz.Biz
{
    public class SystemModuleBiz : BaseTableBiz<SystemModule>, ISystemModuleService
    {
        private SystemModuleDal _dal;

        protected override IRepository<SystemModule> Repository
        {
            get { return _dal; }
            set { _dal = value.As<SystemModuleDal>(); }
        }
    }
}