using AutofacCallerAssemblyContextualBinding.Behavior;

namespace iCDT2.Tests.Common.IoCTestFixture
{
    public interface ISystemUnderTest
    {
        string[] DLLPlugins { get; }
        AbstractBinderBehavior BinderBehavior { get; }
    }
}