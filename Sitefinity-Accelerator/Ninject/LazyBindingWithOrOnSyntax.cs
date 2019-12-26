using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Activation;
using Ninject.Parameters;
using Ninject.Syntax;
using SitefinityAccelerator.Extensions;
using NamedAttribute = Ninject.NamedAttribute;

namespace SitefinityAccelerator.Ninject
{
    public class LazyBindingWithOrOnSyntax<T1, T2>
    where T1 : class
    where T2 : class, T1
    {
        private readonly IBindingWithOrOnSyntax<T2> _eager;
        private readonly IBindingWithOrOnSyntax<Lazy<T1>> _lazy;
        private readonly IKernel _kernel;
        private readonly string _name;
        //private static IKernel Kernel => Constants.NinjectKernel;

        public IBindingWithOrOnSyntax<T2> Eager => _eager;
        public IBindingWithOrOnSyntax<Lazy<T1>> Lazy => _lazy;

        public LazyBindingWithOrOnSyntax
        (
            IBindingToSyntax<T1> eager,
            IBindingToSyntax<Lazy<T1>> lazy,
            IKernel kernel,
            string name,
            bool inSingletonScope
        )
        {
            _kernel = kernel;
            _name = name;
            _eager = eager.To<T2>().Named(name);
            _lazy = inSingletonScope 
                ? lazy.ToMethod(GetInstance).InSingletonScope().Named(name)
                : lazy.ToMethod(GetInstance).Named(name);
        }

        private Lazy<T1> GetInstance(IContext context)
        {
            var constructors = typeof(T2).GetConstructors();

            // only one constructor, so nothing to decide
            if (constructors.Length == 1)
            {
                return new Lazy<T2>(() => _kernel.Get<T2>()) as Lazy<T1>;
            }

            var lazyConstructors = constructors.Where(c => c.GetParameters().Any(p => p.ParameterType.IsLazy())).Select(c => c).ToList();

            // if there is more than one constructor, and none of them contain a constructor with a 
            // lazy argumentm maybe ninject can figure it out, so just pass it along but this will
            // most likely throw a ninject multiple-constructor error, especially considering any
            // other constraints are ignored.
            if (lazyConstructors.Count != 1)
            {
                return new Lazy<T1>(() => _kernel.Get<T1>());
            }

            // there is one constructor here that is lazy, use that one
            var parameters = lazyConstructors[0].GetParameters().ToList();
            var arguments = new List<IParameter>();

            foreach (var parameter in parameters)
            {
                var propertyNamedAttribute = parameter.GetCustomAttributes(typeof(NamedAttribute), false).FirstOrDefault();
                var propertyName = ((NamedAttribute) propertyNamedAttribute)?.Name;
                var argument = propertyName == null ? _kernel.Get(parameter.ParameterType) : _kernel.Get(parameter.ParameterType, propertyName);

                arguments.Add(new ConstructorArgument(parameter.Name, argument));
            }

            return new Lazy<T1>(() => (string.IsNullOrWhiteSpace(_name) ? _kernel.Get<T2>(arguments.ToArray()) : _kernel.Get<T2>(_name, arguments.ToArray())) as T1);


        }
    }
}