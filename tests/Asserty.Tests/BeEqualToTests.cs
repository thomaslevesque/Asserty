namespace Asserty.Tests;

public static class BeEqualToTests
{
    public class WhenValueIsEqualToExpectedValue
    {
        [Fact]
        public void BeEqualTo_Should_Pass()
        {
            const string actual = "hello";
            Verify.That(() => actual.Should().BeEqualTo("hello")).Passes();
        }

        [Fact]
        public void Not_BeEqualTo_Should_Fail()
        {
            const string actual = "hello";
            Verify.That(() => actual.Should().Not.BeEqualTo("hello"))
                .Fails("Expected `actual` not to be equal to \"hello\", but it is actually equal.");
        }
    }

    public class WhenValueIsEqualToExpectedValueWithSpecifiedComparer
    {
        [Fact]
        public void BeEqualTo_Should_Pass()
        {
            const string actual = "HeLlO";
            Verify.That(() => actual.Should().BeEqualTo("hello", StringComparer.OrdinalIgnoreCase)).Passes();
        }

        [Fact]
        public void Not_BeEqualTo_Should_Fail()
        {
            const string actual = "HeLlO";
            Verify.That(() => actual.Should().Not.BeEqualTo("hello", StringComparer.OrdinalIgnoreCase))
                .Fails("Expected `actual` not to be equal to \"hello\", but it is actually equal.");
        }
    }

    public class WhenValueIsNotEqualToExpectedValue
    {
        [Fact]
        public void BeEqualTo_Should_Fail()
        {
            const string actual = "hi";
            Verify.That(() => actual.Should().BeEqualTo("hello"))
                .Fails("Expected `actual` to be equal to \"hello\", but it is actually \"hi\".");
        }

        [Fact]
        public void Not_BeEqualTo_Should_Pass()
        {
            const string actual = "hi";
            Verify.That(() => actual.Should().Not.BeEqualTo("hello")).Passes();
        }
    }
}
