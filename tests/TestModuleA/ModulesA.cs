using AutofacCallerAssemblyContextualBinding;
using TestCommonData;
using TestCommonData.Contract;

namespace TestModuleA
{
    public class ModulesA : AbstractModule
    {
        public override void Binder()
        {
            Bind<ITestContract, TestContractA>();
            Bind<IConvertor, TestContractAConverter>();
        }
    }
}