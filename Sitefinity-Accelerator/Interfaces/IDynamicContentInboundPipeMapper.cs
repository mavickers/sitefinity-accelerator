using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Publishing;

namespace SitefinityAccelerator.Interfaces
{
    public interface IDynamicContentInboundPipeMapper : IAugmentingExternalMapper<DynamicContent, WrapperObject>
    {
        string Logs { get; }
    }
}