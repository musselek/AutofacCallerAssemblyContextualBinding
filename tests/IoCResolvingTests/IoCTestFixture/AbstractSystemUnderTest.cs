using AutofacCallerAssemblyContextualBinding.Behavior;
using iCDT2.Tests.Common.IoCTestFixture;

namespace IoCResolvingTests.IoCTestFixture
{
    public abstract class AbstractSystemUnderTest : ISystemUnderTest
    {
        public abstract string[] DLLPlugins { get; }

        public virtual AbstractBinderBehavior BinderBehavior { get; private set; }
    }
}