using TestCommonData.Contract;

namespace TestModuleA
{
    public class TestContractA : ITestContract
    {
        public int GetValue()
            => 110;
    }
}