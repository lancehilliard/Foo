using System;
using ArbitraryValues.FakeMakers;
using Machine.Specifications;
using Rhino.Mocks;

namespace ArbitraryValues.Specs {
    [Subject(Stories.UserAssignsFakeFields)]
    public class when_setting_fake_fields : ScenarioObjects<int> {
        Establish context = () => { };

        Because action = () => { Foo.AssignFakes<when_setting_fake_fields>(FakeMaker.MakeRhinoMocksFake); };

        It should_set_field_values = () => DisposableFake.ShouldNotBeNull();

        protected static IDisposable DisposableFake;
    }

    //[Subject(Stories.UserAssignsFakeFields)]
    //public class when_setting_fake_fields_with_a_non_supported_fake_maker : ScenarioObjects<Exception> {
    //    Establish context = () => { };

    //    Because action = () => {  Result = Catch.Exception(() => Foo.AssignFakes<when_setting_fake_fields>(FakeMaker<String>.Make)); };

    //    It should_throw_not_supported = () => Result.ShouldBeOfType<Exception>();
    //}

}