using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Web.Services;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;

namespace SitefinityAccelerator.Startup
{
    public class SearchEngineStart
    {
        public static void Warmup(Dictionary<string, string> indexes)
        {
            // borrowed from:
            // - https://knowledgebase.progress.com/articles/Article/how-to-run-api-as-background-task-in-sitefinity
            // 
            // we aren't actually running this in the background here but it shows how to run 
            // with elevated privileges; we may want to actually run these in the background at some point.

            var searchIndexWarmupWorker = new SystemManager.RunWithElevatedPrivilegeDelegate(DoWarmup);

            SystemManager.RunWithElevatedPrivilege(searchIndexWarmupWorker, new object[] { indexes });
        }

        private static void DoWarmup(object[] args)
        {
            // borrowed from:
            // - https://knowledgebase.progress.com/articles/Article/how-to-programmatically-launch-a-reindex
            // - https://www.progress.com/documentation/sitefinity-cms/for-developers-index-external-content

            // todo: change Dictionary<string,string> to typed class

            var publishingManager = PublishingManager.GetManager(PublishingConfig.SearchProviderName);
            var indexes = args?[0] as Dictionary<string, string> ?? new Dictionary<string, string>();

            foreach (var index in indexes)
            {
                if (!ServiceBus.ResolveService<ISearchService>().IndexExists(index.Key))
                {
                    var publishingPoint = publishingManager.GetPublishingPoints().FirstOrDefault(p => p.Name == index.Value);

                    if (publishingPoint != null)
                    {
                        var publishingAdminService = new PublishingAdminService();

                        Log.Write($"Indexing {index.Key} {DateTime.Now}");

                        publishingAdminService.ReindexSearchContent(PublishingConfig.SearchProviderName, publishingPoint.Id.ToString());
                    }
                }
            }
        }
    }
}