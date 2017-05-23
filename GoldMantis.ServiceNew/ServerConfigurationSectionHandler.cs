using System.Configuration;

namespace GoldMantis.ServiceNew
{
	public class ServerConfigurationSection : System.Configuration.ConfigurationSection
	{
		[ConfigurationProperty("assembly-collection")]
		[ConfigurationCollection(typeof(AssemblyElementCollection), AddItemName = "assembly")]
		public AssemblyElementCollection AssemblyCollection
		{
			get
			{
				return (AssemblyElementCollection)base["assembly-collection"];
			}
		}

		[ConfigurationProperty("dll-output")]
		public ValueElement DllOutput
		{
			get
			{
				return (ValueElement)base["dll-output"];
			}
		}


		private static ServerConfigurationSection config;
		public static ServerConfigurationSection GetServerConfiguration()
		{
			if (config == null)
			{
				config = ConfigurationManager.GetSection("server-configuration") as ServerConfigurationSection;
			}
			return config;
		}

	}


	[ConfigurationCollection(typeof(AssemblyElement))]
	public class AssemblyElementCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new AssemblyElement();
		}
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AssemblyElement)(element)).Name;
		}
		public AssemblyElement this[string elementKey]
		{
			get { return (AssemblyElement)BaseGet(elementKey); }
		}
		public AssemblyElement this[int idx]
		{
			get { return (AssemblyElement)BaseGet(idx); }
		}
	}
	public class AssemblyElement : ConfigurationElement
	{
		[ConfigurationProperty("name", DefaultValue = "", IsRequired = true)]
		public string Name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("type", DefaultValue = "normal", IsRequired = true)]
		public string Type
		{
			get { return (string)this["type"]; }
			set { this["type"] = value; }
		}

	}

	public class ValueElement : ConfigurationElement
	{
		[ConfigurationProperty("path")]
		public string Path
		{
			get { return (string)base["path"]; }
			set { base["path"] = value; }
		}

		[ConfigurationProperty("assemblyNamePrefix")]
		public string AssemblyNamePrefix
		{
			get
			{
				var temp = (string)base["assemblyNamePrefix"];
				if (temp == null)
					return string.Empty;
				return temp;
			}
			set { base["assemblyNamePrefix"] = value; }
		}
	}

}
