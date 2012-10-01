using System;
using ArbitraryValues.FakeMakers;
using Machine.Specifications;

namespace ArbitraryValues.Specs {
    [Subject(Stories.UserAssignsFakeFields)]
    public class when_setting_fake_fields_using_nsubstitute {
        Establish context = () => {
        };

        Because action = () => Foo.AssignFakes<when_setting_fake_fields_using_nsubstitute>(FakeMaker.MakeNSubstituteFake);

        It should_set_field_values = () => DisposableFake.ShouldNotBeNull();

        static IDisposable DisposableFake;
    }
}