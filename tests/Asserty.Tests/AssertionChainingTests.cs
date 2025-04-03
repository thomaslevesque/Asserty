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
    public void WhenFirstAssertionIsNotVerified_ChainShouldThrow()
    {
        const string value = "hello";
        var exception = Record.Exception(() => value.Should().Contain("blah").And.BeEqualTo("hello"));
        Assert.IsType<AssertionException>(exception);
        Assert.Equal("Expected `value` to contain \"blah\", but \"hello\" didn't.", exception.Message);
    }

    [Fact]
    public void WhenSecondAssertionIsNotVerified_ChainShouldThrow()
    {
        const string value = "hello";
        var exception = Record.Exception(() => value.Should().Contain("ell").And.BeEqualTo("blah"));
        Assert.IsType<AssertionException>(exception);
        Assert.Equal("Expected `value` to be equal to \"blah\", but it is actually \"hello\".", exception.Message);
    }
}
