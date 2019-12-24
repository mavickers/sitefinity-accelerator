using System;

namespace SitefinityAccelerator.Interfaces
{
    public interface IQueryResults<T> : IQueryResultsMetrics
    {
        Type ResultType { get; }
        T Results { get; set; }
        IQueryParameters QueryParameters { get; set; }
    }
}
