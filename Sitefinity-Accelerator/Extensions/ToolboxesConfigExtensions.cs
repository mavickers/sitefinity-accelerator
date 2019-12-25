using System;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace SitefinityAccelerator.Extensions
{
    public static class ToolboxesConfigExtensions
    {
        public static ToolboxItem GetToolboxItem(this ToolboxesConfig config, Type type)
        {
            if (config == null || type == null)
            {
                return null;
            }

            if (!type.Name.Contains("Controller"))
            {
                return null;
            }

            foreach (var configElement in config.Toolboxes)
            {
                var toolbox = (Toolbox) configElement;

                foreach (var section in toolbox.Sections)
                {
                    foreach (var toolboxItem in section.Tools)
                    {
                        if (toolboxItem.ControllerType == type.FullName) return toolboxItem;
                    }
                }
            }

            return null;
        }
    }
}