using Machine.Specifications;

namespace ArbitraryValues.Specs {
    [Subject(Stories.UserAssignsValueFields)]
    public class when_setting_value_fields : ScenarioObjects<int> {
        Establish context = () => { };

        Because action = () => { Foo.AssignArbitraryValues<when_setting_value_fields>(); };

        It should_set_field_values = () => IntValue.ShouldNotEqual(default(int));

        protected static int IntValue;
    }
}