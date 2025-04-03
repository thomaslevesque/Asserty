namespace Asserty.Tests;

public class ShouldExtensionsTests
{
    [Fact]
    public void WhenPassedNoExplicitExpression_Should_ShouldReturnAnAssertionSubject()
    {
        const string value = "hello";
        var subject = value.Should();
        Assert.Equal(nameof(value), subject.Expression);
    }

    [Fact]
    public void WhenPassedAnExplicitExpression_Should_ShouldReturnAnAssertionSubject()
    {
        const string value = "hello";
        const string expression = "test";
        var subject = value.Should(expression);
        Assert.Equal(expression, subject.Expression);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void WhenPassedAnExplicitNullOrEmptyExpression_Should_ShouldThrow(string? expression)
    {
        const string value = "hello";
        var exception = Record.Exception(() => value.Should(expression!));
        var argumentException = Assert.IsType<ArgumentException>(exception);
        Assert.Equal("expression", argumentException.ParamName);
    }
}
