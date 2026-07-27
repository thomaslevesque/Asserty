namespace Asserty.Tests;

public static class HaveAtMostTests
{
    public class WhenValueHasAtMostSpecifiedCount
    {
        private static readonly int[] Collection = [1, 2, 3];

        [Fact]
        public void HaveAtMost_Should_Pass()
        {
            Verify.That(() => Collection.Should().HaveAtMost(3)).Passes();
        }

        [Fact]
        public void Not_HaveAtMost_Should_Fail()
        {
            Verify.That(() => Collection.Should().Not.HaveAtMost(3))
                .Fails("Expected `Collection` not to contain at most 3 elements, but it does. Actual value: [1, 2, 3]");
        }
    }

    public class WhenValueHasMoreThanSpecifiedCount
    {
        private static readonly int[] Collection = [1, 2, 3, 4];

        [Fact]
        public void HaveAtMost_Should_Fail()
        {
            Verify.That(() => Collection.Should().HaveAtMost(3))
                .Fails("Expected `Collection` to contain at most 3 elements, but it contains 4 elements. Actual value: [1, 2, 3, …]");
        }

        [Fact]
        public void Not_HaveAtMost_Should_Pass()
        {
            Verify.That(() => Collection.Should().Not.HaveAtMost(3)).Passes();
        }
    }

    public class WhenValueIsNull
    {
        private static readonly int[]? Collection = null;

        [Fact]
        public void HaveAtMost_Should_Fail()
        {
            Verify.That(() => Collection.Should().HaveAtMost(3))
                .Fails("Expected `Collection` to contain at most 3 elements, but it is null. Actual value: (null)");
        }

        [Fact]
        public void Not_HaveAtMost_Should_Pass()
        {
            Verify.That(() => Collection.Should().Not.HaveAtMost(3)).Passes();
        }
    }
}
