using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.Common.CustomEnum;
using GoldMantis.Entity;
using GoldMantis.Entity;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;
using GoldMantis.Service.Dal.DalView;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.OA;
using GoldMantis.Web.ViewModel.User;
using NHibernate;
using NHibernate.Linq;

namespace GoldMantis.Service.Biz.Biz
{
    public class VW_OAKPILeavesBiz : BaseTableBiz<VW_OAKPILeaves>
    {
        private VW_OAKPILeavesDal _dal;

        protected override IRepository<VW_OAKPILeaves> Repository
        {
            get { return _dal; }
            set { _dal = ObjectExtensions.As<VW_OAKPILeavesDal>(value); }
        }

        public ModelOAKPILeavesIndex GetModelOAKPILeavesIndex(ModelOAKPILeavesIndex model)
        {

            var condition = ExpressionConditionHelper.ExpressionCondition<VW_OAKPILeaves>.GetInstance();

            if (!String.IsNullOrEmpty(model.SearchEntity.CommonSearchCondition))
            {
                condition.Or(x => x.Code, model.SearchEntity.CommonSearchCondition,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
                condition.Or(x => x.WorkAgentName, model.SearchEntity.CommonSearchCondition,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
                condition.Or(x => x.Reason, model.SearchEntity.CommonSearchCondition,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(model.SearchEntity.Code))
                {
                    condition.And(x => x.Code, model.SearchEntity.Code,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
                }
                if (!String.IsNullOrWhiteSpace(model.SearchEntity.Reason))
                {
                    condition.And(x => x.Reason, model.SearchEntity.Reason,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
                }
                if (model.SearchEntity.LeaveType.HasValue)
                {
                    condition.And(x => x.LeaveType, model.SearchEntity.LeaveType,
                    ExpressionConditionHelper.ExpressionValueRelation.Equal);
                }

                if (model.SearchEntity.Days.HasValue)
                {
                    condition.And(x => x.Days, model.SearchEntity.Days,
                    ExpressionConditionHelper.ExpressionValueRelation.Equal);
                }
            }


            //if (string.IsNullOrEmpty(model.SearchEntity.OrderName))
            //{
            //    model.SearchEntity.OrderName = "KeyID";
            //    model.SearchEntity.OrderDirection = ((int)OrderBy.DESC).ToString();
            //}


            if (model.ExportType != null && model.ExportType == EnumExportType.导出全部)
            {
                //model.GridDataSources = this.List(condition.ConditionExpression, model.SearchEntity.OrderName, model.SearchEntity.EnumOrderDirection);
                return model;
            }

            int count = 0;
            //model.GridDataSources = PaginateList(model.SearchEntity.PageSize, model.SearchEntity.PageIndex, ref count, condition.ConditionExpression, model.SearchEntity.OrderName, model.SearchEntity.EnumOrderDirection);
            model.PaginateHelperObject = PaginateAttribute.ConstructPaginate(model.SearchEntity.PageSize, count, model.SearchEntity.PageIndex, model.SearchEntity, "SearchEntity");

            return model;
        }


        public void DeleteByKeys(int[] ids)
        {
            base.DeleteByKeys(ids);
        }

        public string Get(string chenjd)
        {
            return chenjd;
        }
    }
}
