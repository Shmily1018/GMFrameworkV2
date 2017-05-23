using System;
using System.Web.Mvc;
using System.Web.UI;
using Aspose.Cells;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Web.Core;
using GoldMantis.Web.ViewModel.OA;

namespace GoldMantis.Web.Areas.OA.Controllers
{
    public class OAKPILeavesController : BaseController
    {
        protected IOAKPILeavesService _OAKPILeavesService;
        public ActionResult Index(ModelOAKPILeavesIndex model)
        {
            model = _OAKPILeavesService.GetModelOAKPILeavesIndex(model);

            if (model.ExportType != null)
            {
                ExportToExcel(model);
                model.ExportType = null;
            }
            return View(model);
        }

        public void ExportToExcel(ModelOAKPILeavesIndex model)
        {
            try
            {
                model = _OAKPILeavesService.GetModelOAKPILeavesIndex(model);

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
                    //cells[startRow, 0].PutValue(model.GridDataSources[i].MenuName);
                    //cells[startRow, 1].PutValue(model.GridDataSources[i].Name);
                    //cells[startRow, 2].PutValue(model.GridDataSources[i].TokenName);
                    //cells[startRow, 3].PutValue(model.GridDataSources[i].StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    //cells[startRow, 4].PutValue(model.GridDataSources[i].CreateUserName);
                    //cells[startRow, 5].PutValue(model.GridDataSources[i].CreateUserDeptName);
                    //cells[startRow, 6].PutValue(model.GridDataSources[i].CreateUserCompanyName);
                    //cells[startRow, 7].PutValue(model.GridDataSources[i].CurrentActorName);
                    //cells[startRow, 8].PutValue(model.GridDataSources[i].CurrentActorDeptName);
                    //cells[startRow, 9].PutValue(model.GridDataSources[i].TimeSpent.ToString("0.00"));
                    //cells[startRow, 10].PutValue(model.GridDataSources[i].ExtendInfo);

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

        public ActionResult Create(Int32? id, EnumPageMode? pageState)
        {
            ViewBag.PageState = GetPageState(id, pageState);
            var model = new ModelOAKPILeavesCreate();

            try
            {
                if (id.HasValue)
                {
                    model.OAKPILeaves = new OAKPILeaves { ID = id.Value };
                }
                model = _OAKPILeavesService.GetModelOAKPILeavesCreate(model);
                return View(model);
            }
            catch
            {
                return Content("读取信息失败!");
            }
        }


        [HttpPost]
        public ActionResult Create(ModelOAKPILeavesCreate model)
        {
            try
            {
                model = _OAKPILeavesService.PostModelOAKPILeavesCreate(model);
                return Content("<script>window.top.menu.closeTabCallBack(" + model.OAKPILeaves.ID + ");</script>");
            }
            catch (Exception ex)
            {
                Error = "操作失败";
                return View(model);
            }
        }


        [HttpPost]
        public ActionResult Delete(int[] ids)
        {
            var jsresult = new JsonResult();
            try
            {
                 _OAKPILeavesService.DeleteByKeys(ids);
                jsresult.Data = new { result = "操作成功!" };
            }
            catch
            {
                jsresult.Data = new { result = "操作失败!" };
            }
            return jsresult;
        }

    }
}