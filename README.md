Foo
===

You can install Foo using [NuGet](http://nuget.org/packages/Foo):

<pre>
  PM> Install-Package Foo
</pre>

# What is it?

Foo creates values for your test class variables so that you can write tests faster.

The values are not random; they're arbitrary. Use Foo when you need a value and don't care what it is. This is particularly useful when you're writing interaction tests, for example using machine.specifications. Because Foo's values are arbitrary, they're not well suited for state-based tests.

## What is Foo and when should I use it?

Foo was created by a group of developers who needed named value variables for use by their interaction test classes. It's most useful if you're using machine.specifications and Rhino Mocks (or Moq), but you may find some of its offerings useful if you're using different testing frameworks. Foo is used by [ScenarioObjects](https://github.com/lancehilliard/ScenarioObjects), a test-authoring accelerator that may also interest you if you're using machine.specifications.

## Getting a single arbitrary value of a certain type...

Sometimes your test needs a value whose name is important and whose value is not. Foo is a good fit for this situation. Store an arbitrary value into this variable easily. The value is determined at runtime, so you'll know your test's passing status isn't dependent on the variable's value.

```c#
  int clientNumberValue = Foo.Get<int>();
  IEnumerable<int> clientNumbersValue = Foo.Get<IEnumerable<int>>();
```

## Getting an arbitrary element from a given enumerable...

If you already have an enumerable of values, and you need an arbitrary element from that enumerable, Foo can help.

```c#
  var color = Foo.GetFrom(new List<string> { "red", "blue", "green" });
```

## Providing arbitrary values for your test classes...

Another method on Foo, AssignArbitraryValues<T>(), is particularly useful if you're writing machine.specification tests, as it will quickly and easily assign arbitrary values to every variable used for storing arbitrary values needed by your tests. If you keep all of those variables in a parent class, they can all be assigned in one call:

```c#
  Foo.AssignArbitraryValues<TestsBase>();
```

## Instantiating fakes for your test classes...

If your tests use Fakes, and they're also defined in that parent class, you can instantiate all of them in one call:

### Rhino Mocks
```c#
  Foo.AssignFakes<TestsBase>(FakeMaker<MockRepository>.Make);
```

### Moq
```c#
  Foo.AssignFakes<TestsBase>(FakeMaker<Mock>.Make);
```
