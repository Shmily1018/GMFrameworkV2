using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using GoldMantis.Common;
using NHibernate;
using NHibernate.Context;

namespace GoldMantis.ServiceNew
{

	public class NhibernateInspector : IDispatchMessageInspector
	{
		private static readonly ISessionFactory _sessionFactory = NHibernateHelper.SessionFactory;
		public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
		{
			Console.WriteLine("{0} -- {1}", DateTime.Now.ToString(), request.Headers.Action);
			CurrentSessionContext.Bind(_sessionFactory.OpenSession());
			return null;
		}

		public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
		{
			var session = CurrentSessionContext.Unbind(_sessionFactory);
			session.Dispose();
		}
	}

	public class NHibernateBehavior : IServiceBehavior
	{
		
		public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
		{
			//have no need to implement this method.
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
		{
			//This is where the Inspector gets hooked to all the endpoints.
			if (serviceHostBase != null)
			{
				for (Int32 i = 0; i < serviceHostBase.ChannelDispatchers.Count; i++)
				{
					ChannelDispatcher channelDispatcher = serviceHostBase.ChannelDispatchers[i] as ChannelDispatcher;
					if (channelDispatcher != null)
					{
						foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
						{
							endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new NhibernateInspector());
						}
					}
				}
			}
			if (serviceDescription != null)
			{
				for (int i = 0; i < serviceDescription.Endpoints.Count; i++)
				{
					var contract = serviceDescription.Endpoints[i].Contract;
					var operations = contract.Operations;
					for (int j = 0; j < operations.Count; j++)
					{
						DataContractSerializerOperationBehavior serializerBehavior =operations[j].Behaviors.Find<DataContractSerializerOperationBehavior>();
						if (serializerBehavior == null)
						{
							serializerBehavior = new DataContractSerializerOperationBehavior(operations[j]);
							operations[j].Behaviors.Add(serializerBehavior);
						}
						serializerBehavior.MaxItemsInObjectGraph = 2147483647;
						serializerBehavior.IgnoreExtensionDataObject = true;
					}
				}
			}
		}

		public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
		{

		}

		
	}

}
