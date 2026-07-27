namespace Asserty.Tests;

public static class HaveSameElementsAsTests
{
    public class WhenValueHasSameElementsAsExpectedCollection
    {
        private static readonly int[] Actual = [3, 1, 2, 3];

        [Fact]
        public void HaveSameElementsAs_Should_Pass()
        {
            Verify.That(() => Actual.Should().HaveSameElementsAs([1, 3, 2, 3])).Passes();
        }

        [Fact]
        public void Not_HaveSameElementsAs_Should_Fail()
        {
            Verify.That(() => Actual.Should().Not.HaveSameElementsAs([1, 3, 2, 3]))
                .Fails("Expected `Actual` not to have the same elements as [1, 3, 2, …], but it does. Actual value: [3, 1, 2, …]");
        }
    }

    public class WhenValueHasSameElementsAsExpectedCollectionWithSpecifiedComparer
    {
        private static readonly string?[] Actual = ["hello", null, "world"];

        [Fact]
        public void HaveSameElementsAs_Should_Pass()
        {
            Verify.That(() => Actual.Should().HaveSameElementsAs([null, "WORLD", "HELLO"], StringComparer.OrdinalIgnoreCase)).Passes();
        }

        [Fact]
        public void Not_HaveSameElementsAs_Should_Fail()
        {
            Verify.That(() => Actual.Should().Not.HaveSameElementsAs([null, "WORLD", "HELLO"], StringComparer.OrdinalIgnoreCase))
                .Fails("Expected `Actual` not to have the same elements as [(null), \"WORLD\", \"HELLO\"], but it does. Actual value: [\"hello\", (null), \"world\"]");
        }
    }

    public class WhenValueIsMissingAnExpectedDuplicateElement
    {
        private static readonly int[] Actual = [3, 1, 2];

        [Fact]
        public void HaveSameElementsAs_Should_Fail()
        {
            Verify.That(() => Actual.Should().HaveSameElementsAs([1, 3, 2, 3]))
                .Fails("Expected `Actual` to have the same elements as [1, 3, 2, …], but it doesn't (missing expected element 3). Actual value: [3, 1, 2]");
        }

        [Fact]
        public void Not_HaveSameElementsAs_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.HaveSameElementsAs([1, 3, 2, 3])).Passes();
        }
    }

    public class WhenValueIsMissingANullElement
    {
        private static readonly string?[] Actual = [null, "hello"];

        [Fact]
        public void HaveSameElementsAs_Should_Fail()
        {
            Verify.That(() => Actual.Should().HaveSameElementsAs([null, null, "hello"]))
                .Fails("Expected `Actual` to have the same elements as [(null), (null), \"hello\"], but it doesn't (missing expected element (null)). Actual value: [(null), \"hello\"]");
        }
    }

    public class WhenValueHasUnexpectedExtraElements
    {
        private static readonly int[] Actual = [1, 2, 3, 4];

        [Fact]
        public void HaveSameElementsAs_Should_Fail()
        {
            Verify.That(() => Actual.Should().HaveSameElementsAs([1, 2, 3]))
                .Fails("Expected `Actual` to have the same elements as [1, 2, 3], but it doesn't (contains unexpected element 4). Actual value: [1, 2, 3, …]");
        }
    }

    public class WhenValueHasUnexpectedNullElement
    {
        private static readonly string?[] Actual = [null, "hello", "world"];

        [Fact]
        public void HaveSameElementsAs_Should_Fail()
        {
            Verify.That(() => Actual.Should().HaveSameElementsAs(["hello", "world"]))
                .Fails("Expected `Actual` to have the same elements as [\"hello\", \"world\"], but it doesn't (contains unexpected element (null)). Actual value: [(null), \"hello\", \"world\"]");
        }
    }

    public class WhenValueIsNull
    {
        private static readonly int[]? Actual = null;

        [Fact]
        public void HaveSameElementsAs_Should_Fail()
        {
            Verify.That(() => Actual.Should().HaveSameElementsAs([1, 2, 3]))
                .Fails("Expected `Actual` to have the same elements as [1, 2, 3], but it is null. Actual value: (null)");
        }

        [Fact]
        public void Not_HaveSameElementsAs_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.HaveSameElementsAs([1, 2, 3])).Passes();
        }
    }

    public class WhenExpectedCollectionIsNull
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void HaveSameElementsAs_Should_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => Actual.Should().HaveSameElementsAs(null!));
        }
    }
}
