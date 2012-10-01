using System;
using System.Linq;
using System.Reflection;

namespace ArbitraryValues.FakeMakers {
    class RhinoMocksFakeMaker : IFakeMaker {
        readonly MethodInfo _generateMockMethodInfo;
        readonly object[] _generateMockMethodInfoArguments = new object[] { null };

        public RhinoMocksFakeMaker(Type mockRepositoryType) {
            _generateMockMethodInfo = mockRepositoryType.GetMethods(BindingFlags.Public | BindingFlags.Static).First(x => x.Name.Equals("GenerateMock"));
        }

        public object Make(Type fakeType) {
            var generateMockGenericMethodInfo = _generateMockMethodInfo.MakeGenericMethod(fakeType);
            var result = generateMockGenericMethodInfo.Invoke(null, _generateMockMethodInfoArguments);
            return result;
        }
    }
}