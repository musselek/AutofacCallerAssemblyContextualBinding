using TestCommonData;
using TestCommonData.Contract;

namespace TestModuleB
{
    public class TestContractBConverter : BaseTestConverter
    {
        private readonly ITestContract _testContract;

        public TestContractBConverter(ITestContract testContract) : base("Test Contract BConverter")
            => _testContract = testContract;

        public int GetValue()
            => _testContract.GetValue();
    }
}