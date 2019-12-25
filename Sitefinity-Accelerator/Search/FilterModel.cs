using Microsoft.Extensions.DependencyInjection;
using Ninject;
using SitefinityAccelerator.Interfaces;
using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Services.Search;

namespace SitefinityAccelerator.Search
{
    public class FilterModel
    {
        private readonly Func<string, KeyValuePair<string, FilterOperator>> _parseFilterValue;

        public string FilterTerm { get; set; }
        public string FilterValue { get; set; }
        public FilterOperator FilterOperator { get; set; }

        public FilterModel(IGenericParsingStrategy<string, KeyValuePair<string, FilterOperator>> parsingStrategy)
        {
            _parseFilterValue = parsingStrategy.Parse;
        }

        public static FilterModel Create(string filterTerm, string filterValue, StandardKernel kernel)
        {
            if (filterTerm.IsNullOrWhitespace() || filterValue.IsNullOrWhitespace())
            {
                return null;
            }

            var filterModel = kernel.GetService<FilterModel>();
            var parsedValue = filterModel._parseFilterValue(filterValue);

            filterModel.FilterTerm = filterTerm;
            filterModel.FilterValue = parsedValue.Key;
            filterModel.FilterOperator = parsedValue.Value;

            return filterModel;
        }
    }
}