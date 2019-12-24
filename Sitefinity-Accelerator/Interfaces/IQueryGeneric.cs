namespace SitefinityAccelerator.Interfaces
{
    public interface IQueryGeneric<T>
    {
        IQueryResults<T> GetResults(IQueryParameters queryParameters = null);
    }
}
