using System;
using Ninject;
using Ninject.Syntax;

namespace SitefinityAccelerator.Ninject
{
    public class FullBindingWhenInNamedWithOrOnSyntax<T1, T2>
    where T1 : class
    where T2 : class, T1
    {
        private readonly IBindingToSyntax<T1> _eager;
        private readonly IBindingToSyntax<Lazy<T1>> _lazy;
        private readonly IKernel _kernel;
        private bool inSingletonScope = false;
       
        //private readonly IBindingWhenInNamedWithOrOnSyntax<T2> _eager;
        //private readonly IBindingWhenInNamedWithOrOnSyntax<Lazy<T1>> _lazy;

        public FullBindingWhenInNamedWithOrOnSyntax
        (
            IBindingToSyntax<T1> eager, 
            IBindingToSyntax<Lazy<T1>> lazy,
            IKernel kernel
        )
        {
            // we don't want to convert them via To or ToMethod just yet lest the resolver
            // fail due to multiple eligible bindings
            //_eager = eager.To<T2>();
            //_lazy = lazy.ToMethod(ctx => new Lazy<T1>(() => Constants.NinjectKernel.Get<T1>()));
            _eager = eager;
            _lazy = lazy;
            _kernel = kernel;
        }


        public FullBindingWhenInNamedWithOrOnSyntax<T1, T2> InSingletonScope()
        {
            inSingletonScope = true;

            return this;
        }

        public LazyBindingWithOrOnSyntax<T1, T2> Named(string name)
        {
            return new LazyBindingWithOrOnSyntax<T1, T2>(_eager, _lazy, _kernel, name, inSingletonScope);
        }
    }
}