using System.Collections.Generic;
using SitefinityAccelerator.Interfaces;
using Telerik.Sitefinity.Services.Search;

namespace MyFloridaCfoWeb.Application.Search
{
    public class SearchIndexFilterOperatorStrategy : IGenericParsingStrategy<string, KeyValuePair<string, FilterOperator>>
    {
        public KeyValuePair<string, FilterOperator> Parse(string filterValue)
        {
            switch (filterValue[0])
            {
                case '*': return new KeyValuePair<string, FilterOperator>(filterValue.Substring(1), FilterOperator.Contains);
                case '>': return new KeyValuePair<string, FilterOperator>(filterValue.Substring(1), FilterOperator.Greater);
                case '<': return new KeyValuePair<string, FilterOperator>(filterValue.Substring(1), FilterOperator.Less);
                default: return new KeyValuePair<string, FilterOperator>(filterValue, FilterOperator.Equals);
            }
        }
    }
}