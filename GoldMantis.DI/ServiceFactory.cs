/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         ServiceFactory.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      服务创建工厂
** VersionInfo:
*********************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace GoldMantis.DI
{
    public static class ServiceFactory
    {
        private static ServicesConfig _servicesConfig = ServicesConfig.GetServicesConfig();
        //private static string _contractPrefix = "GoldMantis.Service.Contract.Contract";

        public static T GetService<T>()
        {
            Type type = typeof (T);
            string name = type.Assembly.GetName().Name;
            return GetWcfService<T>(name, _servicesConfig.ServiceConfigItems[name].serverURL);
        }

        private static T GetWcfService<T>(string wcfServer, string serviceUrl)
        {
            if (!serviceUrl.EndsWith("/"))
            {
                serviceUrl = string.Format("{0}/", serviceUrl);
            }

            string fullName = typeof (T).FullName;

            Object o = null;

            serviceUrl = string.Format("{0}wcf/{1}.svc/basicHttp", serviceUrl,
               fullName.Substring(fullName.LastIndexOf('.') + 2));

            //string serviceUrl = string.Format("http://localhost:8205/wcf/VW_OAKPILeavesService.svc/basicHttp");


            EndpointAddress address = new EndpointAddress(serviceUrl);
            Binding bindinginstance = null;
            BasicHttpBinding ws = new BasicHttpBinding();
            ws.Security.Mode = BasicHttpSecurityMode.None;
            ws.MaxBufferPoolSize = 2147483647;
            ws.MaxReceivedMessageSize = 2147483647;
            ws.OpenTimeout = new TimeSpan(0, 1, 20);
            ws.SendTimeout = new TimeSpan(0, 30, 0);
            ws.ReceiveTimeout = new TimeSpan(0, 1, 20);
            ws.CloseTimeout = new TimeSpan(0, 1, 20);
            ws.ReaderQuotas = new XmlDictionaryReaderQuotas() { MaxStringContentLength = 2147483647, MaxArrayLength = 2147483647 };
            bindinginstance = ws;

            Type channelFactoryType = typeof(ChannelFactory<>);
            channelFactoryType = channelFactoryType.MakeGenericType(typeof(T));
            ChannelFactory channelFactory = (ChannelFactory)Activator.CreateInstance(channelFactoryType, bindinginstance, address);


            foreach (OperationDescription op in channelFactory.Endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>();
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = 2147483647;
                }
            }
            MethodInfo createchannel = null;
            createchannel = channelFactory.GetType().GetMethod("CreateChannel", new Type[0]);
            return (T)createchannel.Invoke(channelFactory, null);

        }
    }
}