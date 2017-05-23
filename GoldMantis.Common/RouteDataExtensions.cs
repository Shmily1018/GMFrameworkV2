/*********************************************************
** Copyright (c)     2015 Gold Mantis Co., Ltd. 
** FileName:         RouteDataExtenssions.cs
** Creator:          Joe
** CreateDate:       2015-04-24
** Changer:
** LastChangeDate:
** Description:      RouteData扩展方法集合
** VersionInfo:
*********************************************************/

using System.Web.Routing;

namespace GoldMantis.Common
{
    public static class RouteDataExtensions
    {
        /// <summary>
        /// 从RouteData中获取ActionName
        /// </summary>
        /// <param name="routeData">RouteData</param>
        /// <returns>ActionName</returns>
        public static string GetActionName(this RouteData routeData)
        {
            return routeData.GetRequiredString("action").ToUpperHead();
        }

        /// <summary>
        /// 从RouteData中获取ControllerName
        /// </summary>
        /// <param name="routeData">RouteData</param>
        /// <returns>ControllerName</returns>
        public static string GetControllerName(this RouteData routeData)
        {
            return routeData.GetRequiredString("controller").ToUpperHead();
        }

        /// <summary>
        /// 从RouteData中获取AreaName
        /// </summary>
        /// <param name="routeData">RouteData</param>
        /// <returns>AreaName</returns>
        public static string GetAreaName(this RouteData routeData)
        {
            object areaObj;

            if (routeData.DataTokens.TryGetValue("area", out areaObj))
            {
                return areaObj.ToString().ToUpperHead();
            }


            return null;
        }
    }
}
