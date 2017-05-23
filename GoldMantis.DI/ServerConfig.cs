using System.Configuration;
using GoldMantis.Common;

namespace GoldMantis.DI
{
    public class ServicesConfig : ConfigurationSection
    {
        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("servers")]
        [ConfigurationCollection(typeof (ServersConfigCollection), AddItemName = "server")]
        public ServersConfigCollection ServiceConfigItems
        {
            get { return ((ServersConfigCollection) (base["servers"])); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ServicesConfig GetServicesConfig()
        {
            return ConfigurationManager.GetSection("services").As<ServicesConfig>();
        }
    }


    [ConfigurationCollection(typeof (ServerConfig))]
    public class ServersConfigCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServerConfig();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return element.As<ServerConfig>().assemblyName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementKey"></param>
        /// <returns></returns>
        public ServerConfig this[string elementKey]
        {
            get { return BaseGet(elementKey).As<ServerConfig>(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public ServerConfig this[int idx]
        {
            get { return BaseGet(idx).As<ServerConfig>(); }
        }
    }


    public class ServerConfig : ConfigurationElement
    {
        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("assemblyName", DefaultValue = "", IsRequired = true)]
        public string assemblyName
        {
            get { return (string) this["assemblyName"]; }
            set { this["assemblyName"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("serverType", DefaultValue = "Local", IsRequired = true)]
        public string serverType
        {
            get { return (string) this["serverType"]; }
            set { this["serverType"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("binding", DefaultValue = "basicHttpBinding", IsRequired = false)]
        public string binding
        {
            get { return this["binding"] == null ? "basicHttpBinding" : (string) this["binding"]; }
            set { this["binding"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("serverURL", DefaultValue = "", IsRequired = true)]
        public string serverURL
        {
            get { return (string) this["serverURL"]; }
            set { this["serverURL"] = value; }
        }
    }
}