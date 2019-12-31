using SitefinityAccelerator.Configuration.Security.Widgets;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;

namespace SitefinityAccelerator.Startup
{
    public static class ToolboxFilters
    {
        public static void Warmup()
        {
            if (ObjectFactory.Container != null)
            {
                // filters for toolbox widgets sometimes do not execute until they are read; apparently
                // the process of loading a page or template in the editor does not read the settings
                // by itself every time; the circumstances where the filters fail to execute are not
                // completely know at this time, so leaving this here in case it's needed.

                ObjectFactory.Container.Resolve(typeof(ConfigElementList<WidgetSecurityElement>));
            }
        }
    }
}
