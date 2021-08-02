using AutofacCallerAssemblyContextualBinding.Behavior;
using TestCommonData;

namespace IoCResolvingTests.IoCTestFixture
{
    public class SUTResolvingAutofac : AbstractSystemUnderTest
    {
        public override string[] DLLPlugins => new[] {
            "TestCommonData.dll"
                ,"TestModuleA.dll"
                , "TestModuleB.dll"
                , "TestModuleC.dll"};

        public override AbstractBinderBehavior BinderBehavior => new StandardBinderBehavior(typeof(IConvertor));
    }
}