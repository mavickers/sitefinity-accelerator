using System;
using System.Linq;
using System.Web.Mvc;
using SitefinityAccelerator.Exceptions;
using SitefinityAccelerator.Extensions;
using SitefinityAccelerator.Models;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;

namespace SitefinityAccelerator.Mvc
{
    public class BaseController : Controller
    {
        private readonly BaseControllerParameters _baseControllerParameters;

        protected static bool IsDesignMode => (SystemManager.IsDesignMode || SystemManager.IsInlineEditingMode) && !SystemManager.IsPreviewMode;

        public string DefaultUnconfiguredWidgetView => _baseControllerParameters.DefaultUnconfiguredWidgetView;

        public BaseController(BaseControllerParameters baseControllerParameters)
        {
            _baseControllerParameters = baseControllerParameters;
        }

        public ActionResult HandledException(Exception exception, string errorView = null)
        {
            // if we are handling an unconfigured widget exception and we're in design
            // mode we just want to show an unconfigured widget view...

            if (exception is UnconfiguredWidgetException unconfiguredException && _baseControllerParameters.IsDesignMode)
            {
                var toolboxItem = _baseControllerParameters.ToolboxesConfig.GetToolboxItem(GetType());
                var view = unconfiguredException.View ?? _baseControllerParameters.DefaultUnconfiguredWidgetView;
                var text = unconfiguredException.Message ?? $"Configure {toolboxItem?.Title ?? "Widget"}";
                var textAdditional = unconfiguredException.AdditionalText;
                var classes = unconfiguredException.Classes ?? toolboxItem?.CssClass?.Split(' ').Where(c => c != "sfMvcIcn").JoinString(' ');

                return View(view, new GenericUnconfiguredWidgetViewModel(text, textAdditional, text, classes, unconfiguredException.WithPresentationMode));
            }

            // here we are trapping an exception that we want to display in design mode

            if (exception is DesignModeException designModeException && _baseControllerParameters.IsDesignMode)
            {
                return View(_baseControllerParameters.DefaultGenericDesignView, designModeException.HtmlMessage);
            }

            // otherwise we want to write the exception out to the log file...

            Log.Write(exception, ConfigurationPolicy.ErrorLog);

            // and if an errorView was passed along then return that; if not then
            // just return an empty view; the errorView should accept an exception 
            // for the model.

            errorView = string.IsNullOrWhiteSpace(errorView) ? "GenericErrorView" : errorView;

            return View(errorView, exception);
        }
    }
}