namespace SitefinityAccelerator.Interfaces
{
    public interface IGenericParsingStrategy<in T1, out T2>
    {
        T2 Parse(T1 input);
    }
}