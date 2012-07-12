using System;
using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;

namespace ArbitraryValues.Specs {
    [Subject(Stories.UserGetsValues)]
    public class when_type_is_enumerable_of_int : ScenarioObjects<IEnumerable<int>> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<IEnumerable<int>>(); };

        It should_return_an_enumerable_of_int = () => Result.ShouldBeOfType<IEnumerable<int>>();
    }

    [Subject(Stories.UserGetsValues)]
    public class when_type_is_collection_of_int : ScenarioObjects<ICollection<int>> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<ICollection<int>>(); };

        It should_return_a_collection_of_ints = () => Result.ShouldBeOfType<ICollection<int>>();
    }

    [Subject(Stories.UserGetsValues)]
    public class when_type_is_ilist_of_int : ScenarioObjects<IList<int>> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<IList<int>>(); };

        It should_return_an_ilist_of_ints = () => Result.ShouldBeOfType<IList<int>>();
    }

    [Subject(Stories.UserGetsValues)]
    public class when_type_is_list_of_int : ScenarioObjects<List<int>> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<List<int>>(); };

        It should_return_a_list_of_ints = () => Result.ShouldBeOfType<List<int>>();
    }

    [Subject(Stories.UserGetsValues)]
    public class when_type_is_an_idictionary_of_int_string : ScenarioObjects<IDictionary<int, string>> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<IDictionary<int, string>>(); };

        It should_return_an_idictionary_of_int_string = () => Result.ShouldBeOfType<IDictionary<int, string>>();
    }

    [Subject(Stories.UserGetsValues)]
    public class when_type_is_enumerable_of_enumerable_of_int : ScenarioObjects<IEnumerable<IEnumerable<int>>> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<IEnumerable<IEnumerable<int>>>(); };

        It should_return_an_enumerable_of_enumerable_of_ints = () => Result.ShouldBeOfType<IEnumerable<IEnumerable<int>>>();
    }
}