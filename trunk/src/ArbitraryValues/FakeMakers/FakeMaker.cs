using System;

namespace ArbitraryValues.FakeMakers {
    internal interface IFakeMaker {
        object Make(Type fakeType);
    }

    public static class FakeMaker {
        public static object MakeRhinoMocksFake(Type fakeType) {
            var result = MakeFake("Rhino.Mocks.MockRepository,Rhino.Mocks", fakeType, type => new RhinoMocksFakeMaker(type));
            return result;
        }

        public static object MakeMoqFake(Type fakeType) {
            var result = MakeFake("Moq.Mock`1,Moq", fakeType, type => new MoqFakeMaker(type));
            return result;
        }

        public static object MakeFakeItEasyFake(Type fakeType) {
            var result = MakeFake("FakeItEasy.A,FakeItEasy", fakeType, type => new FakeItEasyFakeMaker(type));
            return result;
        }

        public static object MakeNSubstituteFake(Type fakeType) {
            var result = MakeFake("NSubstitute.Substitute,NSubstitute", fakeType, type => new NSubstituteFakeMaker(type));
            return result;
        }

        static object MakeFake(string fakeMakerTypeName, Type fakeType, Func<Type, IFakeMaker> fakeMakerInstantiator) {
            var @type = GetTypeOrException(fakeMakerTypeName);
            var fakeMaker = fakeMakerInstantiator(@type);
            var result = fakeMaker.Make(fakeType);
            return result;
        }

        static Type GetTypeOrException(string typeName) {
            var result = Type.GetType(typeName);
            if (result == null) {
                var message = string.Format("Unable to get '{0}' type.", typeName);
                throw new TypeLoadException(message);
            }
            return result;
        }

    }
}