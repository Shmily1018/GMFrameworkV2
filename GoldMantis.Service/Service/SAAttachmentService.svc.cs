using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel;

namespace GoldMantis.Service.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SAAttachmentService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SAAttachmentService.svc or SAAttachmentService.svc.cs at the Solution Explorer and start debugging.
    
    [SessionPerCallServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SAAttachmentService :BaseService, ISAAttachmentService
    {
        private SAAttachmentBiz service;


        public IList<Attachment> GetAttachmentList(int billID, string tableName)
        {
            return service.GetAttachmentList(billID, tableName);
        }

        public SAAttachment GetByKey(int id)
        {
            return service.GetByKey(id);
        }

        public IList<SAAttachment> ListByKey(int id)
        {
            return service.ListByKey(id);
        }

        public IList<SAAttachment> List()
        {
            return service.List();
        }
    }
}
