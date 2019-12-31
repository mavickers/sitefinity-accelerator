using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using SitefinityAccelerator.Configuration.Security.Widgets;
using SitefinityAccelerator.Interfaces;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace SitefinityAccelerator.Filters
{
    /// <summary>
    /// Filter toolbox item based on if the widget is designated for use
    /// on pages and/or templates.
    /// </summary>
    public class PageTemplateToolboxItemFilter : IToolboxItemFilter
    {
        private readonly ConfigElementList<WidgetSecurityElement> _settings;

        public PageTemplateToolboxItemFilter(ConfigElementList<WidgetSecurityElement> settings)
        {
            _settings = settings;
        }

        public bool IsEnabled(KeyValuePair<string, ToolboxItem> toolboxItem, Page page)
        {
            // first check to see if there is a toolbox entry in the settings; if not then
            // consider it enabled.

            var widgetConfig = _settings.Elements.FirstOrDefault(el => el.Widget == toolboxItem.Key);

            if (widgetConfig == null)
            {
                return true;
            }

            var isTemplate = page.AppRelativeVirtualPath.ToLower().StartsWith("~/sitefinity/template");
            var isPage = !isTemplate;

            return (isTemplate && widgetConfig.ForTemplates) || (isPage && widgetConfig.ForPages);
        }
    }
}