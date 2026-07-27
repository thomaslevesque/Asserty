namespace Asserty.Tests;

public static class HaveAtLeastTests
{
    public class WhenValueHasAtLeastSpecifiedCount
    {
        private static readonly int[] Collection = [1, 2, 3, 4];

        [Fact]
        public void HaveAtLeast_Should_Pass()
        {
            Verify.That(() => Collection.Should().HaveAtLeast(3)).Passes();
        }

        [Fact]
        public void Not_HaveAtLeast_Should_Fail()
        {
            Verify.That(() => Collection.Should().Not.HaveAtLeast(3))
                .Fails("Expected `Collection` not to contain at least 3 elements, but it does. Actual value: [1, 2, 3, …]");
        }
    }

    public class WhenValueHasLessThanSpecifiedCount
    {
        private static readonly int[] Collection = [1, 2];

        [Fact]
        public void HaveAtLeast_Should_Fail()
        {
            Verify.That(() => Collection.Should().HaveAtLeast(3))
                .Fails("Expected `Collection` to contain at least 3 elements, but it only contains 2 elements. Actual value: [1, 2]");
        }

        [Fact]
        public void Not_HaveAtLeast_Should_Pass()
        {
            Verify.That(() => Collection.Should().Not.HaveAtLeast(3)).Passes();
        }
    }

    public class WhenValueIsNull
    {
        private static readonly int[]? Collection = null;

        [Fact]
        public void HaveAtLeast_Should_Fail()
        {
            Verify.That(() => Collection.Should().HaveAtLeast(3))
                .Fails("Expected `Collection` to contain at least 3 elements, but it is null. Actual value: (null)");
        }

        [Fact]
        public void Not_HaveAtLeast_Should_Pass()
        {
            Verify.That(() => Collection.Should().Not.HaveAtLeast(3)).Passes();
        }
    }
}
