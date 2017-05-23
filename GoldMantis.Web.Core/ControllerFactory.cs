/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         ControllerFactory.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      自定义Controller创建工厂
** VersionInfo:
*********************************************************/
using System;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.DI;
using GoldMantis.Log;

namespace GoldMantis.Web.Core
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {

            IController controller=null;
            try
            {
                 controller = base.GetControllerInstance(requestContext, controllerType);
            }
            catch (Exception e)
            {
                //LogHelper.WriteLog(e.Message);
            }

            if (controller.IsNull())
            {
                return null;
            }

            // 注入服务
            ServiceInjection.Inject(controller);

            return controller;


        }
    }
}