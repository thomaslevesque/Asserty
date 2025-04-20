namespace Asserty.Tests;

public class AssertionChainingTests
{
    [Fact]
    public void WhenAllAssertionsAreVerified_Chain_Should_Pass()
    {
        const string value = "hello";
        Expect(() => value.Should().Contain("ell").And.BeEqualTo("hello")).ToPass();
    }

    [Fact]
    public void WhenAllAssertionsAreVerified_WithNegativeFirstAssertion_Chain_Should_Pass()
    {
        const string value = "hello";
        Expect(() => value.Should().Not.Contain("blah").And.BeEqualTo("hello")).ToPass();
    }

    [Fact]
    public void WhenAllAssertionsAreVerified_WithNegativeSecondAssertion_Chain_Should_Pass()
    {
        const string value = "hello";
        Expect(() => value.Should().Contain("ell").And.Not.BeEqualTo("blah")).ToPass();
    }

    [Fact]
    public void WhenAllAssertionsAreVerified_WithAllNegativeAssertions_Chain_Should_Pass()
    {
        const string value = "hello";
        Expect(() => value.Should().Not.Contain("blah").And.Not.BeEqualTo("blah")).ToPass();
    }

    [Fact]
    public void WhenFirstAssertionIsNotVerified_Chain_Should_Fail()
    {
        const string value = "hello";
        Expect(() => value.Should().Contain("blah").And.BeEqualTo("hello"))
            .ToFail("Expected `value` to contain \"blah\", but \"hello\" doesn't.");
    }

    [Fact]
    public void WhenNegativeFirstAssertionIsNotVerified_Chain_Should_Fail()
    {
        const string value = "hello";
        Expect(() => value.Should().Not.Contain("ell").And.BeEqualTo("hello"))
            .ToFail("Expected `value` not to contain \"ell\", but \"hello\" does.");
    }

    [Fact]
    public void WhenSecondAssertionIsNotVerified_Chain_Should_Fail()
    {
        const string value = "hello";
        Expect(() => value.Should().Contain("ell").And.BeEqualTo("blah"))
            .ToFail("Expected `value` to be equal to \"blah\", but it is actually \"hello\".");
    }

    [Fact]
    public void WhenNegativeSecondAssertionIsNotVerified_Chain_Should_Fail()
    {
        const string value = "hello";
        Expect(() => value.Should().Contain("ell").And.Not.BeEqualTo("hello"))
            .ToFail("Expected `value` not to be equal to \"hello\", but it is actually equal.");
    }
}
