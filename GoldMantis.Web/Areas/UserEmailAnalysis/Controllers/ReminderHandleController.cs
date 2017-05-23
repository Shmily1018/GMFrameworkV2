using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Service.Contract.ContractView;
using GoldMantis.Web.Core;
using GoldMantis.Web.ViewModel.UserEmailAnalysis.WF_REMINDER_WORKFLOW_TEMP;

namespace GoldMantis.Web.Areas.UserEmailAnalysis.Controllers
{
    public class ReminderHandleController : BaseController
    {
        protected IVW_WF_REMINDER_WORKFLOW_TEMPService _WF_REMINDER_WORKFLOW_TEMPService;
        

        public ActionResult Index(ModelVW_WF_REMINDER_WORKFLOW_TEMPIndex model)
        {
            model = _WF_REMINDER_WORKFLOW_TEMPService.GetModelWF_REMINDER_WORKFLOW_TEMPIndex(model);
            if (model.ExportType != null)
            {
                ExportToExcel(model);
                model.ExportType = null;
            }
            return View(model);
        }

        public ActionResult SetReasonByID(Int64 id, string reason)
        {
            var entity= _WF_REMINDER_WORKFLOW_TEMPService.SetReason(id, reason);

            return Content(entity.ID.ToString());
        }

        public void ExportToExcel(ModelVW_WF_REMINDER_WORKFLOW_TEMPIndex model)
        {

            try
            {

                model = _WF_REMINDER_WORKFLOW_TEMPService.GetModelWF_REMINDER_WORKFLOW_TEMPIndex(model);

                var workbook = new Workbook(Server.MapPath("~/ExcelTemplet/催办台账导出.xls"));
                var sheet = workbook.Worksheets[0];
                sheet.Name = "催办台账导出";
                var cells = sheet.Cells;


                cells[0, 0].Value = "催办台账导出";
                cells[1, 11].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var commonStyle = new Style();
                commonStyle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                int startRow = 3;

                for (int i = 0; i < model.GridDataSources.Count; i++)
                {
                    cells[startRow, 0].PutValue(model.GridDataSources[i].WorkflowName);
                    cells[startRow, 1].PutValue(model.GridDataSources[i].TokenName);
                    cells[startRow, 2].PutValue(model.GridDataSources[i].FlowStatus);
                    cells[startRow, 3].PutValue(model.GridDataSources[i].UserName);
                    cells[startRow, 4].PutValue(model.GridDataSources[i].Mobile);
                    cells[startRow, 5].PutValue(model.GridDataSources[i].DeptName);
                    cells[startRow, 6].PutValue(model.GridDataSources[i].CompanyName);
                    cells[startRow, 7].PutValue(model.GridDataSources[i].MessageContent);
                    cells[startRow, 8].PutValue(model.GridDataSources[i].SendDate.ToString("yyyy-MM-dd HH:mm:ss"));

                    var dateTime = model.GridDataSources[i].EndTime;
                    if (dateTime != null)
                        cells[startRow, 9].PutValue(dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                    cells[startRow, 10].PutValue(model.GridDataSources[i].TimeSpent);
                    cells[startRow, 11].PutValue(model.GridDataSources[i].Reason);

                    cells.SetRowHeight(startRow, 20);

                    for (int j = 0; j <=11; j++)
                    {
                        cells[startRow, j].SetStyle(commonStyle);
                    }
                    startRow++;
                }

                #region 输出到客户端
                string fileName = String.Format("催办台账导出({0}).xls",
                    DateTime.Now.ToString("yyyyMMdd-hhmmss"));
                System.Web.HttpContext.Current.Response.Clear();
                workbook.Save(System.Web.HttpContext.Current.Response,
                                        Url.Encode(fileName),
                                        ContentDisposition.Attachment,
                                        new OoxmlSaveOptions(SaveFormat.Excel97To2003));
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.Clear();


                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception("工作流监控导出！" + ex.Message);
            }
        }
    }
}