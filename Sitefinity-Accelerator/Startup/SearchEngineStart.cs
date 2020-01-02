using SitefinityAccelerator.Configuration.Startup;
using System;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Web.Services;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;

namespace SitefinityAccelerator.Startup
{
    public class SearchEngineStart
    {
        private class WarmupParameters
        {
            public PublishingManager PublishingManager { get; set; }
            public ISearchService SearchService { get; set; }
            public ConfigElementList<SearchIndexStartupElement> Indexes { get; set; }
        }

        public static void Warmup(ConfigElementList<SearchIndexStartupElement> indexes)
        {
            // adopted from:
            // - https://knowledgebase.progress.com/articles/Article/how-to-run-api-as-background-task-in-sitefinity
            // 
            // we aren't actually running this in the background here but it shows how to run 
            // with elevated privileges; we may want to actually run these in the background at some point.

            var searchIndexWarmupWorker = new SystemManager.RunWithElevatedPrivilegeDelegate(DoWarmup);

            SystemManager.RunWithElevatedPrivilege(searchIndexWarmupWorker, new object[] { indexes });
        }

        private static void DoWarmup(object[] args)
        {
            // adopted from:
            // - https://knowledgebase.progress.com/articles/Article/how-to-programmatically-launch-a-reindex
            // - https://www.progress.com/documentation/sitefinity-cms/for-developers-index-external-content

            if (!TryInitialize(args, out var parameters)) return;

            foreach (var index in parameters.Indexes)
            {
                if (!parameters.SearchService.IndexExists(index.IndexName))
                {
                    var publishingPoint = parameters.PublishingManager.GetPublishingPoints().FirstOrDefault(p => p.Name == index.PublishingPointName);

                    if (publishingPoint != null)
                    {
                        var publishingAdminService = new PublishingAdminService();

                        Log.Write($"Indexing {index.IndexName} {DateTime.Now}");

                        publishingAdminService.ReindexSearchContent(PublishingConfig.SearchProviderName, publishingPoint.Id.ToString());
                    }
                }
            }
        }

        private static bool TryInitialize(object[] args, out WarmupParameters parameters)
        {
            parameters = null;

            var indexes = args?[0] as ConfigElementList<SearchIndexStartupElement>;

            if (indexes == null) return false;

            var publishingManager = PublishingManager.GetManager(PublishingConfig.SearchProviderName);

            if (publishingManager == null) return false;

            var searchService = ServiceBus.ResolveService<ISearchService>();

            if (searchService == null) return false;

            parameters = new WarmupParameters
            {
                PublishingManager = publishingManager,
                SearchService = searchService,
                Indexes = indexes
            };

            return true;
        }
    }
}