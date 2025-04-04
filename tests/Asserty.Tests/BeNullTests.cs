using static Asserty.Tests.AssertionTestHelpers;

namespace Asserty.Tests;

public static class BeNullTests
{
    public class WhenValueIsNull
    {
        [Fact]
        public void BeNull_Should_Pass()
        {
            const string? value = null;
            Expect(() => value.Should().BeNull()).ToPass();
        }

        [Fact]
        public void Not_BeNull_Should_Fail()
        {
            const string? value = null;
            Expect(() => value.Should().Not.BeNull())
                .ToFail("Expected `value` not to be null, but it is actually null.");
        }
    }

    public class WhenValueIsNotNull
    {
        [Fact]
        public void BeNull_Should_Fail()
        {
            const string value = "hello";
            Expect(() => value.Should().BeNull())
                .ToFail("Expected `value` to be null, but it is actually \"hello\".");
        }

        [Fact]
        public void Not_BeNull_Should_Pass()
        {
            const string value = "hello";
            Expect(() => value.Should().Not.BeNull()).ToPass();
        }

        [Fact]
        public void Not_BeNull_Can_Be_Chained_With_Other_Assertion()
        {
            const string value = "hello";
            Expect(() => value.Should().Not.BeNull().And.Contain("ell")).ToPass();
        }
    }
}
