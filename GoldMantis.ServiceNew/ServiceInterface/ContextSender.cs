using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace GoldMantis.ServiceNew.ServiceInterface
{
	public class ContextSender : IClientMessageInspector
	{
		public void AfterReceiveReply(ref Message reply, object correlationState) { }
		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			
			HttpRequestMessageProperty requestProperty;
			if (!request.Properties.Keys.Contains(HttpRequestMessageProperty.Name))
			{
				requestProperty = new HttpRequestMessageProperty();
				request.Properties.Add(HttpRequestMessageProperty.Name, requestProperty);
			}
			else
			{
				requestProperty = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
			}
			foreach (var context in ApplicationContext.Current)
			{
				requestProperty.Headers.Add(context.Key, context.Value.ToString());
			}
			
			return null;
		}
	}
}
