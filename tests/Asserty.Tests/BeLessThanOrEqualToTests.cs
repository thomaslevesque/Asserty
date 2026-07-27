namespace Asserty.Tests;

public static class BeLessThanOrEqualToTests
{
    public class WhenValueIsLessThanOtherValue
    {
        [Fact]
        public void BeLessThanOrEqualTo_Should_Pass()
        {
            const int value = 3;
            Verify.That(() => value.Should().BeLessThanOrEqualTo(5)).Passes();
        }

        [Fact]
        public void Not_BeLessThanOrEqualTo_Should_Fail()
        {
            const int value = 3;
            Verify.That(() => value.Should().Not.BeLessThanOrEqualTo(5))
                .Fails("Expected `value` not to be less than or equal to 5, but it is. Actual value: 3");
        }
    }

    public class WhenValueIsEqualToOtherValue
    {
        [Fact]
        public void BeLessThanOrEqualTo_Should_Pass()
        {
            const int value = 5;
            Verify.That(() => value.Should().BeLessThanOrEqualTo(5)).Passes();
        }

        [Fact]
        public void Not_BeLessThanOrEqualTo_Should_Fail()
        {
            const int value = 5;
            Verify.That(() => value.Should().Not.BeLessThanOrEqualTo(5))
                .Fails("Expected `value` not to be less than or equal to 5, but it is. Actual value: 5");
        }
    }

    public class WhenValueIsGreaterThanOtherValue
    {
        [Fact]
        public void BeLessThanOrEqualTo_Should_Fail()
        {
            const int value = 10;
            Verify.That(() => value.Should().BeLessThanOrEqualTo(5))
                .Fails("Expected `value` to be less than or equal to 5, but it is greater than 5. Actual value: 10");
        }

        [Fact]
        public void Not_BeLessThanOrEqualTo_Should_Pass()
        {
            const int value = 10;
            Verify.That(() => value.Should().Not.BeLessThanOrEqualTo(5)).Passes();
        }
    }

    public class WhenCustomComparerIsUsed
    {
        [Fact]
        public void BeLessThanOrEqualTo_Should_Pass_WithCustomComparer()
        {
            const string value = "a";
            Verify.That(() => value.Should().BeLessThanOrEqualTo("b", StringComparer.Ordinal)).Passes();
        }

        [Fact]
        public void BeLessThanOrEqualTo_Should_Fail_WithCustomComparer()
        {
            const string value = "b";
            Verify.That(() => value.Should().BeLessThanOrEqualTo("a", StringComparer.Ordinal))
                .Fails("Expected `value` to be less than or equal to \"a\", but it is greater than \"a\". Actual value: \"b\"");
        }
    }
}
