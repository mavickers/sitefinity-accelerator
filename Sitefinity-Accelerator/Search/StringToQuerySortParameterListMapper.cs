using System;
using System.Collections.Generic;
using System.Linq;
using SitefinityAccelerator.Interfaces;
using SitefinityAccelerator.Models;

namespace SitefinityAccelerator.Search
{
    public class StringToQuerySortParameterListMapper : IGenericMapper<string, List<QuerySortParameter>>
    {
        public List<QuerySortParameter> Map(string item)
        {
            if (item == null)
            {
                return null;
            }

            // first let's break the fields down in the string to a list of string arrays; this
            // should represent each field (separated by comma) which is further represented
            // by field name and sort direction (a two-item string array).
            var fields = item.Split(',').Select(s => s.Trim()).Where(s => !s.IsNullOrWhitespace()).Select(s => s.Split(' '));

            // we only want string arrays with one or two items and the second item is "desc"
            fields = fields.Where(f => f.Length == 1 || (f.Length == 2 && f[1].ToUpper() == "DESC"));

            // now return this as a list of QuerySortParameter

            return fields.Select(f => new QuerySortParameter(f[0], f.Length == 2 ? OrderBy.DESCENDING : OrderBy.ASCENDING)).ToList();
        }
    }
}