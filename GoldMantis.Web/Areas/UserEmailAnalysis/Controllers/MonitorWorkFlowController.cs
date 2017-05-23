using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using GoldMantis.Common.CustomEnum;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Service.Contract.ContractView;
using GoldMantis.Web.Core;
using GoldMantis.Web.ViewModel.UserEmailAnalysis.MonitorWorkFlow;
using GoldMantis.Web.ViewModel.UserEmailAnalysis.Report;
using GoldMantis.Web.ViewModel.UserEmailAnalysis.WF_REMINDER_WORKFLOW_TEMP;

namespace GoldMantis.Web.Areas.UserEmailAnalysis.Controllers
{
    public class MonitorWorkFlowController : BaseController
    {
        protected IMonitorWorkFlowService _MonitorWorkFlowService;
        protected IVW_WF_REMINDER_WORKFLOW_TEMPService _VW_WF_REMINDER_WORKFLOW_TEMPService;

        public ActionResult Index(ModelMonitorWorkFlowIndex model)
        {
            model = _MonitorWorkFlowService.GetMonitorWorkFlow(model);
            if (model.ExportType != null)
            {
                ExportToExcel(model);
                model.ExportType = null;
            }
            

            return View(model);
        }

        public async Task<ActionResult> DeskTop(ModelMonitorWorkFlowDeckIndexTop model)
        {
            return await Task<ActionResult>.Factory.StartNew(() =>
            {
                model =  _MonitorWorkFlowService.GetDeskTopIndex(model);
                return View(model);
            });

           
        }

        public void ExportToExcel(ModelMonitorWorkFlowIndex model)
        {

            try
            {

                model = _MonitorWorkFlowService.GetMonitorWorkFlow(model);

                var workbook = new Workbook(Server.MapPath("~/ExcelTemplet/工作流监控导出.xls"));
                var sheet = workbook.Worksheets[0];
                sheet.Name = "工作流监控导出";
                var cells = sheet.Cells;


                cells[0, 0].Value = "工作流监控导出";
                cells[1, 10].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var commonStyle = new Style();
                commonStyle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                commonStyle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

                int startRow = 3;

                for (int i = 0; i < model.GridDataSources.Count; i++)
                {
                    cells[startRow, 0].PutValue(model.GridDataSources[i].MenuName);
                    cells[startRow, 1].PutValue(model.GridDataSources[i].Name);
                    cells[startRow, 2].PutValue(model.GridDataSources[i].TokenName);
                    cells[startRow, 3].PutValue(model.GridDataSources[i].StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    cells[startRow, 4].PutValue(model.GridDataSources[i].CreateUserName);
                    cells[startRow, 5].PutValue(model.GridDataSources[i].CreateUserDeptName);
                    cells[startRow, 6].PutValue(model.GridDataSources[i].CreateUserCompanyName);
                    cells[startRow, 7].PutValue(model.GridDataSources[i].CurrentActorName);
                    cells[startRow, 8].PutValue(model.GridDataSources[i].CurrentActorDeptName);
                    cells[startRow, 9].PutValue(model.GridDataSources[i].TimeSpent.ToString("0.00"));
                    cells[startRow, 10].PutValue(model.GridDataSources[i].ExtendInfo);

                    cells.SetRowHeight(startRow, 20);

                    for (int j = 0; j < 11; j++)
                    {
                        cells[startRow, j].SetStyle(commonStyle);
                    }
                    startRow++;
                }

                #region 输出到客户端
                string fileName = String.Format("工作流监控导出({0}).xls",
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

        /// <summary>
        /// 发送微信。
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="wechatSendType"></param>
        [HttpPost]
        public void SendWeChatMessage(Guid[] ids)
        {
            _MonitorWorkFlowService.SendWechatMessag(ids, EnumWeChatSendType.部分发送, null);
        }


        /// <summary>
        /// 发送微信。
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="wechatSendType"></param>
        [HttpPost]
        public void SendWeChatMessageBatch(ModelMonitorWorkFlowIndex model)
        {
            _MonitorWorkFlowService.SendWechatMessag(null,EnumWeChatSendType.全部发送, model);
        }


        public ActionResult ShowReminderHandleRecord(ModelVW_WF_REMINDER_WORKFLOW_TEMPIndex model)
        {
            model=_VW_WF_REMINDER_WORKFLOW_TEMPService.GetModelWF_REMINDER_WORKFLOW_TEMPIndexByInstanceID(model);
            return View(model);
        }
    }
}