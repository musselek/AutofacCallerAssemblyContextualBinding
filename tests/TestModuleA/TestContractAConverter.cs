using TestCommonData;
using TestCommonData.Contract;

namespace TestModuleA
{
    public class TestContractAConverter : BaseTestConverter
    {
        private readonly ITestContract _testContract;

        public TestContractAConverter(ITestContract testContract) : base("Test Contract AConverter")
            => _testContract = testContract;

        public int GetValue()
            => _testContract.GetValue();
    }
}