using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitefinityAccelerator.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchIndexInitializingModel
    {
        public string IndexName { get; set; }
        public string PublishingPointName { get; set; }

        /// <summary>
        /// Run the indexing in the background. This allows the website to
        /// be available before the index is fully built.
        /// </summary>
        public bool WithBackgroundIndexing { get; set; }
    }
}
