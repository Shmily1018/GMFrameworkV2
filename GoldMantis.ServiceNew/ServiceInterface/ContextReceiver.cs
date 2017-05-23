using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace GoldMantis.ServiceNew.ServiceInterface
{
	public class ContextReceiver : ICallContextInitializer
	{
		public void AfterInvoke(object correlationState) { }
		public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
		{
			HttpRequestMessageProperty requestProperty = (HttpRequestMessageProperty)message.Properties[HttpRequestMessageProperty.Name];
			foreach (string key in requestProperty.Headers.Keys)
			{
				if (key.StartsWith("__"))
				{
					ApplicationContext.Current[key] = requestProperty.Headers[key];
				}
			}
			return null;
		}
	}
}
