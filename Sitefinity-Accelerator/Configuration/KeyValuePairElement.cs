using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace SitefinityAccelerator.Configuration
{
    public class KeyValuePairElement : ConfigElement
    {
        public KeyValuePairElement(ConfigElement parent) : base(parent) { }

        [ObjectInfo(Title = "Key", Description = "The key value of the configuration pair.")]
        [ConfigurationProperty("Key", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Key
        {
            get => (string) this["Key"];
            set => this["Key"] = value;
        }

        [ObjectInfo(Title = "Value", Description = "The value of the configuration pair.")]
        [ConfigurationProperty("Value", DefaultValue = "", IsRequired = true, IsKey = false)]
        public string Value
        {
            get => (string) this["Value"];
            set => this["Value"] = value;
        }
    }
}
