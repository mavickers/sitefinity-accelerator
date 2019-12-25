using System;

namespace SitefinityAccelerator.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ClientValueAttribute : Attribute
    {
        public string ClientValue { get; }

        public ClientValueAttribute(string clientValue)
        {
            ClientValue = clientValue;
        }
    }
}