namespace SitefinityAccelerator.Interfaces
{
    public interface IFactory<out T1>
    {
        T1 Create(dynamic parameters = null);
    }
}