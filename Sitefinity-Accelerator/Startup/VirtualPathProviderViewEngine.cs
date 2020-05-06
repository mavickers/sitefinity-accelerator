using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace SitefinityAccelerator.Startup
{
    public class VirtualPathProviderViewEngine : System.Web.Mvc.VirtualPathProviderViewEngine
    {
        private readonly string[] _rootModuleNamespaces;

        private VirtualPathProviderViewEngine(string[] rootModuleNamespaces)
        {
            _rootModuleNamespaces = rootModuleNamespaces;
        }

        public static VirtualPathProviderViewEngine Create(string[] rootModuleNamespaces, string[] locationFormats)
        {
            var engine = new VirtualPathProviderViewEngine(rootModuleNamespaces);

            // find all the namespaces created by the /Application/Modules folder structure
            // and pluck the top-level names out to create new search locations for views;
            // we do not want to search the actual folder structure via system.io because we do 
            // not want to deploy that folder.

            var moduleTypes = Assembly.GetExecutingAssembly().GetTypes().Where(engine.IsAssemblyMatchRootNamespace).Select(t => t.Namespace).Distinct();
            var classNames = moduleTypes.Select(t => t.Split('.')[3]).ToList();
            var additionalLocations = new List<string>();

            classNames.ForEach(t => additionalLocations.AddRange(locationFormats.Where(f => f.Contains("{1}")).Select(format => format.Replace("{1}", t))));

            var allLocationFormats = locationFormats.Concat(additionalLocations).ToArray();

            engine.FileExtensions = new[] { "cshtml", "vbhtml", "aspx", "ascx" };
            engine.AreaViewLocationFormats = allLocationFormats;
            engine.AreaMasterLocationFormats = allLocationFormats;
            engine.AreaPartialViewLocationFormats = allLocationFormats;
            engine.ViewLocationFormats = allLocationFormats;
            engine.MasterLocationFormats = allLocationFormats;
            engine.PartialViewLocationFormats = allLocationFormats;

            return engine;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            if (partialPath.EndsWith(".cshtml") || partialPath.EndsWith(".vbhtml"))
            {
                return new RazorView(controllerContext, partialPath, null, false, null);
            }

            return new WebFormView(controllerContext, partialPath);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            if (viewPath.EndsWith(".cshtml") || viewPath.EndsWith(".vbhtml"))
            {
                return new RazorView(controllerContext, viewPath, masterPath, false, null);
            }

            return new WebFormView(controllerContext, viewPath, masterPath);
        }

        private bool IsAssemblyMatchRootNamespace(System.Type assemblyType)
        {
            return _rootModuleNamespaces.Any(ns => (assemblyType.Namespace?.StartsWith(ns) ?? false) && assemblyType.Namespace != ns);
        }
    }
}
