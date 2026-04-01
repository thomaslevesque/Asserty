namespace Asserty.Tests;

public static class StringEndWithTests
{
    public class WhenValueEndsWithSpecifiedSuffix
    {
        [Fact]
        public void EndWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().EndWith("World!")).Passes();
        }

        [Fact]
        public void Not_EndWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Not.EndWith("World!"))
                .Fails("Expected `actual` not to end with \"World!\", but \"Hello World!\" does.");
        }
    }

    public class WhenValueEndWithSpecifiedSuffixWithSpecifiedComparison
    {
        [Fact]
        public void EndWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().EndWith("world!", StringComparison.OrdinalIgnoreCase)).Passes();
        }

        [Fact]
        public void Not_EndWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Not.EndWith("world!", StringComparison.OrdinalIgnoreCase))
                .Fails("Expected `actual` not to end with \"world!\", but \"Hello World!\" does.");
        }
    }

    public class WhenValueDoesNotEndWithSpecifiedSuffix
    {
        [Fact]
        public void EndWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().EndWith("world!"))
                .Fails("Expected `actual` to end with \"world!\", but \"Hello World!\" doesn't.");
        }

        [Fact]
        public void Not_EndWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Not.EndWith("world!")).Passes();
        }
    }
}
