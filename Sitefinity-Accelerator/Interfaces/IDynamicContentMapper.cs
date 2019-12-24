using Telerik.Sitefinity.DynamicModules.Model;

namespace SitefinityAccelerator.Interfaces
{
    public interface IDynamicContentMapper<out T> : IGenericMapper<DynamicContent, T>
    {
        new T Map(DynamicContent item);
    }
}
