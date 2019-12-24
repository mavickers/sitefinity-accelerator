using System.Collections.Generic;
using SitefinityAccelerator.Models;

namespace SitefinityAccelerator.Interfaces
{
    public interface IQueryParameters
    {
        Operator Operator { get; set; }
        List<QuerySortParameter> Sortings { get; set; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
        bool IsAny { get; }
        DynamicModuleDefinition DynamicModuleDefinition { get; }
    }
}