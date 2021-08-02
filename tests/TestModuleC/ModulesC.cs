using AutofacCallerAssemblyContextualBinding;
using TestCommonData;

namespace TestModuleC
{
    public class ModulesC : AbstractModule
    {
        public override void Binder()
        {
            Bind<IConvertor, TestContractCConverter>();
        }
    }
}