
using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace ArbitraryValues.Specs {
    [Subject(Stories.UserGetsValue)]
    public class when_type_is_int : ScenarioObjects<int> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<int>(); };

        It should_return_an_integer = () => Result.ShouldBeOfType<int>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_Uri : ScenarioObjects<Uri> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<Uri>(); };

        It should_return_a_Uri = () => Result.ShouldBeOfType<Uri>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_nullable : ScenarioObjects<int?> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<int?>(); };

        It should_return_the_underlying_type = () => Result.ShouldBeOfType<int?>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_nullable_datetime : ScenarioObjects<DateTime?> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<DateTime?>(); };

        It should_return_the_underlying_type = () => Result.ShouldBeOfType<DateTime?>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_double : ScenarioObjects<double> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<double>(); };

        It should_return_a_double = () => Result.ShouldBeOfType<double>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_float : ScenarioObjects<float> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<float>(); };

        It should_return_a_float = () => Result.ShouldBeOfType<float>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_enum : ScenarioObjects<Environment.SpecialFolder> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<Environment.SpecialFolder>(); };

        It should_return_a_value_from_that_enum = () => Result.ShouldBeOfType<Environment.SpecialFolder>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_string : ScenarioObjects<string> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<string>(); };

        It should_return_a_string = () => Result.ShouldBeOfType<string>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_short : ScenarioObjects<short> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<short>(); };

        It should_return_a_short = () => Result.ShouldBeOfType<short>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_byte : ScenarioObjects<byte> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<byte>(); };

        It should_return_a_byte = () => Result.ShouldBeOfType<byte>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_sbyte : ScenarioObjects<sbyte> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<sbyte>(); };

        It should_return_an_sbyte = () => Result.ShouldBeOfType<sbyte>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_char : ScenarioObjects<char> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<char>(); };

        It should_return_a_char = () => Result.ShouldBeOfType<char>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_decimal : ScenarioObjects<decimal> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<decimal>(); };

        It should_return_a_decimal = () => Result.ShouldBeOfType<decimal>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_uint : ScenarioObjects<uint> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<uint>(); };

        It should_return_a_uint = () => Result.ShouldBeOfType<uint>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_long : ScenarioObjects<long> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<long>(); };

        It should_return_a_long = () => Result.ShouldBeOfType<long>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_ulong : ScenarioObjects<ulong> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<ulong>(); };

        It should_return_a_ulong = () => Result.ShouldBeOfType<ulong>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_ushort : ScenarioObjects<ushort> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<ushort>(); };

        It should_return_a_ushort = () => Result.ShouldBeOfType<ushort>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_bool : ScenarioObjects<bool> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<bool>(); };

        It should_return_a_bool = () => Result.ShouldBeOfType<bool>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_exception : ScenarioObjects<Exception> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<Exception>(); };

        It should_return_an_exception = () => Result.ShouldBeOfType<Exception>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_datetime : ScenarioObjects<DateTime> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<DateTime>(); };

        It should_return_a_datetime = () => Result.ShouldBeOfType<DateTime>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_object : ScenarioObjects<object> {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<object>(); };

        It should_return_a_object = () => Result.ShouldBeOfType<object>();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_eventArg : ScenarioObjects<ExampleEventArg>
    {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<ExampleEventArg>(); };

        It should_return_a_eventArg = () => Result.ShouldBeOfType<ExampleEventArg>();

        It should_return_a_eventArg_with_non_null_a = () => Result.a.ShouldNotBeNull();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_eventArg_with_value_constructor : ScenarioObjects<ExampleValueTypeConstructorEventArg>
    {
        Establish context = () => { };

        Because action = () => { Result = Foo.Get<ExampleValueTypeConstructorEventArg>(); };

        It should_return_a_eventArg = () => Result.ShouldBeOfType<ExampleValueTypeConstructorEventArg>();

        It should_return_a_eventArg_with_non_null_a = () => Result.a.ShouldNotBeNull();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_eventArg_with_non_value_constructor : ScenarioObjects<ExampleNonValueTypeConstructorEventArg> {
        Establish context = () => { };

        Because action = () => Result = Foo.Get<ExampleNonValueTypeConstructorEventArg>();

        It should_return_the_proper_type = () => Result.ShouldNotBeNull();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_eventArg_with_no_public_constructor : ScenarioObjects<Exception> {
        Establish context = () => { };

        Because action = () => Result = Catch.Exception(() => Foo.Get<ExampleNoPublicConstructorEventArgs>());

        It should_return_an_error_telling_it_failed_to_get_a_value = () => Result.Message.ShouldEqual(Messages.FailedToGetValueForType(typeof(ExampleNoPublicConstructorEventArgs)));

        It should_return_an_inner_error_reporting_there_were_no_known_child_types = () => Result.InnerException.Message.ShouldEqual(Messages.NoKnownChildTypes(typeof(ExampleNoPublicConstructorEventArgs)));

        It should_return_an_inner_error_reporting_there_were_no_usable_constructors = () => Result.InnerException.InnerException.Message.ShouldEqual(Messages.NoUsableConstructorFound(typeof(ExampleNoPublicConstructorEventArgs)));
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_trained : ScenarioObjects<ExampleNoCallableConstructorType> {
        Establish context = () => {
            Foo.AddBuilder(random => ExampleNoCallableConstructorType.Instance);
        };

        Because action = () => Result = Foo.Get<ExampleNoCallableConstructorType>();

        It should_return_the_correct_type_of_object = () => Result.ShouldNotBeNull();
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_trained_twice : ScenarioObjects<Exception> {
        Establish context = () => {
            Foo.AddBuilder(random => string.Empty);
        };

        Because action = () => Result = Catch.Exception(() => Foo.AddBuilder(random => string.Empty));

        It should_return_an_error_explaining_that_type_cannot_be_added_twice = () => Result.Message.ShouldEqual(Messages.BuilderAlreadyAddedForType(typeof(string)));
    }

    [Subject(Stories.UserGetsValue)]
    public class when_type_is_interface_that_has_implementation : ScenarioObjects<IConcreteImpl> {
        Establish context = () => { };

        Because action = () => Result = Foo.Get<IConcreteImpl>();

        It should_return_an_instance_of_the_requested_type = () => Result.ShouldNotBeNull();
    }

    public class ConcreteImpl : IConcreteImpl {}

    public interface IConcreteImpl {}

    public class ExampleNoCallableConstructorType {
        ExampleNoCallableConstructorType() {}

        public static readonly ExampleNoCallableConstructorType Instance = new ExampleNoCallableConstructorType();
    }

    public class ExampleNoPublicConstructorEventArgs : EventArgs {
        ExampleNoPublicConstructorEventArgs() {}
    }

    public class ExampleEventArg : EventArgs
    {
        public String a { get; set; }
        public int b { get; set; }
    }

    public class ExampleNonValueTypeConstructorEventArg : EventArgs
    {
        private readonly Exception _exception;

        public ExampleNonValueTypeConstructorEventArg(Exception exception)
        {
            _exception = exception;
        }

        public String a { get; set; }
        public int b { get; set; }
    }

    public class ExampleValueTypeConstructorEventArg : EventArgs
    {
       
        public ExampleValueTypeConstructorEventArg(int b)
        {
            this.b = b;
        }

        public String a { get; set; }
        public int b { get; set; }
    }
}