using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoldMantis.Common.CustomEnum;
using GoldMantis.Entity;
using GoldMantis.Service.Contract.Contract;
using GoldMantis.Service.Contract.ContractView;
using GoldMantis.Web.Core;
using GoldMantis.Web.ViewModel.OA;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoldMantis.Web.Areas.SystemConfig.Controllers
{
    public class SystemParameterSetController : BaseController
    {
        // GET: SystemConfig/SystemParameterSet
        protected IVW_SCBasicTypeService _VW_SCBasicTypeService;
        protected ISCBasicTypeService _SCBasicTypeService;

        public ActionResult Index()
        {
            return View();
        }

        public string GetData(int parentID)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" };
            var basicTypeList = _VW_SCBasicTypeService.ListByParentID(parentID);

            var list = new ArrayList { };
            foreach (VW_SCBasicType item in basicTypeList)
            {

                //有无子节点判断
                var child = _VW_SCBasicTypeService.ListByParentID(item.TypeID);
                if (child != null && child.Count > 0)
                {
                    item.state = "closed";
                }
                list.Add(item);
            }

            return JsonConvert.SerializeObject(list, Formatting.Indented, timeConverter);

        }


        public ActionResult Create(Int32? id, EnumPageState? pageState)
        {
            ModelSCBasicTypeCreate model = new ModelSCBasicTypeCreate();
            if (id != null)
            {
                //修改或查看用户信息
                model.SCBasicType = new SCBasicType { TypeID = id.Value };
            }
            model = _SCBasicTypeService.GetModelSCBasicTypeCreate(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ModelSCBasicTypeCreate model)
        {
            try
            {
                model = _SCBasicTypeService.PostModelSCBasicTypeCreate(model);

                return Content("<script>window.top.menu.closeTabCallBack();</script>");
            }
            catch (Exception ex)
            {
                Error = "操作失败";
                return View(model);
            }
        }
    }



}