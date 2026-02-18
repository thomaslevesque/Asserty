namespace Asserty.Tests;

public static class StringStartWithTests
{
    public class WhenValueStartsWithSpecifiedPrefix
    {
        [Fact]
        public void StartWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().StartWith("Hell")).ToPass();
        }

        [Fact]
        public void Not_StartWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Not.StartWith("Hell"))
                .ToFail("Expected `actual` not to start with \"Hell\", but \"Hello World!\" does.");
        }
    }

    public class WhenValueStartWithSpecifiedPrefixWithSpecifiedComparison
    {
        [Fact]
        public void StartWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().StartWith("hello", StringComparison.OrdinalIgnoreCase)).ToPass();
        }

        [Fact]
        public void Not_StartWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Not.StartWith("hello", StringComparison.OrdinalIgnoreCase))
                .ToFail("Expected `actual` not to start with \"hello\", but \"Hello World!\" does.");
        }
    }

    public class WhenValueDoesNotStartWithSpecifiedPrefix
    {
        [Fact]
        public void StartWith_Should_Fail()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().StartWith("hello"))
                .ToFail("Expected `actual` to start with \"hello\", but \"Hello World!\" doesn't.");
        }

        [Fact]
        public void Not_StartWith_Should_Pass()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Not.StartWith("hello")).ToPass();
        }
    }
}
