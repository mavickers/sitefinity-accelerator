namespace SitefinityAccelerator.Interfaces
{
    public interface IQueryResultsMetrics
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        int TotalPages { get; set; }
    }
}
