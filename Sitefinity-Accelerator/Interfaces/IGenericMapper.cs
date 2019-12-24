namespace SitefinityAccelerator.Interfaces
{
    public interface IGenericMapper<in T1, out T2>
    {
        T2 Map(T1 item);
    }
}
