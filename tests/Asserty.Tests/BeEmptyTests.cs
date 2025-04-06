using static Asserty.Tests.AssertionTestHelpers;

namespace Asserty.Tests;

public static class BeEmptyTests
{
    public class WhenCollectionIsEmpty
    {
        private static readonly int[] Collection = [];

        [Fact]
        public void BeEmpty_Should_Pass()
        {
            Expect(() => Collection.Should().BeEmpty()).ToPass();
        }

        [Fact]
        public void Not_BeEmpty_Should_Fail()
        {
            Expect(() => Collection.Should().Not.BeEmpty()).ToFail("Expected `Collection` not to be empty, but it is.");
        }
    }

    public class WhenCollectionIsNotEmpty
    {
        private static readonly int[] Collection = [1, 2, 3, 4];

        [Fact]
        public void BeEmpty_Should_Fail()
        {
            Expect(() => Collection.Should().BeEmpty()).ToFail("Expected `Collection` to be empty, but [1, 2, 3, …] contains 4 elements.");
        }

        [Fact]
        public void Not_BeEmpty_Should_Pass()
        {
            Expect(() => Collection.Should().Not.BeEmpty()).ToPass();
        }
    }
}
