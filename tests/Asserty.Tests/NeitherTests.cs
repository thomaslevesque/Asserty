namespace Asserty.Tests;

public class NeitherTests
{
    [Fact]
    public void Neither_Should_Pass_When_All_Assertions_Fail()
    {
        string value = "hello";
        Expect(() => value.Should().Neither(s => s.BeNull(), s => s.StartWith("foo"))).ToPass();
    }

    [Fact]
    public void Neither_Should_Fail_When_Any_Assertion_Passes()
    {
        string value = "hello";
        Expect(() => value.Should().Neither(s => s.StartWith("he"), s => s.EndWith("llo"))).ToFail(
            """
            Expected neither of multiple assertions to pass, but at least one does:
            - Expected `value` not to start with "he", but "hello" does.
            - Expected `value` not to end with "llo", but "hello" does.
            """);
    }

    [Fact]
    public void Neither_Should_Pass_When_All_Negative_Assertions_Fail()
    {
        string value = "hello";
        Expect(() => value.Should().Neither(s => s.Not.StartWith("he"), s => s.Not.EndWith("llo"))).ToPass();
    }

    [Fact]
    public void Neither_Should_Fail_When_Any_Negative_Assertion_Passes()
    {
        string value = "hello";
        Expect(() => value.Should().Neither(s => s.Not.BeNull(), s => s.Not.StartWith("foo"))).ToFail(
            """
            Expected neither of multiple assertions to pass, but at least one does:
            - Expected `value` to be null, but it is actually "hello".
            - Expected `value` to start with "foo", but "hello" doesn't.
            """);
    }
}
