using static Asserty.Expectations;

namespace Asserty.Tests.Expectations;

public class ChainedExpectationTests
{
    [Fact]
    public void WhenAllAssertionsAreVerified_Chain_Should_Pass()
    {
        const string value = "hello";
        Verify.That(() => Expect(value).To.Contain("ell").And.BeEqualTo("hello")).Passes();
    }

    [Fact]
    public void WhenAllAssertionsAreVerified_WithNegativeFirstAssertion_Chain_Should_Pass()
    {
        const string value = "hello";
        Verify.That(() => Expect(value).To.Not.Contain("blah").And.BeEqualTo("hello")).Passes();
    }

    [Fact]
    public void WhenAllAssertionsAreVerified_WithNegativeSecondAssertion_Chain_Should_Pass()
    {
        const string value = "hello";
        Verify.That(() => Expect(value).To.Contain("ell").And.Not.BeEqualTo("blah")).Passes();
    }

    [Fact]
    public void WhenAllAssertionsAreVerified_WithAllNegativeAssertions_Chain_Should_Pass()
    {
        const string value = "hello";
        Verify.That(() => Expect(value).To.Not.Contain("blah").And.Not.BeEqualTo("blah")).Passes();
    }

    [Fact]
    public void WhenFirstAssertionIsNotVerified_Chain_Should_Fail()
    {
        const string value = "hello";
        Verify.That(() => Expect(value).To.Contain("blah").And.BeEqualTo("hello"))
            .Fails("Expected `value` to contain \"blah\", but it doesn't. Actual value: \"hello\"");
    }

    [Fact]
    public void WhenNegativeFirstAssertionIsNotVerified_Chain_Should_Fail()
    {
        const string value = "hello";
        Verify.That(() => Expect(value).To.Not.Contain("ell").And.BeEqualTo("hello"))
            .Fails("Expected `value` not to contain \"ell\", but it does. Actual value: \"hello\"");
    }

    [Fact]
    public void WhenSecondAssertionIsNotVerified_Chain_Should_Fail()
    {
        const string value = "hello";
        Verify.That(() => Expect(value).To.Contain("ell").And.BeEqualTo("blah"))
            .Fails("Expected `value` to be equal to \"blah\", but it's not. Actual value: \"hello\"");
    }

    [Fact]
    public void WhenNegativeSecondAssertionIsNotVerified_Chain_Should_Fail()
    {
        const string value = "hello";
        Verify.That(() => Expect(value).To.Contain("ell").And.Not.BeEqualTo("hello"))
            .Fails("Expected `value` not to be equal to \"hello\", but it is. Actual value: \"hello\"");
    }
}
