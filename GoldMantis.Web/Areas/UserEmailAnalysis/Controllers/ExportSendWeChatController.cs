using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Web.Core;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.UserEmailAnalysis.ExportSendWeChat;

namespace GoldMantis.Web.Areas.UserEmailAnalysis.Controllers
{
    public class ExportSendWeChatController : BaseController
    {
        protected IExportSendWeChatService _ExportSendWeChatService;


        public ActionResult Index(ModelExportSendWeChatIndex model)
        {
            if (model.FileUploadHelperObject == null)
            {
                model.FileUploadHelperObject = new ModelFileUpload() { FileKey = Guid.NewGuid() };
                SessionManager.EmailAndUseInfoList = null;
            }

            if (SessionManager.EmailAndUseInfoList != null)
            {
                model = _ExportSendWeChatService.GetSAUserExportSendWeChatByEmails(model, SessionManager.EmailAndUseInfoList);
            }
            return View(model);
        }

        public ActionResult AnalysisExcel(string fileKey, HttpPostedFileBase fileData)
        {
            Stream stream = fileData.InputStream;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

           LoadOptions loadOptions = new LoadOptions(LoadFormat.CSV);
           var tcWorkBook = new Workbook(stream, loadOptions);
            var tcWorkSheet = tcWorkBook.Worksheets[0];
            var tcCells = tcWorkSheet.Cells;

            var emailAndUseInfoList = new Dictionary<string, double>();


            for (var j = 1; j <= tcCells.MaxRow; j++)
            {
                if (!String.IsNullOrWhiteSpace(tcCells[j, 1].StringValue))
                {
                    string useInfo= tcCells[j, 4].StringValue;
                    if (useInfo.EndsWith("%"))
                    {
                        emailAndUseInfoList.Add(tcCells[j, 1].StringValue + "@goldmantis.com", double.Parse(useInfo.TrimEnd('%')) / 100);
                    }
                    
                }
            }

            SessionManager.EmailAndUseInfoList = emailAndUseInfoList;
            return Content("ok");

        }

        public ActionResult GetExcelTemplet()
        {
            var path = Server.MapPath("~/ExcelTemplet/邮件发送微信模板.csv");
            var name = Path.GetFileName(path);
            return File(path, "application/csv", Url.Encode(name));
        }

        public ActionResult SendWeChatMessage(ModelExportSendWeChatIndex model)
        {
            _ExportSendWeChatService.SendWechatMessag(model, SessionManager.EmailAndUseInfoList);

            return Content("<script>window.top.bootstrapGM.unblockUI('form');window.top.bootstrapGM.alert('发送微信成功！');</script>");
        }
    }
}