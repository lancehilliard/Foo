using System;

namespace ArbitraryValues.FakeMakers{
    public static class SubstituteFakeMaker{
        static readonly object[] EmptyConstructorParameters = new object[] {null};
        const string MethodName = "For";

        /*
         * substituteType needs to be typeof(Substitute)
         * I decided this was better than having a dependency on NSubstitute.
         * This can be used like this: Foo.AssignFakes<TargetType>(typeof(Substitute), SubstituteFakeMaker.Make);
         */
        public static object Make(Type fakeType, Type substituteType)
        {
            object result = null;
            foreach (var method in substituteType.GetMethods()){
                if (method.Name == MethodName && method.IsGenericMethod && method.GetGenericArguments().Length == 1)
                {
                    var genericMethod = method.MakeGenericMethod(fakeType);
                    result = genericMethod.Invoke(null, EmptyConstructorParameters);
                }
            }
            return result;
        }
    }
}