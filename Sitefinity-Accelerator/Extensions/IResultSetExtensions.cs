using System.Collections.Generic;
using System.Linq;
using SitefinityAccelerator.Models;
using Telerik.Sitefinity.Services.Search.Data;

namespace SitefinityAccelerator.Extensions
{
    public static class IResultSetExtensions
    {
        public static IEnumerable<IDocument> OrderBy(this IResultSet source, List<QuerySortParameter> sortings)
        {
            if (!sortings?.Any() ?? false)
            {
                return source;
            }

            var sortedResults = default(IOrderedEnumerable<IDocument>);

            foreach (var sort in sortings)
            {
                if (sortedResults == default(IOrderedEnumerable<IDocument>))
                {
                    sortedResults = sort.OrderBy == SitefinityAccelerator.Models.OrderBy.DESCENDING
                        ? source.OrderByDescending(s => s.GetValue(sort.FieldName))
                        : source.OrderBy(s => s.GetValue(sort.FieldName));
                }
                else
                {
                    sortedResults = sort.OrderBy == SitefinityAccelerator.Models.OrderBy.DESCENDING
                        ? sortedResults.ThenByDescending(s => s.GetValue(sort.FieldName))
                        : sortedResults.ThenBy(s => s.GetValue(sort.FieldName));
                }
            }

            return sortedResults;
        }
    }
}