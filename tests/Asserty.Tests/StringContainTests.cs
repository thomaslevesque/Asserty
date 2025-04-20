namespace Asserty.Tests;

public static class StringContainTests
{
    public class WhenValueContainsSpecifiedSubstring
    {
        [Fact]
        public void Contain_Should_Pass()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Contain("World"));
        }

        [Fact]
        public void Not_Contain_Should_Fail()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Not.Contain("World"))
                .ToFail("Expected `actual` not to contain \"World\", but \"Hello World!\" does.");
        }
    }

    public class WhenValueContainsSpecifiedSubstringWithSpecifiedComparison
    {
        [Fact]
        public void Contain_Should_Pass()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Contain("world", StringComparison.OrdinalIgnoreCase)).ToPass();
        }

        [Fact]
        public void Not_Contain_Should_Fail()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Not.Contain("world", StringComparison.OrdinalIgnoreCase))
                .ToFail("Expected `actual` not to contain \"world\", but \"Hello World!\" does.");
        }
    }

    public class WhenValueDoesNotContainSpecifiedSubstring
    {
        [Fact]
        public void Contain_Should_Fail()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Contain("world"))
                .ToFail("Expected `actual` to contain \"world\", but \"Hello World!\" doesn't.");
        }

        [Fact]
        public void Not_Contain_Should_Pass()
        {
            const string actual = "Hello World!";
            Expect(() => actual.Should().Not.Contain("world")).ToPass();
        }
    }
}
