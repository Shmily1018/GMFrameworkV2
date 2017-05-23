using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using GoldMantis.Common.CustomEnum;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Service.Contract.ContractSP;
using GoldMantis.Web.Core;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.ViewModel.UserEmailAnalysis.Report;

namespace GoldMantis.Web.Areas.UserEmailAnalysis.Controllers
{
    public class ReportSendTimesController : BaseController
    {
        protected ISP_Report_SendTimesService _SP_Report_SendTimesService;


        public ActionResult Index(ModelReportSendTimesIndex model)
        {
            if (model.SearchEntity.BeginDate == null || model.SearchEntity.EndDate == null)
            {
                model.SearchEntity.EndDate = DateTime.Now;
                model.SearchEntity.BeginDate = DateTime.Now.AddDays(-2);
            }

            model.Parameter.BeginDate = model.SearchEntity.BeginDate.Value.ToString("yyyy-MM-dd");
            model.Parameter.EndDate = model.SearchEntity.EndDate.Value.AddDays(1).ToString("yyyy-MM-dd");

            model = _SP_Report_SendTimesService.Invoke(model);

            if (!String.IsNullOrWhiteSpace(model.WeChatMessageTemplate))
            {
                model.ExportType = EnumExportType.导出全部;
                _SP_Report_SendTimesService.SendWechatMessag(model);
                model.ExportType = null;
                TempData["Message"] = "发送微信成功！";
            }
            return View(model);

        }

        public void ExportToExcel(string beginDate,string endDate)
        {

            try
            {
                var model = new ModelReportSendTimesIndex();
                model.ExportType = EnumExportType.导出全部;
                model.Parameter.BeginDate = beginDate;
                model.Parameter.EndDate = Convert.ToDateTime(endDate).AddDays(1).ToString("yyyy-MM-dd");
                model = _SP_Report_SendTimesService.Invoke(model);

                var workbook = new Workbook(Server.MapPath("~/ExcelTemplet/连续发送三次以上微信报表.xls"));
                var sheet = workbook.Worksheets[0];
                sheet.Name = "连续发送三次以上微信报表";
                var cells = sheet.Cells;


                cells[0, 0].Value = String.Format("连续发送三次以上微信({0}~{1})", beginDate, endDate);
                cells[1, 5].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var commonStyle = new Style();
                commonStyle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                int startRow =3;
                for (int i = 0; i < model.GridDataSources.Count; i++)
                {
                    cells[startRow, 0].PutValue(model.GridDataSources[i].JobCode);
                    cells[startRow, 1].PutValue(model.GridDataSources[i].UserName);
                    cells[startRow, 2].PutValue(model.GridDataSources[i].Email);
                    cells[startRow, 3].PutValue(model.GridDataSources[i].DeptName);
                    cells[startRow, 4].PutValue(model.GridDataSources[i].SendCount);
                    cells[startRow, 5].PutValue(model.GridDataSources[i].LastEmailUseInfo.ToString("P"));

                    cells.SetRowHeight(startRow, 20);

                    for (int j = 0; j < 6; j++)
                    {
                        cells[startRow, j].SetStyle(commonStyle);
                    }
                    startRow++;
                }

                #region 输出到客户端
                string fileName = String.Format("连续发送三次以上微信报表({0}).xls", Convert.ToDateTime(beginDate).ToString("yyyyMMdd") + "-"+Convert.ToDateTime(endDate).ToString("yyyyMMdd")
                    );
                workbook.Save(System.Web.HttpContext.Current.Response,
                                        Url.Encode(fileName),
                                        ContentDisposition.Attachment,
                                        new OoxmlSaveOptions(SaveFormat.Excel97To2003));
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.Clear();


                #endregion

            }
            catch(Exception ex)
            {
                throw new Exception("连续发送三次以上微信报表！" +ex.Message);
            }
        }

        //public ActionResult SendWeChatMessage(ModelReportSendTimesIndex model)
        //{
        //    if (model.SearchEntity.BeginDate == null || model.SearchEntity.EndDate == null)
        //    {
        //        model.SearchEntity.EndDate = DateTime.Now;
        //        model.SearchEntity.BeginDate = DateTime.Now.AddDays(-2);
        //    }
        //    model.Parameter.BeginDate = model.SearchEntity.BeginDate.Value.ToString("yyyy-MM-dd");
        //    model.Parameter.EndDate = model.SearchEntity.EndDate.Value.AddDays(1).ToString("yyyy-MM-dd");
        //    model=_SP_Report_SendTimesService.Invoke(model);
            
            
        //    return View("Index",model);
        //}
    }
}