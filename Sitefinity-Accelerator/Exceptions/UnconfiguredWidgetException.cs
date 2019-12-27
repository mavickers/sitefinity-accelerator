using System;
using System.Linq;
using System.Web.Mvc;
using SitefinityAccelerator.Extensions;
using SitefinityAccelerator.Mvc;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace SitefinityAccelerator.Exceptions
{
    public class UnconfiguredWidgetException : SystemException
    {
        public string Classes { get; }
        public bool WithPresentationMode { get; }
        public string View { get; }
        private static ToolboxItem ToolboxItem(Type type) => Config.Get<ToolboxesConfig>().GetToolboxItem(type);
        private static string ToolboxItemMessage(Type type) => $"Configure {ToolboxItem(type)?.Title ?? "Widget"}";
        public string AdditionalText;

        public UnconfiguredWidgetException(string message = null, string view = null) : base(message)
        {
            View = view;
        }

        public UnconfiguredWidgetException(BaseController controller, string additionalText = null, bool withPresentationMode = false) : base(ToolboxItemMessage(controller.GetType()))
        {
            Classes = ToolboxItem(controller.GetType())?.CssClass?.Split(' ').Where(c => c != "sfMvcIcn").JoinString(' ') ?? string.Empty;
            View = controller.DefaultUnconfiguredWidgetView;
            WithPresentationMode = withPresentationMode;
            AdditionalText = additionalText;
        }

    }
}