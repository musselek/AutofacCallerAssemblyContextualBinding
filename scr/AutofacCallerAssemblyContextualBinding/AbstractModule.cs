using Autofac;
using AutofacCallerAssemblyContextualBinding.Behavior;
using AutofacCallerAssemblyContextualBinding.Enums;
using System;

namespace AutofacCallerAssemblyContextualBinding
{
    public abstract class AbstractModule : Module
    {
        private ContainerBuilder _containerBuilder;
        protected AbstractBinderBehavior BinderBehavior { get; init; }
        public abstract void Binder();
        protected sealed override void Load(ContainerBuilder builder)
        {
            _containerBuilder = builder;
            Binder();
        }

        public void Bind<T, K>(Scope scope = Scope.Transient) where K : class, T
            => BinderBehavior.Bind<T, K>(scope, _containerBuilder);
        public void Bind<T>(Scope scope) where T : class
            => BinderBehavior.Bind<T>(scope, _containerBuilder);
        public void BindGenerics(Type from, Type to)
            => BinderBehavior.BindGenerics(from, to, _containerBuilder);
    }
}