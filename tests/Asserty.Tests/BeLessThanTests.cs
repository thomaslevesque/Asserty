namespace Asserty.Tests;

public static class BeLessThanTests
{
    public class WhenValueIsLessThanOtherValue
    {
        [Fact]
        public void BeLessThan_Should_Pass()
        {
            const int value = 3;
            Verify.That(() => value.Should().BeLessThan(5)).Passes();
        }

        [Fact]
        public void Not_BeLessThan_Should_Fail()
        {
            const int value = 3;
            Verify.That(() => value.Should().Not.BeLessThan(5))
                .Fails("Expected `value` not to be less than 5, but it is.");
        }
    }

    public class WhenValueIsEqualToOtherValue
    {
        [Fact]
        public void BeLessThan_Should_Fail()
        {
            const int value = 5;
            Verify.That(() => value.Should().BeLessThan(5))
                .Fails("Expected `value` to be less than 5, but 5 is actually greater than or equal to 5.");
        }

        [Fact]
        public void Not_BeLessThan_Should_Pass()
        {
            const int value = 5;
            Verify.That(() => value.Should().Not.BeLessThan(5)).Passes();
        }
    }

    public class WhenValueIsGreaterThanOtherValue
    {
        [Fact]
        public void BeLessThan_Should_Fail()
        {
            const int value = 10;
            Verify.That(() => value.Should().BeLessThan(5))
                .Fails("Expected `value` to be less than 5, but 10 is actually greater than or equal to 5.");
        }

        [Fact]
        public void Not_BeLessThan_Should_Pass()
        {
            const int value = 10;
            Verify.That(() => value.Should().Not.BeLessThan(5)).Passes();
        }
    }

    public class WhenCustomComparerIsUsed
    {
        [Fact]
        public void BeLessThan_Should_Pass_WithCustomComparer()
        {
            const string value = "a";
            Verify.That(() => value.Should().BeLessThan("b", StringComparer.Ordinal)).Passes();
        }

        [Fact]
        public void BeLessThan_Should_Fail_WithCustomComparer()
        {
            const string value = "b";
            Verify.That(() => value.Should().BeLessThan("a", StringComparer.Ordinal))
                .Fails("Expected `value` to be less than \"a\", but \"b\" is actually greater than or equal to \"a\".");
        }
    }
}
