using System.Collections.Generic;
using Machine.Specifications;

namespace ArbitraryValues.Specs {
    [Subject(Stories.UserGetsElement)]
    public class when_type_is_enumerable : ScenarioObjects<string> {
        Establish context = () => { };

        Because action = () => { Result = Foo.GetFrom(new List<string> { string.Empty }); };

        It should_return_an_element = () => Result.ShouldBeOfType<string>();
    }
}