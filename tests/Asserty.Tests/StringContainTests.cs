namespace Asserty.Tests;

public class StringContainTests
{
    [Fact]
    public void When_Actual_Value_Contains_Specified_Substring_Then_Contain_Should_Succeed()
    {
        const string actual = "Hello World!";
        actual.Should().Contain("World");
    }

    [Fact]
    public void When_Actual_Value_Contains_Specified_Substring_Then_Contain_With_Specific_Comparison_Should_Succeed()
    {
        const string actual = "Hello World!";
        actual.Should().Contain("world", StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void When_Actual_Value_Does_Not_Contain_Specified_Substring_Then_Contain_Should_Fail()
    {
        const string actual = "Hello World!";
        var exception = Record.Exception(() => actual.Should().Contain("world"));
        Assert.IsType<AssertionException>(exception);
        Assert.Equal("Expected `actual` to contain \"world\", but \"Hello World!\" doesn't.", exception.Message);
    }

    [Fact]
    public void When_Actual_Value_Does_Not_Contain_Specified_Substring_Then_NotContain_Should_Succeed()
    {
        const string actual = "Hello World!";
        actual.Should().Not.Contain("world");
    }

    [Fact]
    public void When_Actual_Value_Does_Not_Contain_Specified_Substring_Then_NotContain_With_Specific_Comparison_Should_Succeed()
    {
        const string actual = "Hello World!";
        actual.Should().Not.Contain("Joe", StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void When_Actual_Value_Contains_Specified_Substring_Then_NotContain_Should_Fail()
    {
        const string actual = "Hello World!";
        var exception = Record.Exception(() => actual.Should().Not.Contain("World"));
        Assert.IsType<AssertionException>(exception);
        Assert.Equal("Expected `actual` not to contain \"World\", but \"Hello World!\" does.", exception.Message);
    }
}
