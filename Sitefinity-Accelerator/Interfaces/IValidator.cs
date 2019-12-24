using System;

namespace SitefinityAccelerator.Interfaces
{
    public interface IValidator<in T>
    {
        bool Validates(T data, out Exception exception);
    }
}