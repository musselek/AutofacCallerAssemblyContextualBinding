using System;

namespace AutofacCallerAssemblyContextualBinding.Key
{
    public class KeySetter : IKeySetter
    {
        public string ContextualKeyValue(Type type, string assemblyName) => $"{assemblyName}{type.Name}";
        public string CommonKeyValue(Type type) => $"{type.Name}";
    }
}
