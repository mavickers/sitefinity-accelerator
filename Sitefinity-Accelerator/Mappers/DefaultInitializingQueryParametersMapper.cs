using System.Collections.Generic;
using SitefinityAccelerator.Interfaces;
using SitefinityAccelerator.Models;

namespace SitefinityAccelerator.Mappers
{
    public class DefaultInitializingQueryParametersMapper : IAugmentingInternalMapper<IQueryParameters>
    {
        private readonly int _defaultPageNumber;
        private readonly int _defaultPageSize;

        public DefaultInitializingQueryParametersMapper(int defaultPageNumber = 1, int defaultPageSize = 10)
        {
            _defaultPageNumber = defaultPageNumber;
            _defaultPageSize = defaultPageSize;
        }

        public IQueryParameters Map(IQueryParameters queryParameters)
        {
            queryParameters.Operator = Operator.AND;
            queryParameters.Sortings = new List<QuerySortParameter>();
            queryParameters.PageNumber = _defaultPageNumber;
            queryParameters.PageSize = _defaultPageSize;

            return queryParameters;
        }
    }
}