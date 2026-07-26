using Asserty.Assertions;
using static Asserty.Assertions.AssertionValueFormatter;

namespace Asserty.Tests;

public static class BeSameInstanceAsTests
{
    public class WhenValueIsSameInstance
    {
        [Fact]
        public void BeSameInstanceAs_Should_Pass()
        {
            var value = new object();
            var other = value;
            Verify.That(() => value.Should().BeSameInstanceAs(other)).Passes();
        }

        [Fact]
        public void Not_BeSameInstanceAs_Should_Fail()
        {
            var value = new object();
            var other = value;
            Verify.That(() => value.Should().Not.BeSameInstanceAs(other))
                .Fails($"Expected `value` not to be the same instance as {Format(other)}, but it is.");
        }
    }

    public class WhenValueIsNotSameInstance
    {
        [Fact]
        public void BeSameInstanceAs_Should_Fail()
        {
            var value = new List<int> { 1, 2, 3 };
            var other = new List<int> { 1, 2, 3 };
            Verify.That(() => value.Should().BeSameInstanceAs(other))
                .Fails($"Expected `value` to be the same instance as {Format(other)}, but it is a different instance: {Format(value)}.");
        }

        [Fact]
        public void Not_BeSameInstanceAs_Should_Pass()
        {
            var value = new object();
            var other = new object();
            Verify.That(() => value.Should().Not.BeSameInstanceAs(other)).Passes();
        }
    }

    public class WhenValueIsNull
    {
        [Fact]
        public void BeSameInstanceAs_Null_Should_Pass()
        {
            object? value = null;
            Verify.That(() => value.Should().BeSameInstanceAs(null)).Passes();
        }

        [Fact]
        public void BeSameInstanceAs_NonNull_Should_Fail()
        {
            object? value = null;
            var other = new object();
            Verify.That(() => value.Should().BeSameInstanceAs(other))
                .Fails($"Expected `value` to be the same instance as {Format(other)}, but it is null.");
        }
    }
}
