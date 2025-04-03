namespace Asserty.Tests;

public class AssertionChainingTests
{
    [Fact]
    public void WhenAllAssertionsAreVerified_ChainShouldSucceed()
    {
        const string value = "hello";
        value.Should().Contain("ell").And.BeEqualTo("hello");
    }

    [Fact]
    public void WhenAllAssertionsAreVerified_WithNegativeFirstAssertion_ChainShouldSucceed()
    {
        const string value = "hello";
        value.Should().Not.Contain("blah").And.BeEqualTo("hello");
    }

    [Fact]
    public void WhenAllAssertionsAreVerified_WithNegativeSecondAssertion_ChainShouldSucceed()
    {
        const string value = "hello";
        value.Should().Contain("ell").And.Not.BeEqualTo("blah");
    }

    [Fact]
    public void WhenAllAssertionsAreVerified_WithAllNegativeAssertions_ChainShouldSucceed()
    {
        const string value = "hello";
        value.Should().Not.Contain("blah").And.Not.BeEqualTo("blah");
    }

    [Fact]
    public void WhenFirstAssertionIsNotVerified_ChainShouldThrow()
    {
        const string value = "hello";
        var exception = Record.Exception(() => value.Should().Contain("blah").And.BeEqualTo("hello"));
        Assert.IsType<AssertionException>(exception);
        Assert.Equal("Expected `value` to contain \"blah\", but \"hello\" didn't.", exception.Message);
    }

    [Fact]
    public void WhenNegativeFirstAssertionIsNotVerified_ChainShouldThrow()
    {
        const string value = "hello";
        var exception = Record.Exception(() => value.Should().Not.Contain("ell").And.BeEqualTo("hello"));
        Assert.IsType<AssertionException>(exception);
        Assert.Equal("Expected `value` not to contain \"ell\", but \"hello\" did.", exception.Message);
    }

    [Fact]
    public void WhenSecondAssertionIsNotVerified_ChainShouldThrow()
    {
        const string value = "hello";
        var exception = Record.Exception(() => value.Should().Contain("ell").And.BeEqualTo("blah"));
        Assert.IsType<AssertionException>(exception);
        Assert.Equal("Expected `value` to be equal to \"blah\", but it is actually \"hello\".", exception.Message);
    }

    [Fact]
    public void WhenNegativeSecondAssertionIsNotVerified_ChainShouldThrow()
    {
        const string value = "hello";
        var exception = Record.Exception(() => value.Should().Contain("ell").And.Not.BeEqualTo("hello"));
        Assert.IsType<AssertionException>(exception);
        Assert.Equal("Expected `value` not to be equal to \"hello\", but it is actually equal.", exception.Message);
    }
}
