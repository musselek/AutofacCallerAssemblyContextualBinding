using TestCommonData;
using TestCommonData.Contract;

namespace TestModuleC
{
    public class TestContractCConverter : BaseTestConverter
    {
        private readonly ITestContract _testContract;

        public TestContractCConverter(ITestContract testContract) : base("Test Contract CConverter")
            => _testContract = testContract;

        public int GetValue()
            => _testContract.GetValue();
    }
}