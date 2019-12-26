using System;
using Ninject;
using Ninject.Syntax;

namespace SitefinityAccelerator.Ninject
{
    public class FullBindingToSyntax<T1>
    where T1 : class
    {
        private readonly IBindingToSyntax<T1> _eager;
        private readonly IBindingToSyntax<Lazy<T1>> _lazy;
        private readonly IKernel _kernel;

        public FullBindingToSyntax
        (
            IBindingToSyntax<T1> eager, 
            IBindingToSyntax<Lazy<T1>> lazy,
            IKernel kernel
        )
        {
            _eager = eager;
            _lazy = lazy;
            _kernel = kernel;
        }

        public FullBindingWhenInNamedWithOrOnSyntax<T1, T2> To<T2>()
        where T2 : class, T1
        {
            return new FullBindingWhenInNamedWithOrOnSyntax<T1, T2>(_eager, _lazy, _kernel);
        }
    }
}