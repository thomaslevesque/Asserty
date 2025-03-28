namespace Asserty.Tests;

public class BeEqualToTests
{
    [Fact]
    public void When_Actual_Value_Is_Equal_To_Expected_Value_Then_BeEqualTo_Should_Succeed()
    {
        const string actual = "hello";
        actual.Should().BeEqualTo("hello");
    }

    [Fact]
    public void When_Actual_Value_Is_Equal_To_Expected_Value_Then_BeEqualTo_With_Specific_Comparer_Should_Succeed()
    {
        const string actual = "HeLlO";
        actual.Should().BeEqualTo("hello", StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public void When_Actual_Value_Is_Not_Equal_To_Expected_Value_Then_BeEqualTo_Should_Fail()
    {
        const string actual = "hi";
        var exception = Record.Exception(() => actual.Should().BeEqualTo("hello"));
        Assert.IsType<AssertionException>(exception);
        Assert.Equal("Expected `actual` to be equal to \"hello\", but it is actually \"hi\".", exception.Message);
    }

    [Fact]
    public void When_Actual_Value_Is_Not_Equal_To_Expected_Value_Then_NotBeEqualTo_Should_Succeed()
    {
        const string actual = "hi";
        actual.Should().Not.BeEqualTo("hello");
    }

    [Fact]
    public void When_Actual_Value_Is_Not_Equal_To_Expected_Value_Then_NotBeEqualTo_With_Specific_Comparer_Should_Succeed()
    {
        const string actual = "hi";
        actual.Should().Not.BeEqualTo("hello", StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public void When_Actual_Value_Is_Equal_To_Expected_Value_Then_NotBeEqualTo_Should_Fail()
    {
        const string actual = "hello";
        var exception = Record.Exception(() => actual.Should().Not.BeEqualTo("hello"));
        Assert.IsType<AssertionException>(exception);
        Assert.Equal("Expected `actual` not to be equal to \"hello\", but it is actually equal.", exception.Message);
    }
}
