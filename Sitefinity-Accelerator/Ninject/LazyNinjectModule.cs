using System;
using Ninject.Modules;

namespace SitefinityAccelerator.Ninject
{
    public abstract class LazyNinjectModule : NinjectModule
    {
        /// <summary>
        /// Facilitates simultaneous binding of eager and lazy versions of same interface/class.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public FullBindingToSyntax<T1> FullBind<T1>()
        where T1 : class
        {
            var eager = Bind<T1>();
            var lazy = Bind<Lazy<T1>>();

            return new FullBindingToSyntax<T1>(eager, lazy, Kernel);
        }
    }
}