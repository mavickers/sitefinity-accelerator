using System.Linq;
using Ninject;
using Ninject.Activation;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;

namespace SitefinityAccelerator.Extensions
{
    public static class DynamicModuleManagerExtensions
    {
        public static DynamicModuleManager GetDynamicModuleManager(IContext context)
        {
            // lifted this from https://knowledgebase.progress.com/articles/Article/get-module-providers-for-site-in-multisite

            if (!SystemManager.CurrentContext?.IsMultisiteMode ?? true)
            {
                return DynamicModuleManager.GetManager();
            }

            var multisiteContext = SystemManager.CurrentContext as MultisiteContext;

            if (multisiteContext == null)
            {
                return null;
            }

            if (context == null)
            {
                return null;
            }

            if (context.Request.Constraint?.Target != null)
            {
                var name = ((NamedAttribute) context.Request.Constraint.Target).Name;
                var datasourceLink = multisiteContext.CurrentSite.SiteDataSourceLinks.FirstOrDefault(ds => ds.DataSourceName == name);

                return datasourceLink == null ? null : DynamicModuleManager.GetManager(datasourceLink.ProviderName);
            }

            return null;

            /*
             * Reference
             *
             
            // All data source links for current site
            List<MultisiteContext.SiteDataSourceLinkProxy> dataSources = multisiteContext.CurrentSite.SiteDataSourceLinks.ToList();

            string librariesDataSourceName = typeof(LibrariesManager).FullName; // "Telerik.Sitefinity.Modules.Libraries.LibrariesManager"

            // Get the module's providers by the module's name

            // Get the default provider
            var defaultProvider = multisiteContext.CurrentSite.GetDefaultProvider(librariesDataSourceName);

            // Get the providers for news for current site
            var newsSiteProvs = multisiteContext.CurrentSite.GetProviders(typeof(NewsManager).FullName).Select(p => p.ProviderName);

            // Get the providers for "Press releases" dynamic module for current site
            var pressReleasesSiteProvs = multisiteContext.CurrentSite.GetProviders("Press releases").Select(p => p.ProviderName);

            *
            *
            */
        }
    }
}