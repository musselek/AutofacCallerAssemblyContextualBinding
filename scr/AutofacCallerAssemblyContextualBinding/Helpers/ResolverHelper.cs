using Autofac;
using Autofac.Core.Resolving;
using Autofac.Core.Resolving.Pipeline;
using AutofacCallerAssemblyContextualBinding.Extensions;
using AutofacCallerAssemblyContextualBinding.Key;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutofacCallerAssemblyContextualBinding.Helpers
{
    internal class ResolverHelper
    {
        private readonly IEnumerable<string> _stackAssemblyNames;
        private readonly IKeySetter _keySetter;

        public ResolverHelper(ResolveRequestContext context, IKeySetter keySetter)
        {
            _stackAssemblyNames = GetStackAssemblyNames(context);
            _keySetter = keySetter;
        }

        private static IEnumerable<string> GetStackAssemblyNames(ResolveRequestContext context)
        {
            var operation = context.Operation as IDependencyTrackingResolveOperation;
            var requestStack = operation?.RequestStack;
            var assemblyNames = requestStack?.Where(x => x is not null).Select(x => x.Registration?.Activator?.LimitType?.Assembly?.GetName().Name ?? string.Empty).Distinct().ToList();

            return assemblyNames?.Where(x => x.HasValue()).ToList() ?? Enumerable.Empty<string>();
        }

        public string GetResolverKey(IComponentContext context, Type type)
        {
            var resolverKey = _stackAssemblyNames
                .Select(name => _keySetter.ContextualKeyValue(type, name))
                .Append(_keySetter.CommonKeyValue(type))
                .FirstOrDefault(x => context.IsRegisteredWithKey(x, type)) ?? string.Empty;

            return resolverKey;
        }
    }
}