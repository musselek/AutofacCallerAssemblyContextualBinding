using System;
using Autofac;
using Autofac.Builder;
using AutofacCallerAssemblyContextualBinding.Enums;

namespace AutofacCallerAssemblyContextualBinding.Behavior
{
    public class StandardBinderBehavior : AbstractBinderBehavior
    {
        public StandardBinderBehavior(Type notContextualInterface) : base(notContextualInterface)
        { }

        public override void Bind<T, K>(Scope scope, ContainerBuilder containerBuilder)
        {
            var binderType = BinderType(typeof(T), typeof(K));

            var registrationBuilder = containerBuilder
           .RegisterType<K>()
           .As<T>()
           .ConfigurePipeline(p => p.Use(new KeyResolverMiddleware(KeySetter)));

            if (binderType != BindType.Normal)
            {
                registrationBuilder.Keyed<T>
                   (
                     binderType == BindType.Contextual
                       ? KeySetter.ContextualKeyValue(typeof(T), typeof(K).Assembly.GetName().Name)
                       : KeySetter.CommonKeyValue(typeof(T))
                   );
            }

            SetScope(scope, registrationBuilder);
        }

        public override void Bind<T>(Scope scope, ContainerBuilder containerBuilder)
        {
            var registrationBuilder = containerBuilder.RegisterType<T>()
            .AsSelf()
            .ConfigurePipeline(p => p.Use(new KeyResolverMiddleware(KeySetter)));

            SetScope(scope, registrationBuilder);
        }

        public override void BindGenerics(Type from, Type to, ContainerBuilder containerBuilder)
        {
            if (!from.IsGenericType || !to.IsGenericType)
            {
                throw new ArgumentException("Argument must be a generic type.");
            }

            containerBuilder
           .RegisterGeneric(to)
            .As(from)
            .ConfigurePipeline(p => p.Use(new KeyResolverMiddleware(KeySetter)))
            .InstancePerDependency();
        }

        private static void SetScope<T>(Scope scope, IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder)
        {
            switch (scope)
            {
                default:
                case Scope.Transient:
                    registrationBuilder.InstancePerDependency();
                    break;

                case Scope.Singleton:
                    registrationBuilder.SingleInstance();
                    break;

                case Scope.CallScope:
                    registrationBuilder.InstancePerLifetimeScope();
                    break;
            }
        }
    }
}
