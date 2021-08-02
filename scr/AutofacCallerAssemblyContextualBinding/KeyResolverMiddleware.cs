using Autofac;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using AutofacCallerAssemblyContextualBinding.Extensions;
using AutofacCallerAssemblyContextualBinding.Helpers;
using AutofacCallerAssemblyContextualBinding.Key;
using System;
using System.Linq;

namespace AutofacCallerAssemblyContextualBinding
{
    internal class KeyResolverMiddleware : IResolveMiddleware
    {
        private readonly IKeySetter _keySetter;

        public KeyResolverMiddleware(IKeySetter keySetter)
            => _keySetter = keySetter;

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            var parameterKeyedByName = new ResolvedParameter
            (
                (pi, ctx) =>
                {
                    var resHelper = new ResolverHelper(context, _keySetter);
                    var resolverKey = resHelper.GetResolverKey(ctx, pi.ParameterType);
                    return resolverKey.HasValue() || ctx.IsRegistered(pi.ParameterType);
                },
                (pi, ctx) =>
                {
                    var resHelper = new ResolverHelper(context, _keySetter);
                    var resolverKey = resHelper.GetResolverKey(ctx, pi.ParameterType);
                    return resolverKey.HasValue()
                            ? ctx.ResolveKeyed(resolverKey, pi.ParameterType)
                            : ctx.Resolve(pi.ParameterType);
                }
            );

            var parameters = context.Parameters.Union(new[] { parameterKeyedByName });

            context.ChangeParameters(parameters);

            next(context);
        }
    }
}