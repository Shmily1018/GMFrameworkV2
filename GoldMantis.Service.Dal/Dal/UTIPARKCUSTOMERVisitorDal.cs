using System;
using System.Collections.Generic; 
using System.Linq;
using GoldMantis.Entity;

namespace GoldMantis.Service.Dal.Dal
{
    /// <summary>
    /// ��������Ϣ����
    /// </summary>
    public class  UTIPARKCUSTOMERVisitorDal:RepositoryTable<UTIPARKCUSTOMERVisitor>
    {
        public override string KeyID
        {
           get { return "VisitorID"; }
           set { base.KeyID = value; } 
        }

        public UTIPARKCUSTOMERVisitorDal()
        {
           base._defaultKeyID= "VisitorID"; 
        }
    }

}
