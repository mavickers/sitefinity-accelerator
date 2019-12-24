using Telerik.Sitefinity.Services.Search;

namespace SitefinityAccelerator.Interfaces
{
    public interface ISearchFilterMapper<in T> : IGenericMapper<T, SearchFilter> { }
}