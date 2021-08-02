using Autofac;
using AutofacCallerAssemblyContextualBinding.Enums;
using AutofacCallerAssemblyContextualBinding.Extensions;
using AutofacCallerAssemblyContextualBinding.Key;
using System;
using System.Linq;

namespace AutofacCallerAssemblyContextualBinding.Behavior
{
    public abstract class AbstractBinderBehavior
    {
        protected readonly Type _notContextualInterface;
        public IKeySetter KeySetter { get; init; } = new KeySetter();

        public AbstractBinderBehavior(Type notContextualInterface)
            => _notContextualInterface = notContextualInterface;

        internal virtual BindType BinderType(Type from, Type to)
        {
            if (from.IsNotContextualInterface(_notContextualInterface) || to.IsNotContextualInterface(_notContextualInterface))
            { return BindType.Normal; }

            var converterTypes = to.Assembly.GetTypes()
                                   .Where(t => _notContextualInterface.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                                   .Distinct();

            return converterTypes.HasData() && converterTypes.Count() == 1
                ? BindType.Contextual
                : BindType.Common;
        }

        abstract public void Bind<T, K>(Scope scope, ContainerBuilder containerBuilder) where K : class, T;
        abstract public void Bind<T>(Scope scope, ContainerBuilder containerBuilder) where T : class;
        abstract public void BindGenerics(Type from, Type to, ContainerBuilder containerBuilder);
    }
}
