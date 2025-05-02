namespace Asserty.Tests;

public class EitherTests
{
    [Theory]
    [InlineData("hello")]
    [InlineData(null)]
    public void Either_Should_Pass_When_Any_Assertion_Passes(string? value)
    {
        Expect(() => value.Should().Either(s => s.BeNull(), s => s.HaveLength(5))).ToPass();
    }

    [Fact]
    public void Either_Should_Fail_When_All_Assertions_Fail()
    {
        string value = "foo";
        Expect(() => value.Should().Either(s => s.BeNull(), s => s.HaveLength(5))).ToFail(
            """
            Expected either of multiple assertions to pass, but none does. Assertion failure messages:
            - Expected `value` to be null, but it is actually "foo".
            - Expected `value` to have a length of 5 characters, but its actual length is 3.
            """);
    }

    [Theory]
    [InlineData("hello")]
    [InlineData(null)]
    public void Either_Should_Pass_When_Any_Negative_Assertion_Passes(string? value)
    {
        Expect(() => value.Should().Either(s => s.Not.BeNull(), s => s.Not.HaveLength(5))).ToPass();
    }

    [Fact]
    public void Either_Should_Fail_When_All_Negative_Assertions_Pass()
    {
        string value = "hello";
        Expect(() => value.Should().Either(s => s.Not.StartWith("hell"), s => s.Not.HaveLength(5))).ToFail(
            """
            Expected either of multiple assertions to pass, but none does. Assertion failure messages:
            - Expected `value` not to start with "hell", but "hello" does.
            - Expected `value` not to have a length of 5 characters, but it does.
            """);
    }
}
