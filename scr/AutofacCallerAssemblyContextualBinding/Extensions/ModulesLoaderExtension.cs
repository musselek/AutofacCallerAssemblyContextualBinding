using Autofac;
using Autofac.Core;
using AutofacCallerAssemblyContextualBinding.Behavior;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutofacCallerAssemblyContextualBinding.Extensions
{
    public static class ModulesLoaderExtension
    {
        public static void RegisterModules(this ContainerBuilder builder, Assembly[] assemblies, AbstractBinderBehavior binderBehaviorAutofac)
        {
            foreach (var module in Load(assemblies, binderBehaviorAutofac) ?? Enumerable.Empty<IModule>())
            {
                builder.RegisterModule(module);
            }
        }

        private static IModule[] Load(Assembly[] assemblies, AbstractBinderBehavior binderBehavior)
        {
            var builder = new ContainerBuilder();

            bool isBinderBehaviourBinded = TryBindBinderBehavior(builder, binderBehavior);

            var typeBinded = builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.IsAssignableTo<IModule>());

            if (isBinderBehaviourBinded)
            {
                typeBinded.WithProperty
                (
                     new ResolvedParameter
                     (
                        (pi, ctx) => pi.ParameterType == typeof(AbstractBinderBehavior),
                        (pi, ctx) => binderBehavior
                     )
                 );
            }

            typeBinded.AsImplementedInterfaces();

            using var container = builder.Build();
            return container.Resolve<IEnumerable<IModule>>().ToArray();
        }

        private static bool TryBindBinderBehavior(ContainerBuilder builder, AbstractBinderBehavior binderBehavior)
        {
            if (binderBehavior is null) { return false; }
            builder.RegisterInstance(binderBehavior).As<AbstractBinderBehavior>().AsImplementedInterfaces();

            return true;
        }
    }
}
