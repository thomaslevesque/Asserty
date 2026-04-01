using static Asserty.Expectations;

namespace Asserty.Tests.Expectations;

public class EitherExpectationTests
{
    [Theory]
    [InlineData("hello")]
    [InlineData(null)]
    public void Either_Should_Pass_When_Any_Assertion_Passes(string? value)
    {
        Verify.That(() => Expect(value).To.Either(s => s.BeNull(), s => s.HaveLength(5))).Passes();
    }

    [Fact]
    public void Either_Should_Fail_When_All_Assertions_Fail()
    {
        string value = "foo";
        Verify.That(() => Expect(value).To.Either(s => s.BeNull(), s => s.HaveLength(5))).Fails(
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
        Verify.That(() => Expect(value).To.Either(s => s.Not.BeNull(), s => s.Not.HaveLength(5))).Passes();
    }

    [Fact]
    public void Either_Should_Fail_When_All_Negative_Assertions_Pass()
    {
        string value = "hello";
        Verify.That(() => Expect(value).To.Either(s => s.Not.StartWith("hell"), s => s.Not.HaveLength(5))).Fails(
            """
            Expected either of multiple assertions to pass, but none does. Assertion failure messages:
            - Expected `value` not to start with "hell", but "hello" does.
            - Expected `value` not to have a length of 5 characters, but it does.
            """);
    }
}
