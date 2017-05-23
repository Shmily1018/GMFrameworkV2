using System;
using System.Collections.Generic;
using System.Linq;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Entity;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;
using GoldMantis.Service.Dal.DalView;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.Home;
using GoldMantis.Web.ViewModel.OA;
using GoldMantis.Web.ViewModel.Permission;
using GoldMantis.Web.ViewModel.User;
using NHibernate;
using NHibernate.Linq;

namespace GoldMantis.Service.Biz.Biz
{
    public class VW_UserProfileBiz : BaseTableBiz<VW_UserProfile>
    {
        private VW_UserProfileDal _dal;

        protected override IRepository<VW_UserProfile> Repository
        {
            get { return _dal; }
            set { _dal = ObjectExtensions.As<VW_UserProfileDal>(value); }
        }

        public IList<VW_UserProfile> ListByLoginID(string LoginID)
        {
            return base.List(x => x.LoginID == LoginID);
        }

        public IList<VW_UserProfile> ListByKeyID(int keyID)
        {
            return base.List(x => x.KeyID == keyID);
        }

        public ModelVW_UserProfileIndex GetModelVW_OAKPILeavesIndex(ModelVW_UserProfileIndex model)
        {
            var condition = ExpressionConditionHelper.ExpressionCondition<VW_UserProfile>.GetInstance();
            if (!String.IsNullOrEmpty(model.SearchEntity.CommonSearchCondition))
            {
                condition.Or(x => x.JobCode, model.SearchEntity.CommonSearchCondition,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);

                condition.Or(x => x.UserName, model.SearchEntity.CommonSearchCondition,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);

                condition.Or(x => x.PositionName, model.SearchEntity.CommonSearchCondition,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);

                condition.Or(x => x.DeptName, model.SearchEntity.CommonSearchCondition,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);

                condition.Or(x => x.LoginID, model.SearchEntity.CommonSearchCondition,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(model.SearchEntity.JobCode))
                {
                    condition.And(x => x.JobCode, model.SearchEntity.JobCode,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
                }

                if (!String.IsNullOrWhiteSpace(model.SearchEntity.UserName))
                {
                    condition.And(x => x.UserName, model.SearchEntity.UserName,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
                }

                if (!String.IsNullOrWhiteSpace(model.SearchEntity.PositionName))
                {
                    condition.And(x => x.PositionName, model.SearchEntity.PositionName,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
                }

                if (!String.IsNullOrWhiteSpace(model.SearchEntity.DeptName))
                {
                    condition.And(x => x.DeptName, model.SearchEntity.DeptName,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
                }

            }
            if (string.IsNullOrEmpty(model.SearchEntity.OrderName))
            {
                model.SearchEntity.OrderName = "KeyID";
                model.SearchEntity.OrderDirection = ((int)OrderBy.DESC).ToString();
            }

            int count = 0;


            model.GridDataSources = PaginateList(model.SearchEntity.PageSize, model.SearchEntity.PageIndex, ref count, condition.ConditionExpression, model.SearchEntity.OrderName, model.SearchEntity.EnumOrderDirection);
            //分页加载
            model.PaginateHelperObject = PaginateAttribute.ConstructPaginate(model.SearchEntity.PageSize, count, model.SearchEntity.PageIndex, model.SearchEntity, "SearchEntity");
            return model;
        }


    }
}
