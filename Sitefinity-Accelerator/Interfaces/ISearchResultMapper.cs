using Telerik.Sitefinity.Services.Search.Data;

namespace SitefinityAccelerator.Interfaces
{
    /// <summary>
    /// Interface for mapping an individual search result to a target type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchResultMapper<out T> : IGenericMapper<IDocument, T> { }
}