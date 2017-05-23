using System;
using System.Collections.Generic;
using System.Linq;
using GoldMantis.Common;
using GoldMantis.Common.Const;
using GoldMantis.Common.Data;
using GoldMantis.Entity;
using GoldMantis.Entity;
using GoldMantis.Service.Dal;
using GoldMantis.Service.Dal.Dal;
using GoldMantis.Web.ViewModel;
using GoldMantis.Web.ViewModel.User;
using NHibernate.Linq;
using NHibernate.Mapping;
using NHibernate.Transform;

namespace GoldMantis.Service.Biz.Biz
{
    public class SCFlowMapingBiz :BaseBiz
    {
        static string cacheKeyMenuWF = "SCFlowMapingBLL/MenuFlow/{0}";
        private static Dictionary<string, Dictionary<int, List<SCFlowMaping>>> cacheManage = new Dictionary<string, Dictionary<int, List<SCFlowMaping>>>();


//        public ModelSCFlowMapingIndex GetModelChooseWorkFlowIndex(ModelSCFlowMapingIndex model)
//        {
//            model.GridDataSources = this.GetSCFlowMapingByMenuIDAndDeptID(model.MenuID, model.DeptID);
//            if (!String.IsNullOrEmpty(model.SearchEntity.CommonSearchCondition))
//            {
//                model.GridDataSources =
//                    model.GridDataSources.Where(x => x.WFName.Contains(model.SearchEntity.CommonSearchCondition)).ToList();

//            }
//            else
//            {
//                if (!String.IsNullOrEmpty(model.SearchEntity.WFName))
//                {
//                    model.GridDataSources =
//                     model.GridDataSources.Where(x => x.WFName.Contains(model.SearchEntity.WFName)).ToList();
//                }

//            }
//            if (string.IsNullOrEmpty(model.SearchEntity.OrderName))
//            {
//                model.SearchEntity.OrderName = "ID";
//                model.SearchEntity.OrderDirection = ((int)OrderBy.DESC).ToString();
//            }

//            int count = model.GridDataSources.Count;

//            model.GridDataSources = model.GridDataSources.Take(model.SearchEntity.PageSize * (model.SearchEntity.PageIndex + 1)).Skip(model.SearchEntity.PageSize *

//model.SearchEntity.PageIndex).ToList();
//            model.PaginateHelperObject = new PaginateAttribute(model.SearchEntity.PageSize, count, model.SearchEntity.PageIndex, string.Empty);

//            return model;
//        }


        public List<SCFlowMaping> GetMenuWFFromCache(int menuID, int deptID, int keyID, int checkstatus)
        {
            List<SCFlowMaping> deptFlows = GetSCFlowMapingByMenuIDAndDeptID(menuID, deptID);
            int count = deptFlows.Count;

            if (keyID == 0 || checkstatus == 0)
            {
                deptFlows = deptFlows.Where(m => !m.IsDelete).ToList();
            }


            if (count > 0 && deptFlows.Count == 0 && deptID > 0)
            {
                deptFlows = GetSCFlowMapingByMenuIDAndDeptID(menuID, 0);

                if (keyID == 0 || checkstatus == 0)
                {
                    deptFlows = deptFlows.Where(m => !m.IsDelete).ToList();
                }
            }

            return deptFlows;
        }

        public List<SCFlowMaping> GetSCFlowMapingByMenuIDAndDeptID(int menuID, int deptID)
        {

            var dic = cacheManage.FirstOrDefault(x => x.Key == string.Format(cacheKeyMenuWF, menuID)).Value;

            if (dic == null)
            {
                dic = new Dictionary<int, List<SCFlowMaping>>();

                IList<SCFlowMaping> mapingList = GetSCFlowMapingByMenuID(menuID);

                var dicDept = GetSCDepartmentDictionary();
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

            List<SCFlowMaping> result = new List<SCFlowMaping>();
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

        public SCFlowMaping GetSCFlowMapingByID(int id)
        {
            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  ID ,
                                        MenuID ,
                                        Url ,
                                        WFName ,
                                        DeptID ,
                                        WFPID ,
                                        ParentID ,
                                        AllowCustomFlow ,
                                        IsOn ,
                                        IsDelete
                                FROM    SCFlowMaping WHERE ID=? ";
            using (session)
            {
                return session.CreateSQLQuery(sqlString).SetInt32(0, id)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SCFlowMaping))).List<SCFlowMaping>().FirstOrDefault();
            }
        }

        public IList<SCFlowMaping> GetSCFlowMapingByMenuID(int menuID)
        {
            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  ID ,
                                        MenuID ,
                                        Url ,
                                        WFName ,
                                        DeptID ,
                                        WFPID ,
                                        ParentID ,
                                        AllowCustomFlow ,
                                        IsOn ,
                                        IsDelete
                                FROM    SCFlowMaping WHERE MenuID=? ";
            using (session)
            {
                return session.CreateSQLQuery(sqlString).SetInt32(0, menuID)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SCFlowMaping))).List<SCFlowMaping>();
            }
        }

        public IList<SCFlowMaping> GetSCFlowMapingList()
        {
            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  ID ,
                                        MenuID ,
                                        Url ,
                                        WFName ,
                                        DeptID ,
                                        WFPID ,
                                        ParentID ,
                                        AllowCustomFlow ,
                                        IsOn ,
                                        IsDelete
                                FROM    SCFlowMaping ";
            using (session)
            {
                return session.CreateSQLQuery(sqlString)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SCFlowMaping))).List<SCFlowMaping>();
            }
        }


        public List<int> GetChildDeptmentsByDept(int deptID)
        {
            Dictionary<int, SCDepartment> dic = GetSCDepartmentList().ToDictionary(item => item.ID);
            return dic.Values.ToList().Where(p => p.RootPath.StartsWith(dic[deptID].RootPath + ",")).Select(dept => dept.ID).ToList();
        }

        public IList<SCDepartment> GetSCDepartmentList()
        {

            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  ID ,
                                        Code ,
                                        DeptName ,
                                        ParentID ,
                                        DeptTypeID ,
                                        ManagerID ,
                                        LeaderID ,
                                        PersonNumber ,
                                        Tel ,
                                        Address ,
                                        IsCostCenter ,
                                        Memo ,
                                        IsOn ,
                                        RootPath ,
                                        CompanyID ,
                                        OrderCode ,
                                        DeptShortName ,
                                        Area ,
                                        SortNum ,
                                        DeptLevels ,
                                        HRID
                                FROM    SCDepartment
                                ORDER BY CODE";
            using (session)
            {
                return session.CreateSQLQuery(sqlString)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SCDepartment))).List<SCDepartment>();;
            }
        }

        public Dictionary<int, SCDepartment> GetSCDepartmentDictionary()
        {

            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  ID ,
                                        Code ,
                                        DeptName ,
                                        ParentID ,
                                        DeptTypeID ,
                                        ManagerID ,
                                        LeaderID ,
                                        PersonNumber ,
                                        Tel ,
                                        Address ,
                                        IsCostCenter ,
                                        Memo ,
                                        IsOn ,
                                        RootPath ,
                                        CompanyID ,
                                        OrderCode ,
                                        DeptShortName ,
                                        Area ,
                                        SortNum ,
                                        DeptLevels ,
                                        HRID
                                FROM    SCDepartment
                                ORDER BY CODE";
            using (session)
            {
                return session.CreateSQLQuery(sqlString)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SCDepartment))).List<SCDepartment>().ToDictionary(item => item.ID); ;
            }
        }

        public SCDepartment GetSCDepartmentByID(int id)
        {
            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  ID ,
                                        Code ,
                                        DeptName ,
                                        ParentID ,
                                        DeptTypeID ,
                                        ManagerID ,
                                        LeaderID ,
                                        PersonNumber ,
                                        Tel ,
                                        Address ,
                                        IsCostCenter ,
                                        Memo ,
                                        IsOn ,
                                        RootPath ,
                                        CompanyID ,
                                        OrderCode ,
                                        DeptShortName ,
                                        Area ,
                                        SortNum ,
                                        DeptLevels ,
                                        HRID
                                FROM    SCDepartment
                                WHERE ID=?
                                ORDER BY CODE";
            using (session)
            {
                return session.CreateSQLQuery(sqlString).SetInt32(0, id)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SCDepartment))).List<SCDepartment>().FirstOrDefault();
            }
        }


    }
}
