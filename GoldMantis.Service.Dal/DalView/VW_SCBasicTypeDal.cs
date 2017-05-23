using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Entity;

namespace GoldMantis.Service.Dal.DalView
{
    public class VW_SCBasicTypeDal : RepositoryTable<VW_SCBasicType>
    {
        public override string KeyID
        {
            get { return "KeyID"; }
            set { base.KeyID = value; }
        }
    }
}
