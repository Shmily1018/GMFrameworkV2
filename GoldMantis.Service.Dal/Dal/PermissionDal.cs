using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Common;
using GoldMantis.Common.Const;
using GoldMantis.Common.Data;
using NHibernate.Transform;

namespace GoldMantis.Service.Dal.Dal
{
    public class PermissionDal:IDal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginID"></param>
        /// <param name="password"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public UserInfo GetLoginInfo(string loginID)
        {
            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  A.ID AS UserID ,
                                        UserName ,
                                        LoginID AS Account ,
                                        Email ,
                                        [Password] ,
                                        JobCode ,
                                        IsSync ,
                                        CONCAT(UserName, '(', LoginID, ')') AS DisplayName ,
                                        A.CanLogin,
                                        A.IsOnJob,
		                                A.IsOn,
                                        ConfirmAccountCorrect ,
                                        C.ID AS HrDeptID ,
                                        C.DeptName AS DisplayDepartMent ,
                                        ( SELECT    p.Name
                                          FROM      HREmployees e
                                                    LEFT JOIN HRPosition p ON e.MasterPositionId = p.ID
                                          WHERE     e.EmployeeNo = A.JobCode
                                                    AND p.ID IS NOT NULL
                                        ) AS DisplayPosition
                                FROM    dbo.SAUser A
                                        LEFT JOIN dbo.SAUserDept B ON B.UserID = A.ID
                                        LEFT JOIN dbo.SCDepartment C ON C.ID = B.DeptID
		                                WHERE LoginID=?";
            using (session)
            {
                return session.CreateSQLQuery(sqlString).SetString(0, loginID).SetResultTransformer(new AliasToBeanResultTransformer(typeof(UserInfo))).List<UserInfo>().FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取模拟用户权限
        /// </summary>
        /// <param name="agentUserId">需要模拟的用户</param>
        /// <param name="currentUserId">当前用户</param>
        /// <returns></returns>
        public bool GetAgentPermission(int agentUserId, int currentUserId)
        {
            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"IF EXISTS(SELECT ID FROM SCUserAgentConfig WHERE AgentUserID=:AgentUserID)
	                                           BEGIN
		                                           IF EXISTS(SELECT ID FROM SCUserAgentConfig WHERE AgentUserID=:AgentUserID 
                                                                                 AND CurrentUserID=:CurrentUserID AND IsOn=1)
			                                           SELECT 1 SelectResult
		                                           ELSE
			                                           SELECT 0 SelectResult
	                                           END
                                           ELSE
	                                           SELECT 1 SelectResult";
            using (session)
            {
                int uniqueResult = session.CreateSQLQuery(sqlString)
                    .SetParameter("CurrentUserID", currentUserId)
                    .SetParameter("AgentUserID", agentUserId)
                    .UniqueResult<int>();

                return uniqueResult == 1;
            }
        }

        public IList<SAMenu> GetUserMenuInfo(int userID, int sourceAppId)
        {
            sourceAppId = Convert.ToInt32(ConfigurationManager.AppSettings["SourceAppId"]);
            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT DISTINCT
                                    sm.ID ,
                                    sm.SourceAppId ,
                                    sm.MenuName ,
                                    sm.ParentID ,
                                    sm.MenuURL ,
                                    sm.MenuImage ,
                                    sm.BillNO ,
                                    sm.Sort ,
                                    sm.MenuPath ,
                                    sm.Memo ,
                                    sm.IsOn ,
                                    sm.OpenType
                            FROM    SARoleMenu rm
                                    INNER JOIN SAUserRole ur ON ur.RoleID = rm.RoleID
                                    INNER JOIN dbo.SARole sr ON ur.RoleID = sr.ID
                                                                AND sr.IsOn = 1
                                    INNER JOIN dbo.SAMenu sm ON rm.MenuID = sm.ID
                                                                AND sm.IsOn = 1
                            WHERE   ur.UserID = ?
                                    AND sm.SourceAppId = ?";
            using (session)
            {
                return session.CreateSQLQuery(sqlString).SetInt32(0, userID).SetInt32(1, sourceAppId)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SAMenu))).List<SAMenu>();
            }
        }

        public IList<SAUser> GetSAUserList()
        {
            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  ID ,
                                    LoginID ,
                                    Password ,
                                    UserName ,
                                    Email ,
                                    Mobile ,
                                    SMobile ,
                                    Telephone ,
                                    JobCode ,
                                    IdentityCard ,
                                    UserType ,
                                    CreateTime ,
                                    IsOn ,
                                    IsOnTime ,
                                    CanLogin ,
                                    CanLoginTime ,
                                    IsOnJob ,
                                    ExpireTime ,
                                    IsSync ,
                                    ExtensionNumber ,
                                    DirectNummer ,
                                    Fax ,
                                    IsSimple ,
                                    SaleInfoNumEnabled ,
                                    SortNo ,
                                    AccDisabledTime ,
                                    CreateUser ,
                                    LastUpdateDate ,
                                    ConfirmAccountCorrect FROM dbo.SAUser ";
            using (session)
            {
                return session.CreateSQLQuery(sqlString)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SAUser))).List<SAUser>();
            }

        }

        public SAUser GetSAUserByID(int id)
        {
            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  ID ,
                                    LoginID ,
                                    Password ,
                                    UserName ,
                                    Email ,
                                    Mobile ,
                                    SMobile ,
                                    Telephone ,
                                    JobCode ,
                                    IdentityCard ,
                                    UserType ,
                                    CreateTime ,
                                    IsOn ,
                                    IsOnTime ,
                                    CanLogin ,
                                    CanLoginTime ,
                                    IsOnJob ,
                                    ExpireTime ,
                                    IsSync ,
                                    ExtensionNumber ,
                                    DirectNummer ,
                                    Fax ,
                                    IsSimple ,
                                    SaleInfoNumEnabled ,
                                    SortNo ,
                                    AccDisabledTime ,
                                    CreateUser ,
                                    LastUpdateDate ,
                                    ConfirmAccountCorrect FROM dbo.SAUser WHERE ID= ? ";
            using (session)
            {
                return session.CreateSQLQuery(sqlString).SetInt32(0, id)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SAUser))).List<SAUser>().FirstOrDefault();
            }
        }

        public IList<SAUser> GetSAUserByIDs(int[] ids)
        {
            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  ID ,
                                    LoginID ,
                                    Password ,
                                    UserName ,
                                    Email ,
                                    Mobile ,
                                    SMobile ,
                                    Telephone ,
                                    JobCode ,
                                    IdentityCard ,
                                    UserType ,
                                    CreateTime ,
                                    IsOn ,
                                    IsOnTime ,
                                    CanLogin ,
                                    CanLoginTime ,
                                    IsOnJob ,
                                    ExpireTime ,
                                    IsSync ,
                                    ExtensionNumber ,
                                    DirectNummer ,
                                    Fax ,
                                    IsSimple ,
                                    SaleInfoNumEnabled ,
                                    SortNo ,
                                    AccDisabledTime ,
                                    CreateUser ,
                                    LastUpdateDate ,
                                    ConfirmAccountCorrect FROM dbo.SAUser WHERE ID IN  (:ids) ";
            using (session)
            {
                return session.CreateSQLQuery(sqlString).SetParameterList("ids",ids)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SAUser))).List<SAUser>();
            }

            if (ids != null)
            {
                return GetSAUserList().Where(x => Enumerable.Contains(ids, x.ID)).ToList();
            }
            return GetSAUserList();
        }

        public IList<SAMenu> GetSAMenuList()
        {

            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  ID ,
                                        SourceAppId ,
                                        MenuName ,
                                        ParentID ,
                                        MenuURL ,
                                        MenuImage ,
                                        BillNO ,
                                        Sort ,
                                        MenuPath ,
                                        Memo ,
                                        IsOn ,
                                        OpenType
                                FROM    SAMenu";
            using (session)
            {
                return session.CreateSQLQuery(sqlString)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SAMenu))).List<SAMenu>();
            }
        }

        public SAMenu GetSAMenuByID(int id)
        {
            var session = NHibernateHelperWithConnection.GetSessionWithConnection(Consts.PERMISSION_DB_LINK, EnumDBType.SQL_SERVER);
            string sqlString = @"SELECT  ID ,
                                        SourceAppId ,
                                        MenuName ,
                                        ParentID ,
                                        MenuURL ,
                                        MenuImage ,
                                        BillNO ,
                                        Sort ,
                                        MenuPath ,
                                        Memo ,
                                        IsOn ,
                                        OpenType
                                FROM    SAMenu WHERE  ID=? ";
            using (session)
            {
                return session.CreateSQLQuery(sqlString).SetInt32(0, id)
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(SAMenu))).List<SAMenu>().FirstOrDefault();
            }
        }
    }
}
