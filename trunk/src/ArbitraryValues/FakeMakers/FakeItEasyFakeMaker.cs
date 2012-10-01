using System;
using System.Linq;
using System.Reflection;

namespace ArbitraryValues.FakeMakers {
    public class FakeItEasyFakeMaker : IFakeMaker {
        readonly MethodInfo _generateMockMethodInfo;
        readonly object[] _generateMockMethodInfoArguments = new object[] { };

        public FakeItEasyFakeMaker(Type aType) {
            _generateMockMethodInfo = aType.GetMethods(BindingFlags.Public | BindingFlags.Static).First(x => x.Name.Equals("Fake") && !x.GetParameters().Any());
        }

        public object Make(Type fakeType) {
            var generateMockGenericMethodInfo = _generateMockMethodInfo.MakeGenericMethod(fakeType);
            var result = generateMockGenericMethodInfo.Invoke(null, _generateMockMethodInfoArguments);
            return result;
        }
    }
}