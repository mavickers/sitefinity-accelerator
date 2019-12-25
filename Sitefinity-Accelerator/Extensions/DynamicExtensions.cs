using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace SitefinityAccelerator.Extensions
{
    public static class DynamicExtensions
    {
        public static IDictionary<string, object> ToDictionary(dynamic source)
        {
            if (source == null)
            {
                return null;
            }

            return new RouteValueDictionary(source);
        }

        public static T SetProperties<T>(this T destination, dynamic source)
        {
            if (destination == null)
            {
                return default(T);
            }

            var destType = destination.GetType();
            var destPropertiesDictionary = destType.GetProperties().ToDictionary(x => x.Name, x => x.GetValue(destination, null));
            var sourcePropertiesDictionary = ToDictionary(source);

            foreach (var sourceProperty in sourcePropertiesDictionary)
            {
                if (!destPropertiesDictionary.ContainsKey(sourceProperty.Key))
                {
                    continue;
                }

                if (destPropertiesDictionary[sourceProperty.Key].GetType() != sourceProperty.Value.GetType())
                {
                    continue;
                }

                destType.GetProperty(sourceProperty.Key)?.SetValue(destination, sourceProperty.Value);
            }

            return destination;
        }
    }
}