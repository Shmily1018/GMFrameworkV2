using System;
using System.Collections.Generic;
using System.Linq;

using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.OA;
 
namespace GoldMantis.Service.Biz.Biz
{
    /// <summary>
    /// 看房者信息管理
    /// </summary>
    public class UTIPARKCUSTOMERVisitorBiz:BaseTableBiz<UTIPARKCUSTOMERVisitor>{
        
        private UTIPARKCUSTOMERVisitorDal _dal;
        
        protected override IRepository<UTIPARKCUSTOMERVisitor> Repository
        {
            get { return _dal; } 
            set { _dal = ObjectExtensions.As<UTIPARKCUSTOMERVisitorDal>(value); }
        }
        

        public void DeleteByKeys(Int64[] ids)
        {
            base.DeleteByKeys(ids);
        }
        
        public ModelUTIPARKCUSTOMERVisitorIndex GetModelUTIPARKCUSTOMERVisitorIndex(ModelUTIPARKCUSTOMERVisitorIndex model)
        {
           var condition = ExpressionConditionHelper.ExpressionCondition<UTIPARKCUSTOMERVisitor>.GetInstance();

        
                                //if (!String.IsNullOrEmpty(model.SearchEntity.CommonSearchCondition))
                                //{
                                //    condition.Or(x => x.Code, model.SearchEntity.CommonSearchCondition,
                                //        ExpressionConditionHelper.ExpressionValueRelation.Like);
                                //}
                                //else
                                //{
                                //    if (!String.IsNullOrWhiteSpace(model.SearchEntity.Code))
                                //    {
                                //        condition.And(x => x.Code, model.SearchEntity.Code,
                                //        ExpressionConditionHelper.ExpressionValueRelation.Like);
                                //    }
                                //}
        
                                //if (string.IsNullOrEmpty(model.SearchEntity.OrderName))
                                //{
                                //   model.SearchEntity.OrderName = "ID" ;
                                //   model.SearchEntity.OrderDirection = ((int)OrderBy.DESC).ToString();
                                //}
                                                
                
            if (model.ExportType != null && model.ExportType == EnumExportType.导出全部)
            {
                model.GridDataSources = this.List(condition.ConditionExpression,
                   model.SearchEntity.OrderName,
                   model.SearchEntity.EnumOrderDirection);
                return model;
            }
            
            int count = 0;
            model.GridDataSources = PaginateList(model.SearchEntity.PageSize, model.SearchEntity.PageIndex, ref count, condition.ConditionExpression, model.SearchEntity.OrderName, model.SearchEntity.EnumOrderDirection);
            model.PaginateHelperObject = PaginateAttribute.ConstructPaginate(model.SearchEntity.PageSize, count, model.SearchEntity.PageIndex, model.SearchEntity, "SearchEntity");
            
            return model;
        }

        public ModelUTIPARKCUSTOMERVisitorCreate GetModelUTIPARKCUSTOMERVisitorCreate(ModelUTIPARKCUSTOMERVisitorCreate model)
        {
            if (model.UTIPARKCUSTOMERVisitor != null && model.UTIPARKCUSTOMERVisitor.VisitorID != 0)
            {
                model.UTIPARKCUSTOMERVisitor = this.GetByKey<Int64>(model.UTIPARKCUSTOMERVisitor.VisitorID);
            }
            return model;
        }

        public ModelUTIPARKCUSTOMERVisitorCreate PostModelUTIPARKCUSTOMERVisitorCreate(ModelUTIPARKCUSTOMERVisitorCreate model)
        {
           this.SaveOrUpdate(model.UTIPARKCUSTOMERVisitor);
           return model;
        }

    }
}
