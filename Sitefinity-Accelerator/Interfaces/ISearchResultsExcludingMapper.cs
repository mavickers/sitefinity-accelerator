using System.Collections.Generic;

namespace SitefinityAccelerator.Interfaces
{
    /// <summary>
    /// Interface for mapping search index results to a collection
    /// of a target type with an option to set exclusions.
    /// </summary>
    /// <remarks>
    /// The use-case for exclusions is for querying a content tree that
    /// has the possibility of a recursion loop. Use the Exclude method
    /// to push parent items down the query tree so the loop may be
    /// averted.
    /// </remarks>
    /// <typeparam name="T1"></typeparam>
    public interface ISearchResultsExcludingMapper<out T1> : ISearchResultsMapper<T1>
    {
        ISearchResultsExcludingMapper<T1> Exclude<T2>(IEnumerable<T2> excludingItems);
    }
}