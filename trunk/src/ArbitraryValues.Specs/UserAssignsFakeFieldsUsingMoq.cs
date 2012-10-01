using System;
using ArbitraryValues.FakeMakers;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace ArbitraryValues.Specs {
    [Subject(Stories.UserAssignsFakeFields)]
    public class when_setting_fake_fields_using_moq {
        Establish context = () => {
        };

        Because action = () => Foo.AssignFakes<when_setting_fake_fields_using_moq>(FakeMaker.MakeMoqFake);

        It should_set_field_values = () => DisposableFake.ShouldBeOfType<Mock<IDisposable>>();

        static Mock<IDisposable> DisposableFake;
    }
}