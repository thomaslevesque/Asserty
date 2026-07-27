namespace Asserty.Tests;

public static class StringContainTests
{
    public class WhenValueContainsSpecifiedSubstring
    {
        [Fact]
        public void Contain_Should_Pass()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Contain("World")).Passes();
        }

        [Fact]
        public void Not_Contain_Should_Fail()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Not.Contain("World"))
                .Fails("Expected `actual` not to contain \"World\", but it does. Actual value: \"Hello World!\"");
        }
    }

    public class WhenValueContainsSpecifiedSubstringWithSpecifiedComparison
    {
        [Fact]
        public void Contain_Should_Pass()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Contain("world", StringComparison.OrdinalIgnoreCase)).Passes();
        }

        [Fact]
        public void Not_Contain_Should_Fail()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Not.Contain("world", StringComparison.OrdinalIgnoreCase))
                .Fails("Expected `actual` not to contain \"world\", but it does. Actual value: \"Hello World!\"");
        }
    }

    public class WhenValueDoesNotContainSpecifiedSubstring
    {
        [Fact]
        public void Contain_Should_Fail()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Contain("world"))
                .Fails("Expected `actual` to contain \"world\", but it doesn't. Actual value: \"Hello World!\"");
        }

        [Fact]
        public void Not_Contain_Should_Pass()
        {
            const string actual = "Hello World!";
            Verify.That(() => actual.Should().Not.Contain("world")).Passes();
        }
    }
}
