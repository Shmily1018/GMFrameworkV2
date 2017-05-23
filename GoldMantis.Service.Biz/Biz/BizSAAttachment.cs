using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel;

namespace GoldMantis.Service.Biz.Biz
{
    public class SAAttachmentBiz : BaseTableBiz<SAAttachment>
    {
        private DalSAAttachment _dal;

        protected override IRepository<SAAttachment> Repository
        {
            get { return _dal; }
            set { _dal = value.As<DalSAAttachment>(); }
        }

        public IList<Attachment> GetAttachmentList(int billID, string tableName)
        {
            IList<SAAttachment> listUTCommonAttachment = this.List(x => x.BillID == billID && x.TableName == tableName);

            IList<Attachment> listAttachment = new List<Attachment>();
            foreach (var item in listUTCommonAttachment)
            {
                listAttachment.Add(new Attachment()
                {
                    ID = item.ID,
                    FileSize = item.DocSize,
                    Path = item.DocPath,
                    Name = item.DocName,
                    Expand = item.Expand
                });
            }
            return listAttachment;

            return null;
        }

        public SAAttachment GetByKey(int id)
        {
            return base.GetByKey(id);
        }

        public IList<SAAttachment> ListByKey(int id)
        {
            return base.List(x => x.ID == id);
        }

        public IList<SAAttachment> List()
        {
            return base.List();
        }
       
    }
}
