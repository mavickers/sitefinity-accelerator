namespace SitefinityAccelerator.Interfaces
{
    /// <summary>
    /// Interface for mapping the output of a search index query to a target type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchResultsMapper<out T> : IGenericMapper<ISearchResults, T> { }
}