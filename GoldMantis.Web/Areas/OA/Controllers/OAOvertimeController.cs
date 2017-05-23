using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GoldMantis.Common;
using GoldMantis.Common.Const;
using GoldMantis.Common.CustomAttribute;
using GoldMantis.Common.CustomEnum;
using GoldMantis.Entity;
using GoldMantis.Service.Biz.Biz;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Service.Contract.ContractView;
using GoldMantis.Web.Core;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel.OA;
using GoldMantis.WorkFlow.WCFClient;

namespace GoldMantis.Web.Areas.OA.Controllers
{
    public class OAOvertimeController : BaseController
    {
        protected IOAOvertimeService _OAOvertimeService;
        protected IVW_OAOvertimeService _VW_OAOvertimeService;
        protected ISAUserService _SAUserService;
        protected IVW_UserProfileService userProfileService;

        // GET: OA/OAOvertime
        public ActionResult Index(ModelVW_OAOvertimeIndex model)
        {
            model = model ?? new ModelVW_OAOvertimeIndex();
            model = _VW_OAOvertimeService.GetModelVW_OAOvertimeIndex(model);

            TableAttribute<VW_OAOvertime> table = new TableAttribute<VW_OAOvertime>();
            table.DataSource = model.GridDataSources;

            table.TableHeaderRow = new TableRowAttribute<VW_OAOvertime>();

            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { InnerText = "" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { InnerText = "加班编号" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { InnerText = "加班人员" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { InnerText = "所属部门" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { InnerText = "加班时间" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { InnerText = "加班理由" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { InnerText = "编制人" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { InnerText = "编制日期" });
            table.TableHeaderRow.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { InnerText = "审批状态" });


            table.TableBodyRowTemplete = new TableRowAttribute<VW_OAOvertime>()
            {
                HtmlAttributeDelegates = new List<Func<VW_OAOvertime, KeyValuePair<string, object>>>()
                    {
                        m => new KeyValuePair<string, object>("ondblclick", 
                            "window.top.menu.addTabs('"+GetPageStateName(m.ID,m.Checkstatus) +"员工加班信息', Maintain.editUrl+\'?id=" + m.ID + "&checkStatus=" + m.Checkstatus + "\')")
                    }
            };
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { CellWidth = 30, InnerHtmlDelegate = o => string.Format("<input class='cb-maintain-selection' type='checkbox' name='selection' value='{0}' {1}>", o.ID,o.Checkstatus==EnumCheckStatus.Init.GetHashCode()&&!o.Ispost?"":" disabled='disabled'") });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { CellWidth = 100, InnerHtmlDelegate = o => o.OvertimeCode, TipTextDelegate = o => o.OvertimeCode });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { CellWidth = 100, InnerHtmlDelegate = o => o.OvertimeUser, TipTextDelegate = o => o.OvertimeUser });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { CellWidth = 100, InnerHtmlDelegate = o => o.UserDept, TipTextDelegate = o => o.UserDept });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { CellWidth = 100, InnerHtmlDelegate = o => o.OvertimerDate, TipTextDelegate = o => o.OvertimerDate });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { CellWidth = 100, InnerHtmlDelegate = o => o.OvertimeReason, TipTextDelegate = o => o.OvertimeReason });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { CellWidth = 100, InnerHtmlDelegate = o => o.Maker, TipTextDelegate = o => o.Maker });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { CellWidth = 100, InnerHtmlDelegate = o => o.MakeDate.ToDateString(), TipTextDelegate = o => o.MakeDate.ToDateString() });
            table.TableBodyRowTemplete.TableCells.Add(new TableCellAttribute<VW_OAOvertime>() { CellWidth = 100, InnerHtmlDelegate = o => IsPostDecoration(o.Checkstatus), TipTextDelegate = o => EnumCode.GetFieldCode(Enum.Parse(typeof(EnumCheckStatus), o.Checkstatus.ToString())) });

            ViewBag.TableData = table;

            return View(model);
        }

        public ActionResult Create(Int32? id, int? checkStatus)
        {

            ViewBag.PageState = GetPageState(id, checkStatus);
            var model = new ModelOAOvertimeCreate();

            try
            {
                if (id.HasValue)
                {
                    model.OAOvertime = new OAOvertime { ID = id.Value };
                }
                model = _OAOvertimeService.GetModelOAOvertimeCreate(model);

                if (model.OAOvertime == null)
                {
                    model.OAOvertime = new OAOvertime();
                    model.OAOvertime.Code = Consts.SAVE_AND_GENERATE;
                    model.OAOvertime.DeptID = SessionManager.UserInfo.HrDeptID;
                    model.OAOvertime.MakeDate = DateTime.Now;
                    model.OAOvertime.EmployeeID = SessionManager.UserInfo.UserID;

                    model.DeptName = SessionManager.UserInfo.DisplayDepartMent;
                    model.Maker = SessionManager.UserInfo.DisplayName;
                    model.EmployeeName = SessionManager.UserInfo.DisplayName;
                }
                else
                {
                    var userProfile= userProfileService.ListByKeyID(model.OAOvertime.EmployeeID).FirstOrDefault();
                    model.Maker = userProfile.UserName;
                    model.EmployeeName = userProfile.UserName;
                    model.DeptName = userProfile.DeptName;
                }

                #region 绑定工作流

                if (model.OAOvertime != null)
                {
                    model.CheckStatus = model.OAOvertime.Checkstatus;
                    if (model.CheckStatus.HasValue && model.CheckStatus.Value > 0)
                    {
                        model.FormCheckStatus = EnumCheckStatus.Post;
                    }

                }

                model.MenuID = "2685";
                model.MenuURL = "OA/OAOvertimeEdit.aspx";
                model.WFTokenName = "员工加班";
                model.DeptID = SessionManager.UserInfo.HrDeptID;
                model.KeyID = model.OAOvertime != null ? model.OAOvertime.ID : 0;

                base.InitWFController(model);

                #endregion

                if (model.Detail == null)
                {
                    model.Detail = new List<OAOvertimeDetail>()
                    {
                        new OAOvertimeDetail(),
                        new OAOvertimeDetail(),
                        new OAOvertimeDetail(),
                        new OAOvertimeDetail()
                    };
                }
                else
                {
                    model.Detail.Add(new OAOvertimeDetail());
                }
               


                return View(model);
            }
            catch
            {
                return Content("读取信息失败!");
            }
        }


        [HttpPost]
        public ActionResult Create(ModelOAOvertimeCreate model)
        {

            try
            {
                model.OAOvertime.MakeDate = DateTime.Now;
                model.OAOvertime.Postdate = DateTime.Now;
                model.OAOvertime.MakerID = SessionManager.UserInfo.UserID;


                model = _OAOvertimeService.PostModelOAOvertimeCreate(model);

                if (model.Comand == CommandCatalog.Commit.GetHashCode().ToString())
                {
                    model.KeyID = model.OAOvertime.ID;
                    ViewBag.PageState = EnumPageState.Edit;
                    return View(model);
                }


                return Content("<script>window.top.menu.closeTabCallBack();</script>");
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
            JsonResult jsresult = new JsonResult();
            try
            {
                _OAOvertimeService.DeleteByKeys(ids);
                jsresult.Data = new { result = "操作成功!" };
            }
            catch
            {
                jsresult.Data = new { result = "操作失败!" };
            }
            return jsresult;
        }


        public override bool OnWorkflowCompleted(out string message, WorkflowEventArgs e, int keyID)
        {
            int checkstatus = e.CheckStatus.GetHashCode();
            message = string.Empty;

            bool result = _OAOvertimeService.Checked(keyID, checkstatus);

            if (checkstatus == EnumCheckStatus.Ok.GetHashCode()) //审批通过自动审核
            {
                result = result && _OAOvertimeService.Post(keyID, out  message);
            }
            return result;
        }

    }
}