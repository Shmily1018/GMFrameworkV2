using GoldMantis.Entity;
using GoldMantis.Entity;

namespace GoldMantis.Service.Dal.Dal
{
    public class SCBasicTypeDal : RepositoryTable<SCBasicType>
    {
        public SCBasicTypeDal()
        {
            _defaultKeyID = "TypeID";
        }

        //public override string KeyID
        //{
        //    get { return "TypeID"; }
        //    set { base.KeyID = value; }
        //}
    }
}