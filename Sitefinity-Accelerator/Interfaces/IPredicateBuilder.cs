using System;
using System.Linq.Expressions;

namespace SitefinityAccelerator.Interfaces
{
    public interface IPredicateBuilder<T>
    {
        Expression<Func<T, bool>> BuildFilter(IQueryParameters queryParameters);
    }
}