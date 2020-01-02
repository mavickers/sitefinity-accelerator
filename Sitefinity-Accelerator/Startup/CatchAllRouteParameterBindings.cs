using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace SitefinityAccelerator.Startup
{
    public class CatchAllRouteParameterBinding : HttpParameterBinding
    {
        // This was lifted from http://www.tugberkugurlu.com/archive/asp-net-web-api-catch-all-route-parameter-binding

        private readonly string _parameterName;
        private readonly char _delimiter;

        public CatchAllRouteParameterBinding(HttpParameterDescriptor descriptor, char delimiter) : base(descriptor)
        {
            _parameterName = descriptor.ParameterName;
            _delimiter = delimiter;
        }

        public override Task ExecuteBindingAsync(System.Web.Http.Metadata.ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var routeValues = actionContext.ControllerContext.RouteData.Values;

            if (routeValues[_parameterName] != null)
            {
                string[] vals = routeValues[_parameterName].ToString().Split(_delimiter);

                actionContext.ActionArguments.Add(_parameterName, vals.Where(v => !v.Trim().IsNullOrEmpty()).ToArray());
            }
            else
            {
                actionContext.ActionArguments.Add(_parameterName, new string[0]);
            }

            return Task.FromResult(0);
        }
    }
}