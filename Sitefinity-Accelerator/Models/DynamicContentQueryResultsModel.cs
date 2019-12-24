using System;
using System.Collections.Generic;
using SitefinityAccelerator.Interfaces;
using Telerik.Sitefinity.DynamicModules.Model;

namespace SitefinityAccelerator.Models
{
    public class DynamicContentQueryResultsModel : IEnumerableQueryResults<DynamicContent>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IQueryParameters QueryParameters { get; set; }
        public IEnumerable<DynamicContent> Results { get; set; }
        public Type ResultType { get; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public DynamicContentQueryResultsModel()
        {
            ResultType = typeof(IEnumerableQueryResults<DynamicContent>);
            Results = null;
            PageNumber = 1;
            PageSize = int.MaxValue;
            TotalCount = 0;
            TotalPages = 0;
            QueryParameters = null;
        }
    }
}