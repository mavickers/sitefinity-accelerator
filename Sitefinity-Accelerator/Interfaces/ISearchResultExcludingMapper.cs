using System.Collections.Generic;

namespace SitefinityAccelerator.Interfaces
{
    /// <summary>
    /// Interface for mapping an individual search result to a target type
    /// with an option to set exclusions.
    /// </summary>
    /// <remarks>
    /// The use-case for exclusions is for querying a content tree that
    /// has the possibility of a recursion loop. Use the Exclude method
    /// to push parent items down the query tree so the loop may be
    /// averted.
    /// </remarks>
    /// <typeparam name="T1"></typeparam>
    public interface ISearchResultExcludingMapper<out T1> : ISearchResultMapper<T1>
    {
        ISearchResultExcludingMapper<T1> Exclude<T2>(IEnumerable<T2> excludingItems);
    }
}