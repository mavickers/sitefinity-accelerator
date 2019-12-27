using System;
using System.Web.Mvc;
using SitefinityAccelerator.Interfaces;
using Telerik.Sitefinity.Services;

namespace SitefinityAccelerator.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Abstract controller with stubbing for querying a search index.
    /// </summary>
    public abstract class SearchIndexController<T> : BaseController
    {
        private readonly BaseControllerParameters _baseControllerParameters;
        protected readonly IQueryParametersMapper<T> QueryParametersMapper;
        protected readonly IFactory<T> _parametersFactory;
        private readonly IQuerySearchIndex _query;
        private readonly Lazy<IQuerySearchIndex> _queryLazy;
        protected ActionResult ErrorAction => SystemManager.IsDesignMode ? (ActionResult)Content("") : View(_baseControllerParameters.DefaultGenericDesignView);
        protected readonly bool IsLazy = false;

        protected IQuerySearchIndex Query => IsLazy ? _queryLazy.Value : _query;

        #region Eager Constructors

        protected SearchIndexController(BaseControllerParameters baseControllerParameters) : base(baseControllerParameters)
        {
            _baseControllerParameters = baseControllerParameters;
        }
        protected SearchIndexController
        (
            BaseControllerParameters baseControllerParameters,
            IQuerySearchIndex query
        ) : base(baseControllerParameters)
        {
            _baseControllerParameters = baseControllerParameters;
            _query = query;
        }
        protected SearchIndexController
        (
            BaseControllerParameters baseControllerParameters,
            IFactory<T> parametersFactory,
            IQuerySearchIndex query
        ) : base(baseControllerParameters)
        {
            _baseControllerParameters = baseControllerParameters;
            _parametersFactory = parametersFactory;
            _query = query;
            IsLazy = true;
        }
        protected SearchIndexController
        (
            BaseControllerParameters baseControllerParameters,
            IQueryParametersMapper<T> queryParametersMapper,
            IQuerySearchIndex query
        ) : base(baseControllerParameters)
        {
            _baseControllerParameters = baseControllerParameters;
            QueryParametersMapper = queryParametersMapper;
            _query = query;
            IsLazy = true;
        }

        #endregion

        #region Lazy Constructors

        protected SearchIndexController
        (
            BaseControllerParameters baseControllerParameters,
            Lazy<IQuerySearchIndex> queryLazy
        ) : base(baseControllerParameters)
        {
            _baseControllerParameters = baseControllerParameters;
            _queryLazy = queryLazy;
            IsLazy = true;
        }
        protected SearchIndexController
        ( 
            BaseControllerParameters baseControllerParameters,
            IFactory<T> parametersFactory,
            Lazy<IQuerySearchIndex> queryLazy
        ) : base(baseControllerParameters)
        {
            _baseControllerParameters = baseControllerParameters;
            _parametersFactory = parametersFactory;
            _queryLazy = queryLazy;
            IsLazy = true;
        }
        protected SearchIndexController
        (
            BaseControllerParameters baseControllerParameters,
            IQueryParametersMapper<T> queryParametersMapper,
            Lazy<IQuerySearchIndex> queryLazy
        ) : base(baseControllerParameters)
        {
            _baseControllerParameters = baseControllerParameters;
            QueryParametersMapper = queryParametersMapper;
            _queryLazy = queryLazy;
            IsLazy = true;
        }

        #endregion
    }
}