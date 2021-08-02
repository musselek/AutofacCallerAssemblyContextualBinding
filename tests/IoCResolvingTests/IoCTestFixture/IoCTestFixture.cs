using IoCResolvingTests.IoCTestFixture;

namespace iCDT2.Tests.Common.IoCTestFixture
{
    public sealed class IoCTestFixtureAutofac<T> : AbstractIoCTestFixture
       where T : class, ISystemUnderTest, new()
    {
        private static readonly T _sut = new();

        public IoCTestFixtureAutofac() : base(_sut.DLLPlugins,_sut.BinderBehavior)
        {
        }
    }
}