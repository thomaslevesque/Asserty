namespace Asserty.Tests;

public abstract class BeEqualToTests
{
    public class WhenValueIsEqualToExpectedValue
    {
        [Fact]
        public void BeEqualTo_Should_Pass()
        {
            const string actual = "hello";
            Expect(() => actual.Should().BeEqualTo("hello")).ToPass();
        }

        [Fact]
        public void Not_BeEqualTo_Should_Fail()
        {
            const string actual = "hello";
            Expect(() => actual.Should().Not.BeEqualTo("hello"))
                .ToFail("Expected `actual` not to be equal to \"hello\", but it is actually equal.");
        }
    }

    public class WhenValueIsEqualToExpectedValueWithSpecifiedComparer
    {
        [Fact]
        public void BeEqualTo_Should_Pass()
        {
            const string actual = "HeLlO";
            Expect(() => actual.Should().BeEqualTo("hello", StringComparer.OrdinalIgnoreCase)).ToPass();
        }

        [Fact]
        public void Not_BeEqualTo_Should_Fail()
        {
            const string actual = "HeLlO";
            Expect(() => actual.Should().Not.BeEqualTo("hello", StringComparer.OrdinalIgnoreCase))
                .ToFail("Expected `actual` not to be equal to \"hello\", but it is actually equal.");
        }
    }

    public class WhenValueIsNotEqualToExpectedValue
    {
        [Fact]
        public void BeEqualTo_Should_Fail()
        {
            const string actual = "hi";
            Expect(() => actual.Should().BeEqualTo("hello"))
                .ToFail("Expected `actual` to be equal to \"hello\", but it is actually \"hi\".");
        }

        [Fact]
        public void Not_BeEqualTo_Should_Pass()
        {
            const string actual = "hi";
            Expect(() => actual.Should().Not.BeEqualTo("hello")).ToPass();
        }
    }
}
