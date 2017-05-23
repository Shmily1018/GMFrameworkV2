using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Entity;

namespace GoldMantis.Service.Dal.Dal
{
    public class DalSAAttachment : RepositoryTable<SAAttachment>
    {
        public override string KeyID
        {
            get { return "ID"; }
            set { base.KeyID = value; }
        }



    }
}
