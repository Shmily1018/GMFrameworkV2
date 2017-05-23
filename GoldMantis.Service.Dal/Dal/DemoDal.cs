using GoldMantis.Entity;

namespace GoldMantis.Service.Dal.Dal
{
    public class DemoDal:RepositoryTable<Demo>
    {
        public override string KeyID
        {
            get { return "ID"; }
            set { base.KeyID = value; }
        }
    }
}