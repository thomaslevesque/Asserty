namespace Asserty.Tests;

public static class StringEndWithTests
{
    public class WhenValueEndsWithSpecifiedSuffix
    {
        [Fact]
        public void EndWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().EndWith("World!"));
        }

        [Fact]
        public void Not_EndWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Not.EndWith("World!"))
                .ToFail("Expected `actual` not to end with \"World!\", but \"Hello World!\" does.");
        }
    }

    public class WhenValueEndWithSpecifiedSuffixWithSpecifiedComparison
    {
        [Fact]
        public void EndWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().EndWith("world!", StringComparison.OrdinalIgnoreCase)).ToPass();
        }

        [Fact]
        public void Not_EndWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Not.EndWith("world!", StringComparison.OrdinalIgnoreCase))
                .ToFail("Expected `actual` not to end with \"world!\", but \"Hello World!\" does.");
        }
    }

    public class WhenValueDoesNotEndWithSpecifiedSuffix
    {
        [Fact]
        public void EndWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().EndWith("world!"))
                .ToFail("Expected `actual` to end with \"world!\", but \"Hello World!\" doesn't.");
        }

        [Fact]
        public void Not_EndWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Not.EndWith("world!")).ToPass();
        }
    }
}
