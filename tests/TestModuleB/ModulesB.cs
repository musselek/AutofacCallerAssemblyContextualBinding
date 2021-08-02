using AutofacCallerAssemblyContextualBinding;
using TestCommonData;
using TestCommonData.Contract;

namespace TestModuleB
{
    public class ModulesB : AbstractModule
    {
        public override void Binder()
        {
            Bind<ITestContract, TestContractB>();
            Bind<IConvertor, TestContractBConverter>();
        }
    }
}