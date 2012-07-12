using System;

namespace ArbitraryValues.FakeMakers {
    interface IFakeMaker {
        object Make(Type fakeType);
    }

    public static class FakeMaker<T> {
         public static object Make(Type fakeType) {
            IFakeMaker fakeMaker;
            var fakeMakerTypeName = typeof(T).Name;
            if (fakeMakerTypeName.Equals("MockRepository")) {
                fakeMaker = new MockRepositoryFakeMaker(typeof(T));
            } else {
                throw new Exception("The fake maker type '" + fakeMakerTypeName + "' is not supported by this class.");
            }
            var result = fakeMaker.Make(fakeType);
            return result;
        }
    }
}