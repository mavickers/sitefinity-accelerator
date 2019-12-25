using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityAccelerator.Extensions
{
    public static class ZoneEditorExtensions
    {
        public static Dictionary<string, ToolboxItem> GetToolboxItems(this ZoneEditor source)
        {
            if (!source?.ControlToolbox?.Sections?.Any() ?? true)
            {
                return null;
            }

            var tools = new Dictionary<string, ToolboxItem>();

            foreach (var section in source.ControlToolbox.Sections)
            {
                foreach (var toolboxItem in section.Tools)
                {
                    var tool = (ToolboxItem)toolboxItem;

                    if (!tools.ContainsKey(tool.Name))
                    {
                        tools.Add(tool.Name, tool);
                    }
                }
            }

            return tools;
        }
    }
}