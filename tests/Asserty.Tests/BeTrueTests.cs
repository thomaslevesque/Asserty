namespace Asserty.Tests;

public static class BeTrueTests
{
    public class WhenValueIsTrue
    {
        [Fact]
        public void BeTrue_Should_Pass()
        {
            const bool value = true;
            Verify.That(() => value.Should().BeTrue()).Passes();
        }

        [Fact]
        public void Not_BeTrue_Should_Fail()
        {
            const bool value = true;
            Verify.That(() => value.Should().Not.BeTrue())
                .Fails("Expected `value` not to be true, but it is.");
        }

    }

    public class WhenValueIsFalse
    {
        [Fact]
        public void BeTrue_Should_Fail()
        {
            const bool value = false;
            Verify.That(() => value.Should().BeTrue())
                .Fails("Expected `value` to be true, but it is false.");
        }

        [Fact]
        public void Not_BeTrue_Should_Pass()
        {
            const bool value = false;
            Verify.That(() => value.Should().Not.BeTrue()).Passes();
        }
    }
}
