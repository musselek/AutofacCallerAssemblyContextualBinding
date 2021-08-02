using System.Collections.Generic;

namespace TestCommonData
{
    public abstract class BaseTestConverter : IConvertor
    {
        public string Identifier { get; }
        public BaseTestConverter(string converterIdentifier)
            => Identifier = converterIdentifier;
    }
}