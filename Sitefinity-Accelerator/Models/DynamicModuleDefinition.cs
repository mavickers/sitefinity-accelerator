using System;
using System.Linq;
using System.Reflection;
using System.Web.Configuration;
using SitefinityAccelerator.Interfaces;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace SitefinityAccelerator.Models
{
    public class DynamicModuleDefinition : IDynamicModuleDefinition
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public DynamicModuleType DynamicModuleType { get; }
        public Type InboundPipeType { get; set; }

        public DynamicModuleDefinition(DynamicModuleType dynamicModuleType)
        {
            Name = $"{dynamicModuleType.TypeNamespace.Split('.').Last()}.{dynamicModuleType.TypeName}";
            Type = TypeResolutionService.ResolveType($"{dynamicModuleType.TypeNamespace}.{dynamicModuleType.TypeName}");
            DynamicModuleType = dynamicModuleType;

            var inboundPipeTypeName = WebConfigurationManager.AppSettings[$"InboundPipe.{Name}"];
            var inboundPipeType = inboundPipeTypeName.IsNullOrWhitespace() ? null : Type.GetType(inboundPipeTypeName);

            InboundPipeType = inboundPipeType;
        }
    }
}