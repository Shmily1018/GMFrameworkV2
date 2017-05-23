using System.ServiceModel.Description;

namespace GoldMantis.ServiceNew.ServiceInterface
{
	public class ContextPropagationBehavior : IEndpointBehavior
	{
		public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters) { }
		public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
		{
			clientRuntime.MessageInspectors.Add(new ContextSender());
		}
		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
		{
			foreach (var operation in endpointDispatcher.DispatchRuntime.Operations)
			{
				operation.CallContextInitializers.Add(new ContextReceiver());
			}
		}
		public void Validate(ServiceEndpoint endpoint) { }
	}
}