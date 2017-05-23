/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         ServiceInjection.cs
** Creator:          Joe
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      Service注入工具
** VersionInfo:
*********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using GoldMantis.Common;

namespace GoldMantis.DI
{
    public static class ServiceInjection
    {
        private static MethodInfo _method = typeof (ServiceFactory).GetMethod("GetService");
        private static string _filter = typeof (IService).FullName;
        private static object asyn = new object();

        public static void Inject(IController controller)
        {
            FieldMapping mapping = ControllerServiceMapping.Instance;

            Type contType = controller.GetType();
            string contFullName = contType.FullName;

            if (mapping[contFullName].IsNull())
            {
                List<FieldInfo> fs = contType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(f => f.FieldType.GetInterface(_filter).IsNotNull()).ToList();

                mapping.Add(contFullName, fs ?? FieldMapping.EmptyList);
            }

            List<FieldInfo> fields = mapping[contFullName];


            foreach (FieldInfo field in fields)
            {
                field.SetValue(controller, _method.MakeGenericMethod(field.FieldType).Invoke(null, null));
            }


        }
    }
}