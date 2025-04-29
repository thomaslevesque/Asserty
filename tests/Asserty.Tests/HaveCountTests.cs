namespace Asserty.Tests;

public static class HaveCountTests
{
    public class WhenValueHasSpecifiedCount
    {
        private static readonly int[] Collection = [1, 2, 3];

        [Fact]
        public void HaveCount_Should_Pass()
        {
            Expect(() => Collection.Should().HaveCount(3)).ToPass();
        }

        [Fact]
        public void Not_HaveCount_Should_Fail()
        {
            Expect(() => Collection.Should().Not.HaveCount(3))
                .ToFail("Expected `Collection` not to contain 3 elements, but it does.");
        }
    }

    public class WhenValueHasDifferentCount
    {
        private static readonly int[] Collection = [1, 2, 3, 4];

        [Fact]
        public void HaveCount_Should_Fail()
        {
            Expect(() => Collection.Should().HaveCount(3))
                .ToFail("Expected `Collection` to contain 3 elements, but [1, 2, 3, …] contains 4 elements.");
        }

        [Fact]
        public void Not_HaveCount_Should_Pass()
        {
            Expect(() => Collection.Should().Not.HaveCount(3)).ToPass();
        }
    }

    public class WhenValueIsNull
    {
        private static readonly int[]? Collection = null;

        [Fact]
        public void HaveCount_Should_Fail()
        {
            Expect(() => Collection.Should().HaveCount(3))
                .ToFail("Expected `Collection` to contain 3 elements, but it is actually null.");
        }

        [Fact]
        public void Not_HaveCount_Should_Pass()
        {
            Expect(() => Collection.Should().Not.HaveCount(3)).ToPass();
        }
    }
}
