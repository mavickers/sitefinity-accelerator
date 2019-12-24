using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace SitefinityAccelerator.Interfaces
{
    public interface IToolboxItemFilter
    {
        bool IsEnabled(KeyValuePair<string, ToolboxItem> toolboxItem, Page page);
    }
}