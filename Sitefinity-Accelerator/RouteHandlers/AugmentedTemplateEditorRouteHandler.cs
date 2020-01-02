using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web.UI;
using SitefinityAccelerator.Extensions;
using SitefinityAccelerator.Interfaces;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Routing;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityAccelerator.RouteHandlers
{
    /// <inheritdoc />
    /// <summary>
    /// Template editor router incorporating additional security trimming.
    /// </summary>
    /// <remarks>
    /// https://www.progress.com/documentation/sitefinity-cms/tutorial-use-an-item-parameter-to-implement-role-based-toolbox-filtering-webforms#add-the-disallowedroles-item-in-your-project
    ///
    /// This class follows the guidelines from that article. Something to watch out for - when setting the toolboxItem.Value.Enabled
    /// value you are making a change to a setting that technically affects all users. While this will run for each user and therefore
    /// set the toolbox items accordingly prior to presentation it is something to keep an eye on - the settings are getting thrashed
    /// each time page edit or template edit is loaded.
    /// </remarks>
    public class AugmentedTemplateEditorRouteHandler : MvcTemplateEditorRouteHandler
    {
        private readonly List<IToolboxItemFilter> _filters;

        public AugmentedTemplateEditorRouteHandler(List<IToolboxItemFilter> filters)
        {
            _filters = filters;
        }

        protected override void ApplyLayoutsAndControls(Page page, RequestContext requestContext)
        {
            base.ApplyLayoutsAndControls(page, requestContext);

            var zoneEditor = page?.Form?.FindControl("ZoneEditor") as ZoneEditor;

            // ZoneEditor is not available in some cases (for example, when the
            // page is locked, or page.Form is null).
            if (zoneEditor == null)
            {
                return;
            }

            var tools = zoneEditor.GetToolboxItems();

            if (!tools?.Any() ?? true)
            {
                return;
            }

            foreach (var toolboxItem in tools)
            {
                toolboxItem.Value.Enabled = true;

                foreach (var filter in _filters)
                {
                    toolboxItem.Value.Enabled = toolboxItem.Value.Enabled && filter.IsEnabled(toolboxItem, page);
                }
            }
        }
    }
}
