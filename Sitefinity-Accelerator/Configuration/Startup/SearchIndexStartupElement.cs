using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace SitefinityAccelerator.Configuration.Startup
{
    public class SearchIndexStartupElement : ConfigElement
    {
        public SearchIndexStartupElement(ConfigElement parent) : base(parent) { }

        [ObjectInfo(Title = "Index Name", Description = "The case-sensitive name of the index to include in startup routines.")]
        [ConfigurationProperty("IndexName", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string IndexName
        {
            get => (string) this["IndexName"];
            set => this["IndexName"] = value;
        }

        [ObjectInfo(Title = "Publishing Point Name", Description = "The case-sensitive name of the publishing point of the search index.")]
        [ConfigurationProperty("PublishingPointName", DefaultValue = "", IsRequired = true, IsKey = false)]
        public string PublishingPointName
        {
            get => (string) this["PublishingPointName"];
            set => this["PublishingPointName"] = value;
        }

        [ObjectInfo(Title = "Run In Background", Description = "On startup run this index in the background instead of blocking the startup process.")]
        [ConfigurationProperty("WithBackgroundIndexing", DefaultValue = false)]
        public bool WithBackgroundIndexing
        {
            get => (bool) this["WithBackgroundIndexing"];
            set => this["WithBackgroundIndexing"] = value;
        }

        [ObjectInfo(Title = "Force Rebuild", Description = "Force rebuilding of the index, even if it is already present in the file system.")]
        [ConfigurationProperty("WithForceRebuild", DefaultValue = false)]
        public bool WithForceRebuild
        {
            get => (bool) this["WithForceRebuild"];
            set => this["WithForceRebuild"] = value;
        }
    }
}
