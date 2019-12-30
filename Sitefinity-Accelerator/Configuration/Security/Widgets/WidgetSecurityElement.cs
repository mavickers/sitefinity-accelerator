using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace SitefinityAccelerator.Configuration.Security.Widgets
{

    public class WidgetSecurityElement : ConfigElement
    {
        public WidgetSecurityElement(ConfigElement parent) : base(parent) { }

        [ObjectInfo(Title = "Widget", Description = "The case-sensitive name of the widget item in the toolbox.")]
        [ConfigurationProperty("Widget", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Widget
        {
            get => (string)this["Widget"];
            set => this["Widget"] = value;
        }

        [ObjectInfo(Title = "Security Roles", Description = "A comma-separated list of security roles which will be permitted access to this widget. An asterisk value indicates all roles will be allowed (and any other values will be ignored). If blank the widget will not be accessible.")]
        [ConfigurationProperty("SecurityRoles", DefaultValue = "*", IsRequired = false, IsKey = false)]
        public string SecurityRoles
        {
            get => (string)this["SecurityRoles"];
            set => this["SecurityRoles"] = value;
        }

        [ObjectInfo(Title = "Admin Pages Only", Description = "If checked this widget will only be available when editing admin backend pages.")]
        [ConfigurationProperty("AdminPagesOnly", DefaultValue = false)]
        public bool AdminPagesOnly
        {
            get => (bool)this["AdminPagesOnly"];
            set => this["AdminPagesOnly"] = value;
        }

        [ObjectInfo(Title = "For Pages", Description = "This widget may be placed on an individual page.")]
        [ConfigurationProperty("ForPages", DefaultValue = true)]
        public bool ForPages
        {
            get => (bool)this["ForPages"];
            set => this["ForPages"] = value;
        }

        [ObjectInfo(Title = "For Templates", Description = "This widget may be placed on a page template.")]
        [ConfigurationProperty("ForTemplates", DefaultValue = false)]
        public bool ForTemplates
        {
            get => (bool)this["ForTemplates"];
            set => this["ForTemplates"] = value;
        }
    }
}