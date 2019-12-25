using System;

namespace SitefinityAccelerator.Extensions
{
    public static class QueryExtensions
    {
        public static int TotalPages(int totalCount, int pageSize)
        {
            return (int)Math.Floor((double)totalCount / pageSize) + (totalCount % pageSize == 0 ? 0 : 1);
        }
        public static int ValidatedPageNumber(int totalPages, int requestedPageNumber)
        {
            return requestedPageNumber > 0 && requestedPageNumber <= totalPages ? requestedPageNumber : 1;
        }
    }
}