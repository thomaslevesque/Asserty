# Asserty

Asserty is a simple assertion library for .NET. It offers a fluent API and a number of built-in assertions, and can
easily be extended to add more.

## Basic usage

An example is worth a thousand words, so here's how you would write some simple assertions using Asserty:

```csharp
using Asserty;

...

myObject.Should().Not.BeNull();
myObject.Id.Should().BeEqualTo(42);
myObject.Message.Should().StartWith("Hello").And.HaveLength(12);
```

Note that `Should()` captures the expression that comes before it, so that it can provide better assertion failure
messages. So, for instance, the assertions above would produce messages like these if they were to fail:

```
Expected `myObject` not to be null, but it is actually null.
Expected `myObject.Id` to be equal to 42, but it is actually 0.
Expected `myObject.Message` to start with "Hello", but "Bonjour le monde" doesn't.
```

## API flavors

Asserty's fluent API comes in two flavors: **Should** and **Expect**.

* The **Should** flavor is the one we've seen in the examples above.
* The **Expect** flavor looks more like Jest's `expect()` assertion mechanism:

    ```csharp
    using static Asserty.Expectations;
    
    ...
    
    Expect(myObject).Not.To.BeNull();
    Expect(myObject.Id).To.BeEqualTo(42);
    Expect(myObject.Message).To.StartWith("Hello").And.HaveLength(12);
    ```

Both flavors offer exactly the same functionality, but with slightly different syntax. This is purely a matter of style,
just pick the one that feels most natural to you.

In the rest of the documentation, we'll use the **Should** API for examples, but all assertions are available in both
flavors.

## Extensibility

Extending Asserty to add your own assertions is fairly easy. For example, if you wanted to add an assertion that checks
whether a string is a palindrome, you could write an extension method like this:

```csharp
using Asserty;
using static Asserty.Assertions.AssertionValueFormatter;

public static class StringAssertions
{
    public static IAssertionResult<string?> BeAPalindrome(this IAssertionSubject<string?> subject)
    {
        var assertion = AssertionBuilder.For<string?>()
            .Verify(s => s is not null && s == new string(s.Reverse().ToArray()))
            .ExpectValue("to be a palindrome")
            .DescribeActual(actual => $"{Format(actual)} is not a palindrome")
            .DescribeActualWhenNegated(actual => $"{Format(actual)} is a palindrome");
        return subject.Verify(assertion);
    }
}
```

- `IAssertionSubject<string?>` represents the value being asserted (in this case, a string, possibly null). It's
  returned by `something.Should()` or by `Expect(something).To`.
- `AssertionBuilder.For<string?>()` is the entry point for building a new assertion.
- `Verify()` specifies the condition that must be met for the assertion to pass. In this case, we check that the string
  is not null and that it is equal to its reverse.
- `ExpectValue()` specifies the description of the expectation. It will be used to build the assertion failure  message
  if the assertion fails, in this case ``Expected `something` to be a palindrome, but …``.
- `DescribeActual()` specifies how to describe the actual value when the assertion fails. In this case, the failure
  message will be something like ``Expected `something` to be a palindrome, but "hello" is not a palindrome``.
- `Format()` is a helper method that formats the actual value for display in the assertion failure message. It handles
  null values, adds appropriate quoting for strings, formats collections, etc. You can use it in your own assertions to
  ensure consistent formatting of actual values.
- `DescribeActualWhenNegated()` specifies how to describe the actual value when the assertion is negated (i.e. when
  `Not` is used, as in `something.Should().Not.BeAPalindrome()`). In this case, we simply say that the string is a
   palindrome, but depending on the assertion, you might want to provide better wording for the negated case.
- Finally, we call `subject.Verify(assertion)` to actually perform the assertion.

The method could technically return `void`, but returning an `IAssertionResult<string?>` allows the caller to chain
further assertions. Note that an `IAssertionResult<string?>` will only be returned if the assertion is successful. If it
fails, an `AssertionException` will be thrown and the rest of the chain will not be executed.
