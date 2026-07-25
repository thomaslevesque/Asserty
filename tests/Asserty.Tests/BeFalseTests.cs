namespace Asserty.Tests;

public static class BeFalseTests
{
    public class WhenValueIsFalse
    {
        [Fact]
        public void BeFalse_Should_Pass()
        {
            const bool value = false;
            Verify.That(() => value.Should().BeFalse()).Passes();
        }

        [Fact]
        public void Not_BeFalse_Should_Fail()
        {
            const bool value = false;
            Verify.That(() => value.Should().Not.BeFalse())
                .Fails("Expected `value` not to be false, but it is.");
        }

    }

    public class WhenValueIsTrue
    {
        [Fact]
        public void BeFalse_Should_Fail()
        {
            const bool value = true;
            Verify.That(() => value.Should().BeFalse())
                .Fails("Expected `value` to be false, but it is true.");
        }

        [Fact]
        public void Not_BeFalse_Should_Pass()
        {
            const bool value = true;
            Verify.That(() => value.Should().Not.BeFalse()).Passes();
        }
    }
}