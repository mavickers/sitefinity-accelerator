using Ninject;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace SitefinityAccelerator.Mvc
{
    public class BaseControllerParameters
    {
        public bool IsDesignMode { get; }
        public string DefaultGenericDesignView { get; }
        public string DefaultUnconfiguredWidgetView { get; }
        public ToolboxesConfig ToolboxesConfig { get; }

        public BaseControllerParameters
        (
            [Named("IsDesignMode")] bool isDesignMode,
            [Named("DefaultGenericDesignView")] string defaultGenericDesignView,
            [Named("DefaultGenericUnconfiguredWidgetView")] string defaultUnconfiguredWidgetView,
            ToolboxesConfig toolboxesConfig
        )
        {
            IsDesignMode = isDesignMode;
            DefaultGenericDesignView = defaultGenericDesignView;
            DefaultUnconfiguredWidgetView = defaultUnconfiguredWidgetView;
            ToolboxesConfig = toolboxesConfig;
        }
    }
}