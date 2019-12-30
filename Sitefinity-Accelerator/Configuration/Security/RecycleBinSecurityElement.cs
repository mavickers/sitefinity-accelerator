using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace SitefinityAccelerator.Configuration.Security
{
    public class RecycleBinSecurityElement : ConfigElement
    {
        public RecycleBinSecurityElement(ConfigElement parent) : base(parent) { }

        [ObjectInfo(Title = "Security Roles", Description = "A comma-separated list of security roles which will be permitted access to empty the recycle bin. An asterisk value indicates all roles will be allowed (and any other values will be ignored). If blank emptying the recycle bin will not be accessible.")]
        [ConfigurationProperty("SecurityRoles", DefaultValue = "*", IsRequired = false, IsKey = false)]
        public string SecurityRoles
        {
            get => (string)this["SecurityRoles"];
            set => this["SecurityRoles"] = value;
        }
    }
}