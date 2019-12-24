using System;
using System.Collections.Generic;
using SitefinityAccelerator.Interfaces;
using Telerik.Sitefinity.Services.Search.Data;

namespace SitefinityAccelerator.Models
{
    public class SearchIndexQueryResultsModel : ISearchResults
    {
        public Type ResultType { get; }
        public IEnumerable<IDocument> Results { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IQueryParameters QueryParameters { get; set; }

        public SearchIndexQueryResultsModel()
        {
            ResultType = typeof(IResultSet);
            Results = null;
            PageNumber = 1;
            PageSize = int.MaxValue;
            TotalCount = 0;
            TotalPages = 0;
            QueryParameters = null;
        }

    }
}