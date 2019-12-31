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
    /// Filter toolbox item based on if it has been designated for
    /// use on frontend and/or backend pages.
    /// </summary>
    public class BackendPageToolboxItemFilter : IToolboxItemFilter
    {
        private readonly ConfigElementList<WidgetSecurityElement> _settings;

        public BackendPageToolboxItemFilter(ConfigElementList<WidgetSecurityElement> settings)
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

            var isForEditingAnyPage = !widgetConfig.AdminPagesOnly;
            var isEditingBackendPage = !page.Form.AppRelativeTemplateSourceDirectory.ToLower().StartsWith("~/sflayouts/");

            return isForEditingAnyPage || isEditingBackendPage;
        }
    }
}