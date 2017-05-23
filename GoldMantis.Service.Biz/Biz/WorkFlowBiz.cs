using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.User;
using GoldMantis.WorkFlow.WCFClient;
using Newtonsoft.Json;
using SCFlowMaping = GoldMantis.Common.SCFlowMaping;

namespace GoldMantis.Service.Biz.Biz
{
    public class WorkFlowBiz : BaseBiz
    {
        protected PermissionBiz _PermissionBiz = new PermissionBiz();
        protected SCFlowMapingBiz _SCFlowMapingBiz = new SCFlowMapingBiz();

        private static string cacheKeyMenuWF = "SCFlowMapingBLL/MenuFlow/{0}";
        private static Dictionary<string, Dictionary<int, List<SCFlowMaping>>> cacheManage
            = new Dictionary<string, Dictionary<int, List<SCFlowMaping>>>();


        public SAMenu GetSAMenuByID(int id)
        {
            return _PermissionBiz.GetSAMenuByID(id);
        }

        public ModelCustomFlowIndex GetModelCustomFlowIndex(ModelCustomFlowIndex model)
        {

            SAMenu menu = _PermissionBiz.GetSAMenuByID(model.MenuID);
            model.GridDataSources = new List<FlowEntityChild>();
            int recountCount;
            List<FlowEntity> flowEntities;

            using (TokenServiceClient wf = new TokenServiceClient())
            {
                flowEntities = wf.GetMyCommonFlow(out recountCount,
                    model.SearchEntity.CreateUser,
                    model.SearchEntity.WFName,
                    menu.MenuURL + (model.SearchEntity.WFNodeAuditor == null ? "" : "*" + model.SearchEntity.WFNodeAuditor),
                    model.SearchEntity.WFCreateTimeBegin,
                    model.SearchEntity.WFCreateTimeEnd,
                    model.SearchEntity.PageIndex + 1,
                    model.SearchEntity.PageSize);
            }

            foreach (var item in flowEntities)
            {
                FlowEntityChild child = new FlowEntityChild();
                child = child.AutoCopy(item);
                if (Int32.Parse(child.CreateUserId) > 0)
                {
                    child.CreateUserName = _PermissionBiz.GetSAUserByID(Int32.Parse(child.CreateUserId)).UserName;
                }

                model.GridDataSources.Add(child);
            }

            model.PaginateHelperObject = new PaginateAttribute(model.SearchEntity.PageSize, recountCount, model.SearchEntity.PageIndex, string.Empty);

            return model;
        }

        public ModelCommonFlowEntityCreate GetModelCommonFlowEntityCreate(ModelCommonFlowEntityCreate model)
        {
            CommonFlowEntity commonFlow = null;
            using (TokenServiceClient wf = new TokenServiceClient())
            {
                commonFlow = wf.GetCommonFlowContent(model.PublishID);
            }
            List<CustomFlowNode> nodes = new List<CustomFlowNode>();
            CustomFlowNode commonFlowNode;
            foreach (CommonFlowNode node in commonFlow.FlowContent)
            {
                commonFlowNode = new CustomFlowNode();
                commonFlowNode.Hour = Convert.ToInt32(node.ArrivedHours);
                commonFlowNode.IsRemind = node.NeedPush;
                commonFlowNode.Sort = node.Order;
                commonFlowNode.UserID = node.UserId;
                commonFlowNode.UserName = node.UserName;
                nodes.Add(commonFlowNode);
            }

            model.CommonFlowEntity = commonFlow;
            model.CustomFlowNodes = nodes;

            model.NodesValue = JsonConvert.SerializeObject(nodes);
            model.UserName = _PermissionBiz.GetSAUserByID(Int32.Parse(model.CommonFlowEntity.CreateUserId)).UserName;
            return model;
        }

        public ModelCommonFlowEntityCreate PostModelCommonFlowEntityCreate(ModelCommonFlowEntityCreate model)
        {

            SAMenu menu = _PermissionBiz.GetSAMenuByID(model.MenuID);
            //model.CommonFlowEntity.CreateTime = DateTime.Now;
            //model.CommonFlowEntity.CreateUserId = model.UserID.ToString();
            //model.CommonFlowEntity.FormUrl = menu.MenuURL;

            //model.CommonFlowEntity.CreateUserId = model.UserID.ToString();
            //List<CustomFlowNode> nodes = JsonConvert.DeserializeObject<List<CustomFlowNode>>(model.NodesValue);

            //if (nodes != null)
            //{
            //    foreach (CustomFlowNode node in nodes)
            //    {
            //        var commonFlowNode = new CommonFlowNode();
            //        commonFlowNode.ArrivedHours = node.Hour;
            //        commonFlowNode.NeedPush = node.IsRemind;
            //        commonFlowNode.Order = node.Sort;
            //        commonFlowNode.UserId = node.UserID;
            //        commonFlowNode.UserName = node.UserName;
            //        model.CommonFlowEntity.FlowContent.Add(commonFlowNode);
            //    }
            //}
            //Guid publishID = Guid.Empty;
            //using (TokenServiceClient wf = new TokenServiceClient())
            //{
            //    model.AddResult = wf.CreateFlow(out publishID, model.CommonFlowEntity);
            //    model.PublishID = publishID;
            //}
            //return model;

            CommonFlowEntity commonFlow = new CommonFlowEntity();
            commonFlow.CreateTime = DateTime.Now;
            commonFlow.CreateUserId = model.UserID.ToString();

            commonFlow.FormUrl = menu.MenuURL;
            commonFlow.PublishId = Guid.NewGuid();
            int nodeCount = 0;
            List<CustomFlowNode> nodes = model.CustomFlowNodes = JsonConvert.DeserializeObject<List<CustomFlowNode>>(model.NodesValue);
            if (nodes != null)
            {
                commonFlow.FlowContent = new List<CommonFlowNode>();
                CommonFlowNode commonFlowNode = null;

                commonFlowNode = new CommonFlowNode();


                #region 以下代码被注释

                //var preAuditUsers =
                //    DataAccess.Session.CreateSQLQuery("exec sp_WFRuleGetCustomizeAuditProcess ?")
                //        .SetInt32(0, model.UserID)
                //        .List();

                ////var preAuditUsers = DataAccessHelper.Query("sp_WFRuleGetCustomizeAuditProcess", new Dictionary<string, object> { { "@UserID", UserID } });

                //for (int i = 0; i < preAuditUsers.Count; i++)
                //{
                //    dynamic item = preAuditUsers[i];
                //    commonFlow.FlowContent.Add(new CommonFlowNode
                //    {
                //        ArrivedHours = 0,
                //        NeedPush = false,
                //        Order = Convert.ToInt32(item[0]),
                //        UserId = item[1].ToString(),
                //        UserName = item[2],
                //    });
                //}

                #endregion




                //foreach (var row in preAuditUsers)


                foreach (CustomFlowNode node in nodes)
                {
                    commonFlowNode = new CommonFlowNode();
                    commonFlowNode.ArrivedHours = node.Hour;
                    commonFlowNode.NeedPush = node.IsRemind;
                    commonFlowNode.Order = node.Sort;
                    commonFlowNode.UserId = node.UserID.ToString();
                    commonFlowNode.UserName = node.UserName;
                    commonFlow.FlowContent.Add(commonFlowNode);
                    commonFlow.FlowDesc += commonFlowNode.UserName + ">";
                }
                nodeCount = nodes.Count;
            }
            commonFlow.FlowDesc = commonFlow.FlowDesc.TrimEnd('>');
            commonFlow.WFName = model.SelfWFName ?? string.Format("{0}-自定义流程", menu.MenuName, nodeCount);

            Guid publishID = Guid.Empty;
            bool result = false;
            int recountCount = 0;
            using (TokenServiceClient wf = new TokenServiceClient())
            {
                if (model.SelfWFName != null)
                {

                    List<FlowEntity> flowEntities = wf.GetMyCommonFlow(out recountCount,
                        String.IsNullOrWhiteSpace(model.UserID.ToString()) ? string.Empty : model.UserID.ToString(), model.SelfWFName.Trim(), menu.MenuURL,
                        null, null, 1, 15).Where(p => p.Desc == model.SelfWFName).ToList();
                    if (recountCount != 0)
                    {
                        model.AddResult = false;
                        model.AddResultMessage = "自定义流程不允许重复创建！";
                    }
                }
                model.AddResult = wf.CreateFlow(out publishID, commonFlow);
            }

            return model;

        }

        public ModelSAUserIndex GetSAUsersByIDs(ModelSAUserIndex model)
        {
            model.GridDataSources = _PermissionBiz.GetSAUserByIDs(model.IDs);

            if (!String.IsNullOrEmpty(model.SearchEntity.CommonSearchCondition))
            {
                model.GridDataSources =
                   model.GridDataSources.Where(x => x.UserName.Contains(model.SearchEntity.CommonSearchCondition)).ToList();
            }
            else
            {
                if (!String.IsNullOrEmpty(model.SearchEntity.UserName))
                {
                    model.GridDataSources = model.GridDataSources.Where(x => x.UserName.Contains(model.SearchEntity.UserName)).ToList();
                }
            }
            if (string.IsNullOrEmpty(model.SearchEntity.OrderName))
            {
                model.SearchEntity.OrderName = "SortNo";
                model.SearchEntity.OrderDirection = ((int)OrderBy.ASC).ToString();
            }

            int count = 0;
            //导出本页数据或者不导出逻辑
            model.GridDataSources = model.GridDataSources.Take(model.SearchEntity.PageSize * (model.SearchEntity.PageIndex + 1)).Skip(model.SearchEntity.PageSize * model.SearchEntity.PageIndex).ToList();
            //分页加载
            model.PaginateHelperObject = PaginateAttribute.ConstructPaginate(model.SearchEntity.PageSize, count, model.SearchEntity.PageIndex, model.SearchEntity, "SearchEntity");
            return model;
        }

        public ModelSAUserIndex GetSAUsers(ModelSAUserIndex model)
        {
            model.GridDataSources = _PermissionBiz.GetSAUserList();

            if (!String.IsNullOrEmpty(model.SearchEntity.CommonSearchCondition))
            {
                model.GridDataSources =
                   model.GridDataSources.Where(x => 
                       x.UserName.Contains(model.SearchEntity.CommonSearchCondition)||
                       x.JobCode.Contains(model.SearchEntity.CommonSearchCondition)||
                       x.Telephone.Contains(model.SearchEntity.CommonSearchCondition) ||
                       x.Email.Contains(model.SearchEntity.CommonSearchCondition)
                       
                       
                       
                       ).ToList();
            }
            else
            {
                if (!String.IsNullOrEmpty(model.SearchEntity.UserName))
                {
                    model.GridDataSources = model.GridDataSources.Where(x => x.UserName.Contains(model.SearchEntity.UserName)).ToList();
                }
                if (!String.IsNullOrEmpty(model.SearchEntity.JobCode))
                {
                    model.GridDataSources = model.GridDataSources.Where(x => x.JobCode.Contains(model.SearchEntity.JobCode)).ToList();
                }
                if (!String.IsNullOrEmpty(model.SearchEntity.Mobile))
                {
                    model.GridDataSources = model.GridDataSources.Where(x => x.Mobile.Contains(model.SearchEntity.Mobile)).ToList();
                }
                if (!String.IsNullOrEmpty(model.SearchEntity.Email))
                {
                    model.GridDataSources = model.GridDataSources.Where(x => x.Email.Contains(model.SearchEntity.Email)).ToList();
                }
            }
            if (string.IsNullOrEmpty(model.SearchEntity.OrderName))
            {
                model.SearchEntity.OrderName = "SortNo";
                model.SearchEntity.OrderDirection = ((int)OrderBy.ASC).ToString();
            }

            int count = model.GridDataSources.Count;
            //导出本页数据或者不导出逻辑
            model.GridDataSources = model.GridDataSources.Take(model.SearchEntity.PageSize * (model.SearchEntity.PageIndex + 1)).Skip(model.SearchEntity.PageSize * model.SearchEntity.PageIndex).ToList();
            //分页加载
            model.PaginateHelperObject = PaginateAttribute.ConstructPaginate(model.SearchEntity.PageSize, count, model.SearchEntity.PageIndex, model.SearchEntity, "SearchEntity");
            return model;
        }

        /// <summary>
        /// 退回时选择节点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ModelNodeIndex MutualSelectNodeIndex(ModelNodeIndex model)
        {
            if (!String.IsNullOrEmpty(model.SearchEntity.CommonSearchCondition))
            {
                model.GridDataSources =
                    model.GridDataSources.Where(x => x.DoUserName.Contains(model.SearchEntity.CommonSearchCondition)).ToList();
            }
            else
            {
                if (!String.IsNullOrEmpty(model.SearchEntity.NodeName))
                {
                    model.GridDataSources =
                     model.GridDataSources.Where(x => x.DoUserName.Contains(model.SearchEntity.NodeName)).ToList();
                }
            }

            if (string.IsNullOrEmpty(model.SearchEntity.OrderName))
            {
                model.SearchEntity.OrderName = "DoTime";
                model.SearchEntity.OrderDirection = ((int)OrderBy.ASC).ToString();
            }


            int count = model.GridDataSources.Count;

            model.GridDataSources = model.GridDataSources.Take(model.SearchEntity.PageSize * (model.SearchEntity.PageIndex + 1)).Skip(model.SearchEntity.PageSize * model.SearchEntity.PageIndex).ToList();
            model.PaginateHelperObject = new PaginateAttribute(model.SearchEntity.PageSize, count, model.SearchEntity.PageIndex, string.Empty);

            return model;
        }

        /// <summary>
        /// 下一环节有多个时，使用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ModelSelectNextIndex MutualSelectNextIndex(ModelSelectNextIndex model)
        {
            if (!String.IsNullOrEmpty(model.SearchEntity.CommonSearchCondition))
            {
                model.GridDataSources =
                    model.GridDataSources.Where(x => x.NodeName.Contains(model.SearchEntity.CommonSearchCondition)).ToList();
            }
            else
            {
                if (!String.IsNullOrEmpty(model.SearchEntity.NodeName))
                {
                    model.GridDataSources =
                     model.GridDataSources.Where(x => x.NodeName.Contains(model.SearchEntity.NodeName)).ToList();
                }
            }

            if (string.IsNullOrEmpty(model.SearchEntity.OrderName))
            {
                model.SearchEntity.OrderName = "DoTime";
                model.SearchEntity.OrderDirection = ((int)OrderBy.ASC).ToString();
            }


            int count = model.GridDataSources.Count;

            model.GridDataSources = model.GridDataSources.Take(model.SearchEntity.PageSize * (model.SearchEntity.PageIndex + 1)).Skip(model.SearchEntity.PageSize * model.SearchEntity.PageIndex).ToList();
            model.PaginateHelperObject = new PaginateAttribute(model.SearchEntity.PageSize, count, model.SearchEntity.PageIndex, string.Empty);

            return model;
        }

        public ModelSCFlowMapingIndex GetModelChooseWorkFlowIndex(ModelSCFlowMapingIndex model)
        {
            model.GridDataSources = GetSCFlowMapingByMenuIDAndDeptID(model.MenuID, model.DeptID);
            if (!String.IsNullOrEmpty(model.SearchEntity.CommonSearchCondition))
            {
                model.GridDataSources =
                    model.GridDataSources.Where(x => x.WFName.Contains(model.SearchEntity.CommonSearchCondition)).ToList();

            }
            else
            {
                if (!String.IsNullOrEmpty(model.SearchEntity.WFName))
                {
                    model.GridDataSources =
                     model.GridDataSources.Where(x => x.WFName.Contains(model.SearchEntity.WFName)).ToList();
                }

            }
            if (string.IsNullOrEmpty(model.SearchEntity.OrderName))
            {
                model.SearchEntity.OrderName = "ID";
                model.SearchEntity.OrderDirection = ((int)OrderBy.DESC).ToString();
            }

            int count = model.GridDataSources.Count;

            model.GridDataSources = model.GridDataSources.Take(model.SearchEntity.PageSize * (model.SearchEntity.PageIndex + 1)).Skip(model.SearchEntity.PageSize *

model.SearchEntity.PageIndex).ToList();
            model.PaginateHelperObject = new PaginateAttribute(model.SearchEntity.PageSize, count, model.SearchEntity.PageIndex, string.Empty);

            return model;
        }

        public List<SCFlowMaping> GetSCFlowMapingByMenuIDAndDeptID(int menuID, int deptID)
        {

            var dic = cacheManage.FirstOrDefault(x => x.Key == string.Format(cacheKeyMenuWF, menuID)).Value;

            if (dic == null)
            {
                dic = new Dictionary<int, List<SCFlowMaping>>();
                IList<SCFlowMaping> mapingList = _SCFlowMapingBiz.GetSCFlowMapingByMenuID(menuID);

                Dictionary<int, SCDepartment> dicDept = _SCFlowMapingBiz.GetSCDepartmentDictionary();
                int tempDeptID = 0;
                //设置默认的流程
                foreach (SCFlowMaping mapEntity in mapingList.Where(p => p.DeptID == null || p.DeptID.Equals(0)))
                {
                    if (dic.ContainsKey(0))
                    {
                        dic[0].Add(mapEntity);
                    }
                    else
                    {
                        dic.Add(0, new List<SCFlowMaping>() { mapEntity });
                    }
                }
                if (dic.ContainsKey(0))
                {
                    //按照流程类别来 选取部门定制的流程
                    foreach (SCFlowMaping entity in dic[0])
                    {
                        //查看这些流程中是否含有父子关系
                        //如果有父子关系的，父亲只能访
                        var v = (from a in mapingList
                                 where a.ParentID.Equals(entity.ID)
                                 select a).ToList();
                        foreach (SCFlowMaping mapEntity in v)
                        {
                            tempDeptID = mapEntity.DeptID;
                            //设置本部门流程
                            if (dic.ContainsKey(tempDeptID))
                            {
                                dic[tempDeptID].Add(mapEntity);
                            }
                            else
                            {
                                dic.Add(tempDeptID, new List<SCFlowMaping>() { mapEntity });
                            }
                            //找出自己的子孙
                            var w = from s in v
                                    where dicDept[s.DeptID].RootPath.StartsWith(dicDept[tempDeptID].RootPath + ",")
                                    select s;

                            //设置子孙应用此流程的流程
                            foreach (SCDepartment dept
                                in dicDept.Values.ToList().Where(p => p.RootPath.StartsWith(dicDept[tempDeptID].RootPath + ",")))
                            {
                                //当子孙独立时，不覆盖其包括其后代                           
                                if (w.Where(p =>
                                {
                                    return dicDept[dept.ID].RootPath.StartsWith(dicDept[p.DeptID].RootPath + ",") || p.DeptID.Equals(dept.ID);
                                }).Count() > 0)
                                {
                                    continue;
                                }
                                if (dic.ContainsKey(dept.ID))
                                {
                                    dic[dept.ID].Add(mapEntity);
                                }
                                else
                                {
                                    dic.Add(dept.ID, new List<SCFlowMaping>() { mapEntity });
                                }
                            }
                        }
                    }

                    //找到部门专用流程
                    foreach (SCFlowMaping mapEntity in mapingList.Where(p => p.DeptID > 0 && p.ParentID == null))
                    {
                        List<int> tempDeptIds = GetChildDeptmentsByDept(mapEntity.DeptID.ToInt32());
                        tempDeptIds.Add(mapEntity.DeptID.ToInt32());

                        if (tempDeptIds.Contains(mapEntity.DeptID.ToInt32()))
                        {
                            foreach (int subDeptId in tempDeptIds)
                            {
                                if (dic.ContainsKey(subDeptId))
                                {
                                    dic[subDeptId].Add(mapEntity);
                                }
                                else
                                {
                                    dic.Add(subDeptId, new List<SCFlowMaping>() { mapEntity });
                                }
                            }
                        }
                    }
                }
                else
                    foreach (SCFlowMaping mapEntity in mapingList.Where(p => p.DeptID > 0 && p.ParentID == null))
                    {
                        List<int> tempDeptIds = GetChildDeptmentsByDept(mapEntity.DeptID.ToInt32());
                        tempDeptIds.Add(mapEntity.DeptID.ToInt32());

                        foreach (int subDeptId in tempDeptIds)
                            if (dic.ContainsKey(subDeptId))
                                dic[subDeptId].Add(mapEntity);
                            else
                                dic.Add(subDeptId, new List<SCFlowMaping> { mapEntity });
                    }

                cacheManage.Add(string.Format(cacheKeyMenuWF, menuID), dic);


            }

            var result = new List<SCFlowMaping>();
            if (dic.ContainsKey(deptID)
                && !deptID.Equals(0))//取自己部门值
            {

                dic[deptID].ForEach(p => result.Add(p));

                if (dic.ContainsKey(0))
                {
                    //需要拼装一下
                    List<SCFlowMaping> qlist = (from r in result
                                                join d in dic[0] on r.ParentID equals d.ID
                                                select d).ToList();

                    foreach (SCFlowMaping entity in dic[0])
                    {
                        if (!qlist.Contains(entity)
                            && !result.Contains(entity))
                        {
                            result.Add(entity);
                        }
                    }
                }
            }
            else //取默认值
            {
                result = dic.ContainsKey(0) ? dic[0] : result;
            }

            return result;
        }

        public List<int> GetChildDeptmentsByDept(int deptID)
        {
            var dic = _SCFlowMapingBiz.GetSCDepartmentDictionary();
            return dic.Values.ToList().Where(p => p.RootPath.StartsWith(dic[deptID].RootPath + ",")).Select(dept => dept.ID).ToList();
        }

    }
}
