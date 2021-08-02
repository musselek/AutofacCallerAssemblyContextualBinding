using TestCommonData.Contract;

namespace TestCommonData
{
    public class TestContractCommon : ITestContract
    {
        public int GetValue()
            => 10;
    }
}