using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Aspose.Cells;
using System.Web.Mvc;

using GoldMantis.Entity;
using GoldMantis.Common;
using GoldMantis.Web.Core;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Web.ViewModel.OA;

namespace GoldMantis.Web.Areas.OA.Controllers
{
    public class  VisitorController: BaseController
    {
        protected IUTIPARKCUSTOMERVisitorService _UTIPARKCUSTOMERVisitorService;

        /// <summary>
        /// ������������Ϣ����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageState"></param>
        public ActionResult Create(Int64? id, EnumPageMode? pageState)
        {
            ViewBag.PageState = GetPageState(id, pageState);
            var model = new ModelUTIPARKCUSTOMERVisitorCreate();
            try
            {
                if (id.HasValue){
                    model.UTIPARKCUSTOMERVisitor = new UTIPARKCUSTOMERVisitor(){VisitorID = id.Value};
                }
                model = _UTIPARKCUSTOMERVisitorService.GetModelUTIPARKCUSTOMERVisitorCreate(model);
                return View(model);
            }
            catch
            {
                return Content("��ȡ��Ϣʧ��!");
            }
        }

        /// <summary>
        /// ������������Ϣ����
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public ActionResult Create(ModelUTIPARKCUSTOMERVisitorCreate model)
        {
            try
            {
                model = _UTIPARKCUSTOMERVisitorService.PostModelUTIPARKCUSTOMERVisitorCreate(model);
                return Content("<script> window.top.menu.closeTabCallBack(" + model.UTIPARKCUSTOMERVisitor.VisitorID+");</script>");
            }
            catch (Exception ex)
            {
                Error = "���濴������Ϣ��������ʧ��";
                return View(model);
            }
        }

        /// <summary>
        /// Index��������Ϣ����
        /// </summary>
        public ActionResult Index(ModelUTIPARKCUSTOMERVisitorIndex model)
        {
            try
            {
                model = _UTIPARKCUSTOMERVisitorService.GetModelUTIPARKCUSTOMERVisitorIndex(model);
                if(model.ExportType!=null){
                    ExportToExcel(model);
                    if (model.ExportType == EnumExportType.����ȫ��)
                    {
                        model.ExportType = null;
                        model = _UTIPARKCUSTOMERVisitorService.GetModelUTIPARKCUSTOMERVisitorIndex(model);
                    }
                }
                return View(model);
            }
            catch
            {
                return Content("��ȡ��������Ϣ��������ʧ�ܣ�");
            }
        }

        /// <summary>
        /// Export��������Ϣ����
        /// </summary>
        public void ExportToExcel(ModelUTIPARKCUSTOMERVisitorIndex model)
        {
            try
            {
                model = _UTIPARKCUSTOMERVisitorService.GetModelUTIPARKCUSTOMERVisitorIndex(model);
                //����Ϊʾ������
        
                var workbook = new Workbook(Server.MapPath("~/ExcelTemplet/��������ص���.xls"));
                var sheet = workbook.Worksheets[0];
                sheet.Name = "��������ص���";
                var cells = sheet.Cells;
        
                //cells[0, 0].Value = "��������ص���";
                //cells[1, 10].Value = DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss");
        
                //var commonStyle = new Style();
                //commonStyle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                //commonStyle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                //commonStyle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                //commonStyle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
        
                //int startRow = 3;
        
                //for (int i = 0; i < model.GridDataSources.Count; i++)
                //{
                //    cells[startRow, 0].PutValue(model.GridDataSources[i].MenuName);
                //    cells[startRow, 1].PutValue(model.GridDataSources[i].Name);
                //    cells[startRow, 2].PutValue(model.GridDataSources[i].TokenName);
                //    cells[startRow, 3].PutValue(model.GridDataSources[i].StartTime.ToString("yyyy -MM-dd HH:mm:ss"));
                //    cells[startRow, 4].PutValue(model.GridDataSources[i].CreateUserName);
                //    cells[startRow, 5].PutValue(model.GridDataSources[i].CreateUserDeptName);
                //    cells[startRow, 6].PutValue(model.GridDataSources[i].CreateUserCompanyName);
                //    cells[startRow, 7].PutValue(model.GridDataSources[i].CurrentActorName);
                //    cells[startRow, 8].PutValue(model.GridDataSources[i].CurrentActorDeptName);
                //    cells[startRow, 9].PutValue(model.GridDataSources[i].TimeSpent.ToString("0.00")); //С�����2λ
                //    cells[startRow, 10].PutValue(model.GridDataSources[i].ExtendInfo);
        
                //    cells.SetRowHeight(startRow, 20);
        
                //    for (int j = 0; j < 11; j++)
                //    {
                //        cells[startRow, j].SetStyle(commonStyle);
                //    }
                //    startRow++;
                //}
        
                #region ������ͻ���
        
                string fileName = String.Format("��������ص���({0}).xls",DateTime.Now.ToString("yyyyMMdd -hhmmss"));
                System.Web.HttpContext.Current.Response.Clear();
                workbook.Save(System.Web.HttpContext.Current.Response,Url.Encode(fileName),ContentDisposition.Attachment,
                                        new OoxmlSaveOptions(SaveFormat.Excel97To2003));
        
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.Clear();
        
        
                #endregion
            }
            catch(Exception ex)
            {
                throw new Exception("��ȡ��������Ϣ��������ʧ�ܣ�");
            }
        }

        /// <summary>
        /// ɾ����������Ϣ����
        /// </summary>
        /// <param name="ids"></param>
        [HttpPost]
        public ActionResult Delete(Int64[] ids)
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                _UTIPARKCUSTOMERVisitorService.DeleteByKeys(ids);
                jsonResult.Data = new { result = "�����ɹ���" };
            }
            catch
            {
                jsonResult.Data = new { result = "����ʧ�ܡ�" };
            }
            return jsonResult;
        }
    }
}
