using TestCommonData.Contract;

namespace TestModuleB
{
    public class TestContractB : ITestContract
    {
        public int GetValue()
            => 210;
    }
}