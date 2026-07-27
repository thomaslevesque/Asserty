namespace Asserty.Tests;

public static class BeEmptyTests
{
    public class WhenCollectionIsEmpty
    {
        private static readonly int[] Collection = [];

        [Fact]
        public void BeEmpty_Should_Pass()
        {
            Verify.That(() => Collection.Should().BeEmpty()).Passes();
        }

        [Fact]
        public void Not_BeEmpty_Should_Fail()
        {
            Verify.That(() => Collection.Should().Not.BeEmpty()).Fails("Expected `Collection` not to be empty, but it is. Actual value: []");
        }
    }

    public class WhenCollectionIsNotEmpty
    {
        private static readonly int[] Collection = [1, 2, 3, 4];

        [Fact]
        public void BeEmpty_Should_Fail()
        {
            Verify.That(() => Collection.Should().BeEmpty()).Fails("Expected `Collection` to be empty, but it contains 4 elements. Actual value: [1, 2, 3, …]");
        }

        [Fact]
        public void Not_BeEmpty_Should_Pass()
        {
            Verify.That(() => Collection.Should().Not.BeEmpty()).Passes();
        }
    }

    public class WhenCollectionIsNull
    {
        private static readonly int[]? Collection = null;

        [Fact]
        public void BeEmpty_Should_Fail()
        {
            Verify.That(() => Collection.Should().BeEmpty())
                .Fails("Expected `Collection` to be empty, but it is null. Actual value: (null)");
        }

        [Fact]
        public void Not_BeEmpty_Should_Pass()
        {
            Verify.That(() => Collection.Should().Not.BeEmpty()).Passes();
        }
    }
}
