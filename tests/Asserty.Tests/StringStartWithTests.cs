namespace Asserty.Tests;

public static class StringStartWithTests
{
    public class WhenValueStartsWithSpecifiedPrefix
    {
        [Fact]
        public void StartWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().StartWith("Hell")).Passes();
        }

        [Fact]
        public void Not_StartWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Not.StartWith("Hell"))
                .Fails("Expected `actual` not to start with \"Hell\", but \"Hello World!\" does.");
        }
    }

    public class WhenValueStartWithSpecifiedPrefixWithSpecifiedComparison
    {
        [Fact]
        public void StartWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().StartWith("hello", StringComparison.OrdinalIgnoreCase)).Passes();
        }

        [Fact]
        public void Not_StartWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Not.StartWith("hello", StringComparison.OrdinalIgnoreCase))
                .Fails("Expected `actual` not to start with \"hello\", but \"Hello World!\" does.");
        }
    }

    public class WhenValueDoesNotStartWithSpecifiedPrefix
    {
        [Fact]
        public void StartWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().StartWith("hello"))
                .Fails("Expected `actual` to start with \"hello\", but \"Hello World!\" doesn't.");
        }

        [Fact]
        public void Not_StartWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Not.StartWith("hello")).Passes();
        }
    }
}
