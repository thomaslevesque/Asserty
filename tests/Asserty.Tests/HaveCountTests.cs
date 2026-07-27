namespace Asserty.Tests;

public static class HaveCountTests
{
    public class WhenValueHasSpecifiedCount
    {
        private static readonly int[] Collection = [1, 2, 3];

        [Fact]
        public void HaveCount_Should_Pass()
        {
            Verify.That(() => Collection.Should().HaveCount(3)).Passes();
        }

        [Fact]
        public void Not_HaveCount_Should_Fail()
        {
            Verify.That(() => Collection.Should().Not.HaveCount(3))
                .Fails("Expected `Collection` not to contain 3 elements, but it does. Actual value: [1, 2, 3]");
        }
    }

    public class WhenValueHasDifferentCount
    {
        private static readonly int[] Collection = [1, 2, 3, 4];

        [Fact]
        public void HaveCount_Should_Fail()
        {
            Verify.That(() => Collection.Should().HaveCount(3))
                .Fails("Expected `Collection` to contain 3 elements, but it contains 4 elements. Actual value: [1, 2, 3, …]");
        }

        [Fact]
        public void Not_HaveCount_Should_Pass()
        {
            Verify.That(() => Collection.Should().Not.HaveCount(3)).Passes();
        }
    }

    public class WhenValueIsNull
    {
        private static readonly int[]? Collection = null;

        [Fact]
        public void HaveCount_Should_Fail()
        {
            Verify.That(() => Collection.Should().HaveCount(3))
                .Fails("Expected `Collection` to contain 3 elements, but it is null. Actual value: (null)");
        }

        [Fact]
        public void Not_HaveCount_Should_Pass()
        {
            Verify.That(() => Collection.Should().Not.HaveCount(3)).Passes();
        }
    }
}
