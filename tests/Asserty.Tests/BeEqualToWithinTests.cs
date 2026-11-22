namespace Asserty.Tests;

public static class BeEqualToWithinTests
{
    public class WhenValueIsWithinTolerance
    {
        [Fact]
        public void BeEqualToWithin_Should_Pass()
        {
            const double value = 10.01;
            Verify.That(() => value.Should().BeEqualToWithin(10.0, 0.02)).Passes();
        }

        [Fact]
        public void Not_BeEqualToWithin_Should_Fail()
        {
            const double value = 10.01;
            Verify.That(() => value.Should().Not.BeEqualToWithin(10.0, 0.02))
                .Fails("Expected `value` not to be equal to 10 within a tolerance of 0.02, but it is. Actual value: 10.01");
        }

        [Fact]
        public void BeEqualToWithin_Should_Pass_WhenDifferenceEqualsTolerance()
        {
            const decimal value = 10.1m;
            Verify.That(() => value.Should().BeEqualToWithin(10.0m, 0.1m)).Passes();
        }
    }

    public class WhenValueIsOutsideTolerance
    {
        [Fact]
        public void BeEqualToWithin_Should_Fail()
        {
            const double value = 10.1;
            Verify.That(() => value.Should().BeEqualToWithin(10.0, 0.05))
                .Fails("Expected `value` to be equal to 10 within a tolerance of 0.05, but it's not. Actual value: 10.1");
        }

        [Fact]
        public void Not_BeEqualToWithin_Should_Pass()
        {
            const double value = 10.1;
            Verify.That(() => value.Should().Not.BeEqualToWithin(10.0, 0.05)).Passes();
        }
    }

    public class WhenValueIsAnInteger
    {
        [Fact]
        public void BeEqualToWithin_Should_Pass()
        {
            const int value = 9;
            Verify.That(() => value.Should().BeEqualToWithin(10, 1)).Passes();
        }
    }
}
