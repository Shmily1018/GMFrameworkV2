using GoldMantis.Entity;

namespace GoldMantis.Service.Dal.Dal
{
    public class VW_OAKPILeavesDal : RepositoryTable<VW_OAKPILeaves>
    {
        public override string KeyID
        {
            get { return "KeyID"; }
            set { base.KeyID = value; }
        }
    }
}