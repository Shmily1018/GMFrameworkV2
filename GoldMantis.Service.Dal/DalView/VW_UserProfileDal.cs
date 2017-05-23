using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Entity;

namespace GoldMantis.Service.Dal.DalView
{
    public class VW_UserProfileDal : RepositoryTable<VW_UserProfile>
    {
        public override string KeyID
        {
            get { return "KeyID"; }
            set { base.KeyID = value; }
        }
    }
}
