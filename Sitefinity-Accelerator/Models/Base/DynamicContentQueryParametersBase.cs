using SitefinityAccelerator.Interfaces;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;

namespace SitefinityAccelerator.Models.Base
{
    public abstract class DynamicContentQueryParametersBase : QueryParametersBase
    {
        public readonly DynamicModuleManager DynamicModuleManager;
        public readonly IPredicateBuilder<DynamicContent> PredicateBuilder;
        public new readonly DynamicModuleDefinition DynamicModuleDefinition;

        protected DynamicContentQueryParametersBase
        (
            DynamicModuleManager dynamicModuleManager,
            DynamicModuleDefinition dynamicModuleDefinition,
            IPredicateBuilder<DynamicContent> predicateBuilder
        )
        {
            DynamicModuleManager = dynamicModuleManager;
            DynamicModuleDefinition = dynamicModuleDefinition;
            PredicateBuilder = predicateBuilder;
        }
    }
}