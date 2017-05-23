using GoldMantis.Entity;

namespace GoldMantis.Service.Dal.Dal
{
    public class OAKPILeavesDal : RepositoryTable<OAKPILeaves>
    {
        public override string KeyID
        {
            get { return "ID"; }
            set { base.KeyID = value; }
        }
    }
}