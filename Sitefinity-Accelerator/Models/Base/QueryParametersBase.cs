using System.Collections.Generic;
using SitefinityAccelerator.Interfaces;

namespace SitefinityAccelerator.Models.Base
{
    public abstract class QueryParametersBase : IQueryParameters
    {
        public Operator Operator { get; set; }
        public List<QuerySortParameter> Sortings { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public abstract bool IsAny { get; }
        public DynamicModuleDefinition DynamicModuleDefinition { get; }

        protected QueryParametersBase(int pageSize = 10)
        {
            Operator = Operator.AND;
            Sortings = new List<QuerySortParameter>();
            PageNumber = 1;
            PageSize = pageSize;
            DynamicModuleDefinition = null;
        }
    }
}