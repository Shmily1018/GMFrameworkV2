using System;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Web.Core;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel.User;

namespace GoldMantis.Web.Areas.SystemManage.Controllers
{
    public class UserProfileController : BaseController
    {
        protected IWorkFlowService _WorkFlowService;
        // GET: OA/UserProfile
        public ActionResult Index(ModelSAUserIndex model)
        {
            model = _WorkFlowService.GetSAUsers(model);

            var table = new TableAttribute<SAUser>();
            table.DataSource = model.GridDataSources;

            table.TableHeaderRow = new TableRowAttribute<SAUser>();
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<SAUser>() { InnerText = "" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<SAUser>() { InnerText = "员工工号" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<SAUser>() { InnerText = "登录ID" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<SAUser>() { InnerText = "员工姓名" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<SAUser>() { InnerText = "手机" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<SAUser>() { InnerText = "邮箱" });


            table.TableBodyRowTemplete = new TableRowAttribute<SAUser>();

            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<SAUser>() { CellWidth = 30, InnerHtmlDelegate = o => string.Format("<input class='cb-select-selection' type='radio' name='selection' value='{0}' data-row='{{Text:\"{1}\", Value:{2}}}'>", o.ID, o.UserName, o.ID) });

            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<SAUser>() { CellWidth = 100, InnerHtmlDelegate = o => !String.IsNullOrWhiteSpace(o.JobCode) ? o.JobCode : String.Empty, TipTextDelegate = o => !String.IsNullOrWhiteSpace(o.JobCode) ? o.JobCode : String.Empty });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<SAUser>() { CellWidth = 100, InnerHtmlDelegate = o => o.LoginID, TipTextDelegate = o => o.LoginID });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<SAUser>() { CellWidth = 100, InnerHtmlDelegate = o => o.UserName, TipTextDelegate = o => o.UserName });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<SAUser>() { CellWidth = 100, InnerHtmlDelegate = o => o.Mobile, TipTextDelegate = o => o.Mobile });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<SAUser>() { CellWidth = 100, InnerHtmlDelegate = o => o.Email, TipTextDelegate = o => o.Email });

            ViewBag.TableData = table;

            return View(model);
        }
    }
}