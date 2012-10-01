using System;
using System.Linq;
using System.Reflection;

namespace ArbitraryValues.FakeMakers {
    public class NSubstituteFakeMaker : IFakeMaker {
        readonly MethodInfo _generateMockMethodInfo;
        readonly object[] _generateMockMethodInfoArguments = new object[] { null };

        public NSubstituteFakeMaker(Type substituteType) {
            _generateMockMethodInfo = substituteType.GetMethods(BindingFlags.Public | BindingFlags.Static).First(x => x.Name.Equals("For"));
        }

        public object Make(Type fakeType) {
            var generateMockGenericMethodInfo = _generateMockMethodInfo.MakeGenericMethod(fakeType);
            var result = generateMockGenericMethodInfo.Invoke(null, _generateMockMethodInfoArguments);
            return result;
        }
    }
}