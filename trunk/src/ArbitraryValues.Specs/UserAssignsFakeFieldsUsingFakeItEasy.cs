using System;
using ArbitraryValues.FakeMakers;
using FakeItEasy;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ArbitraryValues.Specs {
    [Subject(Stories.UserAssignsFakeFields)]
    public class when_setting_fake_fields_using_fakeiteasy {
        Establish context = () => {
        };

        Because action = () => Foo.AssignFakes<when_setting_fake_fields_using_fakeiteasy>(FakeMaker.MakeFakeItEasyFake);

        It should_set_field_values = () => DisposableFake.ShouldNotBeNull();

        static IDisposable DisposableFake;
    }
}