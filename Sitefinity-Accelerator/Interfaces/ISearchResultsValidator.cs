using System;

namespace SitefinityAccelerator.Interfaces
{
    public interface ISearchResultsValidator : IValidator<ISearchResults>
    {
        new bool Validates(ISearchResults searchResults, out Exception exception);
    }
}