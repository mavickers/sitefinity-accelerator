using Ninject;
using Ninject.Activation;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFloridaCfoWeb.Application.Extensions
{
    public static class NinjectExtensions
    {
        private static IEnumerable<Func<IRequest, bool>> ComputeMatchConditions<T>
        (
            IBindingWhenSyntax<T> syntax, Type[] types
        )
        {
            foreach (Type type in types)
            {
                syntax.WhenInjectedInto(type);
                yield return syntax.BindingConfiguration.Condition;
            }
        }

        public static IBindingInNamedWithOrOnSyntax<T> WhenInjectedInto<T>
        (
            this IBindingWhenSyntax<T> syntax, 
            params Type[] types
        )
        {
            var conditions = ComputeMatchConditions(syntax, types).ToArray();

            return syntax.When(request => conditions.Any(condition => condition(request)));
        }

        /// <summary>
        /// Class that facilitates tighter lazy binding syntax when registering dependencies.
        /// </summary>
        /// <typeparam name="T1">The interface type to bind.</typeparam>
        /// <typeparam name="T2">The target implementation to bind to the interface.</typeparam>
        public class EagerAndLazyBindingsWhenInNamedWithOrOnSyntax<T1, T2>
        where T1 : class
        where T2 : class, T1
        {
            public IBindingWhenInNamedWithOrOnSyntax<T2> Eager { get; set; }
            public IBindingWhenInNamedWithOrOnSyntax<Lazy<T1>> Lazy { get; set; }

            public EagerAndLazyBindingsWhenInNamedWithOrOnSyntax
            (
                IBindingWhenInNamedWithOrOnSyntax<T2> eager,
                IBindingWhenInNamedWithOrOnSyntax<Lazy<T1>> lazy
            )
            {
                Eager = eager;
                Lazy = lazy;
            }

            public EagerAndLazyBindingsWithOrOnSyntax<T1, T2> Named(string name)
            {
                return new EagerAndLazyBindingsWithOrOnSyntax<T1, T2>(Eager.Named(name), Lazy.Named(name));
            }
        }

        /// <summary>
        /// Class that facilitates tighter name condition syntax when registering
        /// bindings for eager and lazy versions of an interface/class.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public class EagerAndLazyBindingsWithOrOnSyntax<T1, T2>
        where T1 : class
        where T2 : class, T1
        {
            public IBindingWithOrOnSyntax<T2> Eager { get; set; }
            public IBindingWithOrOnSyntax<Lazy<T1>> Lazy { get; set; }

            public EagerAndLazyBindingsWithOrOnSyntax
            (
                IBindingWithOrOnSyntax<T2> eager,
                IBindingWithOrOnSyntax<Lazy<T1>> lazy
            )
            {
                Eager = eager;
                Lazy = lazy;
            }
        }

        /// <summary>
        /// Shortcut to bind a concrete type to an interface, both eagerly and lazily.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="root"></param>
        /// <param name="kernel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Original implementation was accessing kernal from application cache. Kernel is now
        /// injected but this may not be the best way to implement.
        /// </remarks>
        public static EagerAndLazyBindingsWhenInNamedWithOrOnSyntax<T1, T2> FullBindTo<T1, T2>(this BindingRoot root, StandardKernel kernel)
        where T1 : class
        where T2 : class, T1
        {
            var eager = root.Bind<T1>().To<T2>();
            var lazy = root.Bind<Lazy<T1>>().ToMethod(ctx => new Lazy<T1>(() => kernel.Get<T1>()));

            return new EagerAndLazyBindingsWhenInNamedWithOrOnSyntax<T1, T2>(eager, lazy);
        }
    }
}