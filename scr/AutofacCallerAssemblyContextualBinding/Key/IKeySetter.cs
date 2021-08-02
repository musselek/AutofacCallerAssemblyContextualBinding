using System;

namespace AutofacCallerAssemblyContextualBinding.Key
{
    public interface IKeySetter
    {
        string ContextualKeyValue(Type from, string assemblyName);
        string CommonKeyValue(Type from);
    }
}
