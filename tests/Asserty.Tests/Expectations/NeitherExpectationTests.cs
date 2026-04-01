using static Asserty.Expectations;

namespace Asserty.Tests.Expectations;

public class NeitherExpectationTests
{
    [Fact]
    public void Neither_Should_Pass_When_All_Assertions_Fail()
    {
        string value = "hello";
        Verify.That(() => Expect(value).To.Neither(s => s.BeNull(), s => s.StartWith("foo"))).Passes();
    }

    [Fact]
    public void Neither_Should_Fail_When_Any_Assertion_Passes()
    {
        string value = "hello";
        Verify.That(() => Expect(value).To.Neither(s => s.StartWith("he"), s => s.EndWith("llo"))).Fails(
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
        Verify.That(() => Expect(value).To.Neither(s => s.Not.StartWith("he"), s => s.Not.EndWith("llo"))).Passes();
    }

    [Fact]
    public void Neither_Should_Fail_When_Any_Negative_Assertion_Passes()
    {
        string value = "hello";
        Verify.That(() => Expect(value).To.Neither(s => s.Not.BeNull(), s => s.Not.StartWith("foo"))).Fails(
            """
            Expected neither of multiple assertions to pass, but at least one does:
            - Expected `value` to be null, but it is actually "hello".
            - Expected `value` to start with "foo", but "hello" doesn't.
            """);
    }
}
