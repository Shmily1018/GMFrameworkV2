using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel;

namespace GoldMantis.Service.Contract.Contract
{
    [ServiceContract]
    public interface ISAAttachmentService : IService
    {
        [OperationContract]
        IList<Attachment> GetAttachmentList(int billID, string tableName);

        [OperationContract]
        SAAttachment GetByKey(int id);

        [OperationContract]
        IList<SAAttachment> ListByKey(int id);

        [OperationContract]
        IList<SAAttachment> List();

    }
}
