namespace Asserty.Tests;

public static class BeGreaterThanTests
{
    public class WhenValueIsGreaterThanOtherValue
    {
        [Fact]
        public void BeGreaterThan_Should_Pass()
        {
            const int value = 10;
            Verify.That(() => value.Should().BeGreaterThan(5)).Passes();
        }

        [Fact]
        public void Not_BeGreaterThan_Should_Fail()
        {
            const int value = 10;
            Verify.That(() => value.Should().Not.BeGreaterThan(5))
                .Fails("Expected `value` not to be greater than 5, but it is.");
        }
    }

    public class WhenValueIsEqualToOtherValue
    {
        [Fact]
        public void BeGreaterThan_Should_Fail()
        {
            const int value = 5;
            Verify.That(() => value.Should().BeGreaterThan(5))
                .Fails("Expected `value` to be greater than 5, but 5 is actually less than or equal to 5.");
        }

        [Fact]
        public void Not_BeGreaterThan_Should_Pass()
        {
            const int value = 5;
            Verify.That(() => value.Should().Not.BeGreaterThan(5)).Passes();
        }
    }

    public class WhenValueIsLessThanOtherValue
    {
        [Fact]
        public void BeGreaterThan_Should_Fail()
        {
            const int value = 3;
            Verify.That(() => value.Should().BeGreaterThan(5))
                .Fails("Expected `value` to be greater than 5, but 3 is actually less than or equal to 5.");
        }

        [Fact]
        public void Not_BeGreaterThan_Should_Pass()
        {
            const int value = 3;
            Verify.That(() => value.Should().Not.BeGreaterThan(5)).Passes();
        }
    }

    public class WhenCustomComparerIsUsed
    {
        [Fact]
        public void BeGreaterThan_Should_Pass_WithCustomComparer()
        {
            const string value = "b";
            Verify.That(() => value.Should().BeGreaterThan("a", StringComparer.Ordinal)).Passes();
        }

        [Fact]
        public void BeGreaterThan_Should_Fail_WithCustomComparer()
        {
            const string value = "a";
            Verify.That(() => value.Should().BeGreaterThan("b", StringComparer.Ordinal))
                .Fails("Expected `value` to be greater than \"b\", but \"a\" is actually less than or equal to \"b\".");
        }
    }
}
