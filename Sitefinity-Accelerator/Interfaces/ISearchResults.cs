using System.Collections.Generic;
using Telerik.Sitefinity.Services.Search.Data;

namespace SitefinityAccelerator.Interfaces
{
    /// <summary>
    /// Shortcut interface to search index search results interface.
    /// </summary>
    public interface ISearchResults : IQueryResults<IEnumerable<IDocument>> { }
}