using System;
using System.Collections.Generic;
using SitefinityAccelerator.Search;
using Telerik.Sitefinity.Services.Search;

namespace SitefinityAccelerator.Extensions
{
    public static class SearchFilterExtensions
    {
        public static SearchFilter BuildFilters(this SearchFilter searchFilter, List<FilterModel> filterList)
        {
            foreach (var filter in filterList)
            {
                // There may be aspects of this not working correctly. Previously "Contains" vs. "Equals"
                // didn't seem to make a difference, also can't search with phrases using any syntax - "one two", one?two,
                // "one?two", one*two, etc. That may need to be chased down at some point in order to filter on exact phrases
                // such as those in categories. These issues were occurring under a pervious version of the
                // Lucene interface; the latest version clears up a lot of issues, and perhaps these as well.

                foreach (var filterValues in filter.FilterValue.Trim().ToLower().Split(new[] { " ", "&" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    searchFilter.AddClause(new SearchFilterClause(filter.FilterTerm, filterValues, filter.FilterOperator));
                }
            }

            return searchFilter;
        }
    }
}