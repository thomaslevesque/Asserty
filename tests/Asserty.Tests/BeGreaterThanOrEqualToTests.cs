namespace Asserty.Tests;

public static class BeGreaterThanOrEqualToTests
{
    public class WhenValueIsGreaterThanOtherValue
    {
        [Fact]
        public void BeGreaterThanOrEqualTo_Should_Pass()
        {
            const int value = 10;
            Verify.That(() => value.Should().BeGreaterThanOrEqualTo(5)).Passes();
        }

        [Fact]
        public void Not_BeGreaterThanOrEqualTo_Should_Fail()
        {
            const int value = 10;
            Verify.That(() => value.Should().Not.BeGreaterThanOrEqualTo(5))
                .Fails("Expected `value` not to be greater than or equal to 5, but it is.");
        }
    }

    public class WhenValueIsEqualToOtherValue
    {
        [Fact]
        public void BeGreaterThanOrEqualTo_Should_Pass()
        {
            const int value = 5;
            Verify.That(() => value.Should().BeGreaterThanOrEqualTo(5)).Passes();
        }

        [Fact]
        public void Not_BeGreaterThanOrEqualTo_Should_Fail()
        {
            const int value = 5;
            Verify.That(() => value.Should().Not.BeGreaterThanOrEqualTo(5))
                .Fails("Expected `value` not to be greater than or equal to 5, but it is.");
        }
    }

    public class WhenValueIsLessThanOtherValue
    {
        [Fact]
        public void BeGreaterThanOrEqualTo_Should_Fail()
        {
            const int value = 3;
            Verify.That(() => value.Should().BeGreaterThanOrEqualTo(5))
                .Fails("Expected `value` to be greater than or equal to 5, but 3 is actually less than 5.");
        }

        [Fact]
        public void Not_BeGreaterThanOrEqualTo_Should_Pass()
        {
            const int value = 3;
            Verify.That(() => value.Should().Not.BeGreaterThanOrEqualTo(5)).Passes();
        }
    }

    public class WhenCustomComparerIsUsed
    {
        [Fact]
        public void BeGreaterThanOrEqualTo_Should_Pass_WithCustomComparer()
        {
            const string value = "b";
            Verify.That(() => value.Should().BeGreaterThanOrEqualTo("a", StringComparer.Ordinal)).Passes();
        }

        [Fact]
        public void BeGreaterThanOrEqualTo_Should_Fail_WithCustomComparer()
        {
            const string value = "a";
            Verify.That(() => value.Should().BeGreaterThanOrEqualTo("b", StringComparer.Ordinal))
                .Fails("Expected `value` to be greater than or equal to \"b\", but \"a\" is actually less than \"b\".");
        }
    }
}
