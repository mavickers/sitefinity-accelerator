using System;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace SitefinityAccelerator.Interfaces
{
    public interface IDynamicModuleDefinition
    {
        string Name { get; set; }
        Type Type { get; set; }
        DynamicModuleType DynamicModuleType { get; }
        Type InboundPipeType { get; set; }
    }
}