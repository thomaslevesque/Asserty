namespace Asserty.Tests;

public static class BeNullTests
{
    public class WhenValueIsNull
    {
        [Fact]
        public void BeNull_Should_Pass()
        {
            const string? value = null;
            Verify.That(() => value.Should().BeNull()).Passes();
        }

        [Fact]
        public void Not_BeNull_Should_Fail()
        {
            const string? value = null;
            Verify.That(() => value.Should().Not.BeNull())
                .Fails("Expected `value` not to be null, but it is actually null.");
        }
    }

    public class WhenValueIsNotNull
    {
        [Fact]
        public void BeNull_Should_Fail()
        {
            const string value = "hello";
            Verify.That(() => value.Should().BeNull())
                .Fails("Expected `value` to be null, but it is actually \"hello\".");
        }

        [Fact]
        public void Not_BeNull_Should_Pass()
        {
            const string value = "hello";
            Verify.That(() => value.Should().Not.BeNull()).Passes();
        }

        [Fact]
        public void Not_BeNull_Can_Be_Chained_With_Other_Assertion()
        {
            const string value = "hello";
            Verify.That(() => value.Should().Not.BeNull().And.Contain("ell")).Passes();
        }
    }
}
