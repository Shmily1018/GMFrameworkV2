using GoldMantis.Entity;
using GoldMantis.Entity;

namespace GoldMantis.Service.Dal.Dal
{
    public class SAUserDeptDal : RepositoryTable<SAUserDept>
    {
        public override string KeyID
        {
            get { return "ID"; }
            set { base.KeyID = value; }
        }
    }
}