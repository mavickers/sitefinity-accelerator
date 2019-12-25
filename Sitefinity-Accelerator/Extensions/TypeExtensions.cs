using System;

namespace SitefinityAccelerator.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsLazy(this Type source)
        {
            return source.IsGenericType && source.GetGenericTypeDefinition() == typeof(Lazy<>);
        }
    }
}