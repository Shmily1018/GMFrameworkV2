using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel;
using GoldMantis.Common;
using GoldMantis.DI;
using GoldMantis.Entity;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;
using GoldMantis.Web.Core.Session;
using GoldMantis.Web.HtmlControl;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.User;
using GoldMantis.WorkFlow.WCFClient;
using NHibernate;

namespace GoldMantis.Service.Biz.Biz
{
    public class DemoBiz : BaseTableBiz<Demo>
    {
        private DemoDal _dal;
        private SAAttachmentBiz bizSAAttachment
            = new SAAttachmentBiz();

        private DalSAAttachment dalSAAttachment;

        protected override IRepository<Demo> Repository
        {
            get { return _dal; }
            set { _dal = value.As<DemoDal>(); }
        }

        public ModelDemoIndex GetModelDemoIndex(ModelDemoIndex model)
        {
            var condition = ExpressionConditionHelper.ExpressionCondition<Demo>.GetInstance();
            if (!String.IsNullOrEmpty(model.SearchEntity.CommonSearchCondition))
            {

                condition.Or(x => x.ID, model.SearchEntity.CommonSearchCondition,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
                condition.Or(x => x.Name, model.SearchEntity.CommonSearchCondition,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);

            }
            else
            {
                if (!String.IsNullOrEmpty(model.SearchEntity.Name))
                {
                    condition.And(x => x.Name, model.SearchEntity.Name,
                    ExpressionConditionHelper.ExpressionValueRelation.Like);
                }

            }
            if (string.IsNullOrEmpty(model.SearchEntity.OrderName))
            {
                model.SearchEntity.OrderName = "ID";
                model.SearchEntity.OrderDirection = ((int)OrderBy.DESC).ToString();
            }

            int count = 0;
            ////导出全部数据
            //if (model.ExportObject != null && model.ExportObject.ExportType == ExportType.导出全部)
            //{
            //    model.GridDataSources = this.ListBy(condition.ConditionExpression,
            //       model.SearchEntity._OrderName,
            //       model.SearchEntity.EnumOrderDirection);
            //    return model;
            //}

            //导出本页数据或者不导出逻辑
            model.GridDataSources = PaginateList(model.SearchEntity.PageSize, model.SearchEntity.PageIndex, ref count, condition.ConditionExpression, model.SearchEntity.OrderName, model.SearchEntity.EnumOrderDirection);
            //分页加载
            model.PaginateHelperObject = PaginateAttribute.ConstructPaginate(model.SearchEntity.PageSize, count, model.SearchEntity.PageIndex, model.SearchEntity, "SearchEntity");
            return model;
        }

        public ModelDemoCreate GetModelDemoCreate(ModelDemoCreate model)
        {

            #region "附件列表上传操作"
            if (model.FileUploadHelperObject == null)
            {
                model.FileUploadHelperObject = new ModelFileUpload() { FileKey = Guid.NewGuid() };
            }

            if (model.Demo != null && model.Demo.ID != 0)
            {
                model.FileUploadHelperObject.AttachmentList = bizSAAttachment.GetAttachmentList(model.Demo.ID, "Demo");
            }
            #endregion


            if (model.Demo != null && model.Demo.ID != 0)
            {
                model.Demo = this.GetByKey<Int32>(model.Demo.ID);
            }

            //model的其他属性赋值
            return model;
        }


        private void StartWorkFlow(Demo entity)
        {
            TransitionCallbackHandler wfCall = new TransitionCallbackHandler();

            wfCall.Started += delegate(object sender, WorkflowEventArgs e)
            {
                string msg1 = string.Empty;
                if (!OnWorkflowCompleted(out msg1, e, entity.ID))
                {
                    //ToDo 记日志 
                }
            };
            wfCall.Completed += delegate(object sender, WorkflowEventArgs e)
            {
                string msg1 = string.Empty;
                if (!OnWorkflowCompleted(out msg1, e, entity.ID))
                {
                    // UIHelper.Alert(upProcessButton, string.Format("审批已经通过，但审核失败！原因:{0}", msg1));
                    //ToDo 记日志，支付失败
                }
                else
                {
                    //    UIHelper.Alert(upProcessButton, string.Format("审批已经通过，审核成功！{0}", msg1));
                }
            };


            //wfCall.onFilterUsers = new TransitionCallbackHandler.OnFilterUsers(authPage.FilterUsers);

            MessageModel wfMsg = null;

            using (TransitionServiceClient wf = new TransitionServiceClient(new InstanceContext(wfCall)))
            {
                StartModel start = CreateStartModel(entity, CreateWorkFlowExtend(entity));

                wfMsg = wf.Start(start);
                if (wfMsg.Actors != null)
                {
                    wfMsg = wf.StartByInputValue(wfMsg.Actors, string.Empty, start);
                }

                entity.Age = 10000;
                DataAccess.Update(entity);
            }

        }
        private StartModel CreateStartModel(Demo entity, WorkFlowExtend extend)
        {
            return new StartModel()
            {
                BusinessId = entity.ID.ToString(),
                WFPublishId = new Guid("BEC2F384-5BE7-4EFA-A2C6-512BC59C5BD4"),
                UserId = "12388",
                Priority = TokenPriority.Normal,
                ExtendInfo = extend.ToString(),
                WFTokenName = entity.Name
            };
        }
        private WorkFlowExtend CreateWorkFlowExtend(Demo entity)
        {
            return new WorkFlowExtend()
            {
                ID = entity.ID.ToString(),
                ProjectName = "金东房地产商业综合体",
                ProjectId = 4690,
                DeptName = "软件技术部",
                DeptId = 110,
                MenuId = 14663, //MenuID需要替换
                PageClass = "Demo",
                WFType = 3
            };
        }

        public class WorkFlowExtend
        {
            public WorkFlowExtend()
            {
                ProjectName = string.Empty;
                DeptName = string.Empty;
                ContactUnitName = string.Empty;
                WorkGroupName = string.Empty;
                MaterialTypeName = string.Empty;
                PageClass = string.Empty;
                PersonName = string.Empty;
                ID = string.Empty;
                Amount = 0;
            }
            /// <summary>
            /// 项目ID
            /// </summary>
            public int ProjectId
            {
                get;
                set;
            }

            /// <summary>
            /// 项目名称
            /// </summary>
            public string ProjectName
            {
                get;
                set;
            }

            /// <summary>
            /// 部门名称
            /// </summary>
            public string DeptName
            {
                get;
                set;
            }

            /// <summary>
            /// 部门ID
            /// </summary>
            public int DeptId
            {
                get;
                set;
            }

            /// <summary>
            /// 往来单位
            /// </summary>
            public string ContactUnitName
            {
                get;
                set;
            }
            /// <summary>
            /// 班组名称
            /// </summary>
            public string WorkGroupName
            {
                get;
                set;
            }

            public string MaterialTypeName
            {
                get;
                set;
            }
            /// <summary>
            /// 人名
            /// </summary>
            public string PersonName
            {
                get;
                set;
            }

            /// <summary>
            /// 金额
            /// </summary>
            public decimal Amount
            {
                get;
                set;
            }

            /// <summary>
            /// 页面MenuId
            /// </summary>
            public int MenuId
            {
                get;
                set;
            }

            /// <summary>
            /// 模块
            /// </summary>
            public int WFType
            {
                get;
                set;
            }

            /// <summary>
            ///表单流水号
            /// </summary>
            public string ID
            {
                get;
                set;
            }

            /// <summary>
            /// 页面对应的数据库表名
            /// </summary>
            public string PageClass
            {
                get;
                set;
            }

            protected NameValueCollection FormParamList
            {
                get;
                set;
            }

            public override string ToString()
            {
                string extendInfo = string.Empty;
                extendInfo = "ProjectName:" + ProjectName + ";"
                    + "DeptName:" + DeptName + ";"
                    + "ContactUnitName:" + ContactUnitName.ToString() + ";"
                    + "Amount:" + (Amount == 0 ? "0" : Amount.ToString()) + ";"
                    + "MenuId:" + (MenuId == 0 ? "" : MenuId.ToString()) + ";"
                    + "WFType:" + WFType.ToString() + ";"
                    + "ProjectId:" + (ProjectId == 0 ? "" : ProjectId.ToString()) + ";"
                    + "DeptId:" + (DeptId == 0 ? "0" : DeptId.ToString()) + ";"
                    + "PersonName:" + PersonName.ToString() + ";"
                    + "ID:" + ID;

                if (FormParamList != null)
                {
                    foreach (string key in FormParamList.Keys)
                    {
                        if (extendInfo.IndexOf(key) == -1)
                            extendInfo += ";" + key + ":" + FormParamList[key];
                    }
                }

                return extendInfo;
            }


            public static string BuildMemo(string extendInfos)
            {
                string extendInfo = string.Empty;
                string[] infos = extendInfos.Split(';');
                if (infos.Length > 1)
                {
                    string[] namevalue = null;
                    if (infos.Length > 0 && !string.IsNullOrEmpty(infos[0]))
                    {
                        namevalue = infos[0].Split(':');
                        if (!string.IsNullOrEmpty(namevalue[1]))
                            extendInfo = "项目:" + namevalue[1];
                    }

                    if (infos.Length > 1 && !string.IsNullOrEmpty(infos[1]))
                    {
                        namevalue = infos[1].Split(':');
                        if (!string.IsNullOrEmpty(namevalue[1]))
                        {
                            if (!string.IsNullOrEmpty(extendInfo))
                                extendInfo += "；部门:" + namevalue[1];
                            else
                                extendInfo += "部门:" + namevalue[1];
                        }
                    }

                    if (infos.Length > 2 && !string.IsNullOrEmpty(infos[2]))
                    {
                        namevalue = infos[2].Split(':');
                        if (!string.IsNullOrEmpty(namevalue[1]))
                        {
                            if (!string.IsNullOrEmpty(extendInfo))
                                extendInfo += "；往来单位:" + namevalue[1];
                            else
                                extendInfo += "往来单位:" + namevalue[1];
                        }
                    }

                    if (infos.Length > 3 && !string.IsNullOrEmpty(infos[3]))
                    {
                        namevalue = infos[3].Split(':');
                        if (!string.IsNullOrEmpty(namevalue[1]))
                        {
                            if (!string.IsNullOrEmpty(extendInfo) && namevalue[0].Trim() == "Amount")
                                extendInfo += "；金额:" + Convert.ToDecimal(namevalue[1]).ToString();
                            else if (namevalue[0].Trim() == "Amount")
                                extendInfo += "金额:" + Convert.ToDecimal(namevalue[1]).ToString();
                        }
                    }

                    if (infos.Length > 9 && !string.IsNullOrEmpty(infos[8]))
                    {
                        namevalue = infos[8].Split(':');
                        if (!string.IsNullOrEmpty(namevalue[1]) && namevalue[0].Trim() == "PersonName")
                        {
                            if (!string.IsNullOrEmpty(extendInfo))
                                extendInfo += "；姓名:" + namevalue[1];
                            else
                                extendInfo += "姓名:" + namevalue[1];
                        }
                    }
                }

                return extendInfo;
            }

            /// <summary>
            /// 获取扩展字段
            /// </summary>
            /// <param name="extendInfos"></param>
            /// <param name="extendInfoName">字段名称不区分大小写</param>
            /// <returns></returns>
            public static string GetExtendInfo(string extendInfos, string extendInfoName)
            {
                string result = string.Empty;
                string[] infos = extendInfos.Split(';');
                foreach (string row in infos)
                {
                    string[] namevalue = null;
                    namevalue = row.Split(':');
                    if (namevalue.Length > 1 && namevalue[0].ToLower() == extendInfoName)
                    {
                        result = namevalue[1];
                    }
                }
                return result;
            }
            /// <summary>
            /// 添加页面采集信息
            /// </summary>
            /// <param name="fieldName">字段名</param>
            /// <param name="fieldValue">字段值</param>
            /// <param name="isPersisent">是否持久化</param>
            public WorkFlowExtend AddFormInfo(string fieldName, string fieldValue, bool isPersisent)
            {
                if (FormParamList == null)
                {
                    FormParamList = new NameValueCollection();
                }

                if (FormParamList.GetValues(fieldName) == null)
                {
                    FormParamList.Add(fieldName, fieldValue);
                }

                return this;
            }

            /// <summary>
            /// 移除页面采集信息
            /// </summary>
            /// <param name="fieldName"></param>
            public void RemoveFormInfo(string fieldName)
            {
                if (FormParamList != null)
                {
                    FormParamList.Remove(fieldName);
                }

            }

        }

        public bool OnWorkflowCompleted(out string message, WorkflowEventArgs e,
                                                 int keyID)
        {
            int checkstatus = e.CheckStatus.GetHashCode();
            message = string.Empty;


            return true;
        }

        public ModelDemoCreate PostModelDemoCreate(ModelDemoCreate model)
        {
            ITransaction tx = DataAccess.Session.BeginTransaction();

            using (tx)
            {
                DataAccess.SaveOrUpdate(model.Demo);


                int recordID = model.Demo.ID;

                #region 保存附件的操作


                IList<Attachment> oldAttachment = bizSAAttachment.GetAttachmentList(model.Demo.ID, "Demo");

                foreach (var item in oldAttachment)
                {
                    //被删除的附件
                    if (model.FileUploadHelperObject.AttachmentList == null ||
                        model.FileUploadHelperObject.AttachmentList.FirstOrDefault(
                            x => x.ID == item.ID) == null)
                    {
                        dalSAAttachment.DeleteByKey(item.ID, NHibernateUtil.Int32);
                    }
                }

                if (model.FileUploadHelperObject.AttachmentList != null &&
                    model.FileUploadHelperObject.AttachmentList.Count > 0)
                {
                    foreach (var item in model.FileUploadHelperObject.AttachmentList)
                    {
                        SAAttachment sysAttachment = dalSAAttachment.GetByKey(item.ID);
                        if (sysAttachment == null)
                        {
                            sysAttachment = new SAAttachment()
                            {
                                BillID = recordID,
                                DocEncryptPath = String.Empty,
                                DocName = item.Name,
                                DocPath = item.Path,
                                DocSize = item.FileSize,
                                TableName = "Demo"
                            };
                            dalSAAttachment.SaveOrUpdate(sysAttachment);
                        }
                    }
                }

                #endregion

                tx.Commit();
            }



            return model;
        }

        public void DeleteByKey(int id)
        {
            this.DeleteByKey(id, NHibernateUtil.Int64);
        }

        public void DeleteByKeys(int[] ids)
        {
            base.DeleteByKeys(ids);
        }

        public String GetDigramUrl(Guid guid)
        {
            Guid WFPID = new Guid("dffb79d9-abfa-49b6-a0bb-7f7d3ca2fc85");
            //Guid WFPID = guid;
            string imgUrl = string.Empty;
            FlowEntity flowEntity = null;

            using (TokenServiceClient wf = new TokenServiceClient())
            {
                flowEntity = wf.GetFlowByWfId(WFPID);

            }
            if (flowEntity != null)
            {
                if (flowEntity.IsCommon)
                {

                    //ifFlow.InnerHtml = flowEntity.Desc;
                    //plImg.Visible = false;

                }
                else
                {
                    //plImg.Visible = true;
                    //ifFlow.Visible = false;

                    string imgFilePath = string.Format("{0}\\TEMP\\{1}.png", AppDomain.CurrentDomain.BaseDirectory, WFPID);
                    DateTime modifyDate = System.IO.File.GetLastWriteTime(imgFilePath);
                    imgUrl = string.Format("/Temp/{0}.png", WFPID);
                    using (DiagramServiceClient client = new DiagramServiceClient())
                    {
                        DiagramImage img = client.RetrieveImage(modifyDate, WFPID, string.Empty);
                        if (img != null && img.ImageByte != null)
                        {
                            System.IO.File.WriteAllBytes(imgFilePath, img.ImageByte);
                            System.IO.File.SetLastWriteTime(imgFilePath, img.ModifyDate);
                        }
                    }
                }

            }
            return imgUrl;
        }

        /// <summary>
        /// 根据ID设置审批状态
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="checkStatus"></param>
        /// <returns></returns>
        public bool Checked(int keyID, int checkStatus)
        {
            var session = DataAccess.Session;
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var demo = session.Get<Demo>(keyID);
                    demo.CheckStatus = checkStatus;
                    session.Update(demo);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return true;
        }


        /// <summary>
        /// 审核结果
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Post(int keyID, out string message)
        {
            message = string.Empty;
            var session = DataAccess.Session;
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var demo = session.Get<Demo>(keyID);
                    if (demo.IsPost)
                    {
                        message = "已经审核，不可操作！";
                        return false;
                    }

                    demo.PostDate = DateTime.Now;
                    demo.IsPost = true;
                    session.Update(demo);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return true;
        }

        

        

    }
}