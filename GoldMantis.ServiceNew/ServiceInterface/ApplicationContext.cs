using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace GoldMantis.ServiceNew.ServiceInterface
{
	public class ApplicationContext : Dictionary<string, string>
	{
		public const string KeyOfApplicationContext = "__ApplicationContext";
		private ApplicationContext()
		{ }
		public static ApplicationContext Current
		{
			get
			{
				if (HttpContext.Current != null)
				{
					if (HttpContext.Current.Session[KeyOfApplicationContext] == null)
					{
						HttpContext.Current.Session[KeyOfApplicationContext] = new ApplicationContext();
					}
					return (ApplicationContext)HttpContext.Current.Session[KeyOfApplicationContext];
				}

				if (CallContext.GetData(KeyOfApplicationContext) == null)
				{
					CallContext.SetData(KeyOfApplicationContext, new ApplicationContext());
				}
				return (ApplicationContext)CallContext.GetData(KeyOfApplicationContext);
			}
			set
			{
				CallContext.SetData("__ApplicationContext", value);
			}
		}

		public string UserName
		{
			get { return this.GetContextValue("__UserName"); }
			set { this["__UserName"] = value; }
		}
		public string Password
		{
			get { return this.GetContextValue("__Password"); }
			set { this["__Password"] = value; }
		}

		public string IP
		{
			get { return this.GetContextValue("__IP"); }
			set { this["__IP"] = value; }
		}

		private string GetContextValue(string key)
		{
			if (this.ContainsKey(key))
			{
				return (string)this[key];
			}
			return string.Empty;
		}
	}
}
