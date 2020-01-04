using SitefinityAccelerator.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BackgroundTasks;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Web.Services;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;

namespace SitefinityAccelerator.Startup
{
    /// <summary>
    /// Warms up search indexes using search index configuration data.
    /// </summary>
    /// <remarks>
    /// adopted from:
    /// - https://knowledgebase.progress.com/articles/Article/how-to-run-api-as-background-task-in-sitefinity
    /// - https://knowledgebase.progress.com/articles/Article/how-to-programmatically-launch-a-reindex
    /// - https://www.progress.com/documentation/sitefinity-cms/for-developers-index-external-content
    /// </remarks>
    public class SearchEngineStart
    {
        private class WarmupParameters
        {
            public PublishingAdminService PublishingAdminService { get; set; }
            public PublishingManager PublishingManager { get; set; }
            public ISearchService SearchService { get; set; }
            public IEnumerable<SearchIndexStartupElement> Indexes { get; set; }
            public SystemManager.RunWithElevatedPrivilegeDelegate WorkerDelegate { get; set; }
        }

        private class WarmupBackgroundTask : IBackgroundTask
        {
            private readonly PublishingPoint _publishingPoint;
            private readonly WarmupParameters _warmupParameters;

            public WarmupBackgroundTask(PublishingPoint publishingPoint, WarmupParameters warmupParameters)
            {
                _publishingPoint = publishingPoint ?? throw new ArgumentException("publishingPoint");
                _warmupParameters = warmupParameters ?? throw new ArgumentException("warmupParameters");
            }

            public void Run(IBackgroundTaskContext context)
            {
                SystemManager.RunWithElevatedPrivilege(_warmupParameters.WorkerDelegate, new object[] { _publishingPoint, _warmupParameters.PublishingAdminService });
            }
        }

        public static void Warmup(ConfigElementList<SearchIndexStartupElement> indexes)
        {
            if (!TryInitialize(indexes, out var parameters)) return;

            foreach (var index in parameters.Indexes)
            {
                var publishingPoint = parameters.PublishingManager.GetPublishingPoints().FirstOrDefault(p => p.Name == index.PublishingPointName);

                // can't find the index, so abort for this entry

                if (publishingPoint == null) continue;

                // if the index is already present and not configured for forced rebuild, skip it

                if (!index.WithForceRebuild && parameters.SearchService.IndexExists(index.IndexName)) continue;

                if (index.WithBackgroundIndexing)
                {
                    SystemManager.BackgroundTasksService.EnqueueTask(new WarmupBackgroundTask(publishingPoint, parameters));
                }
                else
                {
                    SystemManager.RunWithElevatedPrivilege(parameters.WorkerDelegate, new object[] { publishingPoint, parameters.PublishingAdminService });
                }
            }
        }

        private static void DoWarmup(object[] args)
        {
            var publishingPoint = args?[0] as PublishingPoint ?? throw new ArgumentException("args[0] (PublishingPoint)");
            var publishingAdminService = args?[1] as PublishingAdminService ?? throw new ArgumentException("args[1] (PublishingAdminService");

            Log.Write($"Indexing {publishingPoint.Name} {DateTime.Now}");

            publishingAdminService.ReindexSearchContent(PublishingConfig.SearchProviderName, publishingPoint.Id.ToString());
        }

        private static bool TryInitialize(ConfigElementList<SearchIndexStartupElement> indexes, out WarmupParameters parameters)
        {
            parameters = null;

            if (!(indexes?.Count > 0)) return false;

            var publishingManager = PublishingManager.GetManager(PublishingConfig.SearchProviderName);

            if (publishingManager == null) return false;

            var searchService = ServiceBus.ResolveService<ISearchService>();

            if (searchService == null) return false;

            parameters = new WarmupParameters
            {
                PublishingAdminService = new PublishingAdminService(),
                PublishingManager = publishingManager,
                SearchService = searchService,
                Indexes = ((IList<SearchIndexStartupElement>)indexes).OrderBy(i => i.WithBackgroundIndexing),
                WorkerDelegate = new SystemManager.RunWithElevatedPrivilegeDelegate(DoWarmup)
            };

            return true;
        }
    }
}