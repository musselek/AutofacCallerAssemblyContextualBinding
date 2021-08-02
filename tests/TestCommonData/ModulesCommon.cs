using AutofacCallerAssemblyContextualBinding;
using TestCommonData.Contract;

namespace TestCommonData
{
    public class ModulesCommon : AbstractModule
    {
        public override void Binder()
        {
            Bind<ITestContract, TestContractCommon>();
        }
    }
}