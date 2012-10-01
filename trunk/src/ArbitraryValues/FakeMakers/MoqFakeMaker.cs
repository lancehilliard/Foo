using System;
using System.Linq;

namespace ArbitraryValues.FakeMakers {
    public class MoqFakeMaker : IFakeMaker {
        readonly Type _moqType;

        public MoqFakeMaker(Type moqType) {
            _moqType = moqType;
        }

        public object Make(Type fakeType) {
            var fakeTypeFirstTypeParameter = fakeType.GetGenericArguments().First();
            var genericMockType = _moqType.MakeGenericType(fakeTypeFirstTypeParameter);
            var constructor = genericMockType.GetConstructor(new Type[] { });
            var result = constructor.Invoke(null);
            return result;
        }
    }
}
