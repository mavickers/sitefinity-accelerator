namespace SitefinityAccelerator.Models
{
    public struct QuerySortParameter
    {
        public string FieldName { get; set; }
        public OrderBy OrderBy { get; set; }

        public QuerySortParameter(string fieldName, OrderBy orderBy)
        {
            FieldName = fieldName;
            OrderBy = orderBy;
        }
    }
}