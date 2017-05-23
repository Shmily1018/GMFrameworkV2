using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using GoldMantis.Common.CustomEnum;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Web.Core;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.UserEmailAnalysis.CompleteInfo;

namespace GoldMantis.Web.Areas.UserEmailAnalysis.Controllers
{
    public class CompleteInfoController : BaseController
    {
        protected ICompleteInfoService _CompleteInfoService;

        public ActionResult Index(ModelCompleteInfoIndex model)
        {
            model = model ?? new ModelCompleteInfoIndex();

            if (model.FileUploadHelperObject == null)
            {
                model.FileUploadHelperObject = new ModelFileUpload() { FileKey = Guid.NewGuid() };
                SessionManager.JobCodeList = null;
            }

            if (SessionManager.JobCodeList != null)
            {
                model = _CompleteInfoService.GetSAUserByJobCodes(model, SessionManager.JobCodeList);
            }
            return View(model);
        }

        public ActionResult AnalysisExcel(string fileKey, HttpPostedFileBase fileData)
        {
            Stream stream = fileData.InputStream;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            var tcWorkBook = new Workbook(stream);
            var tcWorkSheet = tcWorkBook.Worksheets[0];
            var tcCells = tcWorkSheet.Cells;

            var jobCodeList = new List<String>();


            for (var j = 1; j <= tcCells.MaxRow; j++)
            {
                if (!String.IsNullOrWhiteSpace(tcCells[j, 0].StringValue))
                {
                    jobCodeList.Add(tcCells[j, 0].StringValue);
                }
            }

            SessionManager.JobCodeList = jobCodeList;
            return Content("ok");

        }

        public ActionResult GetExcelTemplet()
        {
            var path = Server.MapPath("~/ExcelTemplet/工号模板.xls");
            var name = Path.GetFileName(path);
            return File(path, "application/vnd.ms-excel", Url.Encode(name));
        }

        public void ExportToExcel(ModelCompleteInfoIndex model)
        {

            try
            {
                model.ExportType = EnumExportType.导出全部;
                model = _CompleteInfoService.GetSAUserByJobCodes(model, SessionManager.JobCodeList);

                var workbook = new Workbook(Server.MapPath("~/ExcelTemplet/人员信息.xls"));
                var sheet = workbook.Worksheets[0];
                sheet.Name = "邮件信息信息补全";
                var cells = sheet.Cells;

                var commonStyle = new Style();
                commonStyle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                int startRow = 1;
                for (int i = 0; i < model.GridDataSources.Count; i++)
                {
                    cells[startRow, 0].PutValue(model.GridDataSources[i].JobCode);
                    cells[startRow, 1].PutValue(model.GridDataSources[i].UserName);
                    cells[startRow, 2].PutValue(model.GridDataSources[i].DeptName);
                    cells[startRow, 3].PutValue(model.GridDataSources[i].Email);
                    cells[startRow, 4].PutValue(model.GridDataSources[i].IsOnState);

                    cells.SetRowHeight(startRow, 20);

                    for (int j = 0; j < 5; j++)
                    {
                        cells[startRow, j].SetStyle(commonStyle);
                    }
                    startRow++;
                }

                #region 输出到客户端
                string fileName = String.Format("人员信息({0}).xls",
                    DateTime.Now.ToString("yyyyMMdd-hhmmss"));
                workbook.Save(System.Web.HttpContext.Current.Response,
                                        Url.Encode(fileName),
                                        ContentDisposition.Attachment,
                                        new OoxmlSaveOptions(SaveFormat.Excel97To2003));
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.Clear();


                #endregion

            }
            catch
            {
                throw new Exception("人员信息表失败！");
            }
        }
    }
}