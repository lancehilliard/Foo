using System;

namespace ArbitraryValues.FakeMakers {
    internal interface IFakeMaker {
        object Make(Type fakeType);
    }

    public static class FakeMaker<FakeMakerType> {
        public static object Make(Type fakeType) {
            IFakeMaker fakeMaker = null;
            var fakeMakerTypeName = typeof (FakeMakerType).Name;
            if (fakeMakerTypeName.Equals("MockRepository")) {
                fakeMaker = new RhinoMocksFakeMaker(typeof (FakeMakerType));
            }
            else if (fakeMakerTypeName.Equals("Mock")) {
                var moqType = Type.GetType("Moq.Mock`1,Moq");
                if (moqType != null) {
                    fakeMaker = new MoqFakeMaker(moqType);
                }
            }
            if (fakeMaker == null) {
                throw new Exception("The fake maker type '" + fakeMakerTypeName + "' is not yet supported.");
            }
            object result = fakeMaker.Make(fakeType);
            return result;
        }
    }
}