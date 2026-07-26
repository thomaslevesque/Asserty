namespace Asserty.Tests;

public static class CollectionContainTests
{
    public class WhenCollectionContainsExpectedElement
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void Contain_Should_Pass()
        {
            Verify.That(() => Actual.Should().Contain(2)).Passes();
        }

        [Fact]
        public void Not_Contain_Should_Fail()
        {
            Verify.That(() => Actual.Should().Not.Contain(2))
                .Fails("Expected `Actual` not to contain 2, but [1, 2, 3] does.");
        }
    }

    public class WhenCollectionDoesNotContainExpectedElement
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void Contain_Should_Fail()
        {
            Verify.That(() => Actual.Should().Contain(42))
                .Fails("Expected `Actual` to contain 42, but [1, 2, 3] doesn't.");
        }

        [Fact]
        public void Not_Contain_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.Contain(42)).Passes();
        }
    }

    public class WhenCollectionContainsExpectedElementWithSpecifiedComparer
    {
        private static readonly string[] Actual = ["hello", "world"];

        [Fact]
        public void Contain_Should_Pass()
        {
            Verify.That(() => Actual.Should().Contain("HELLO", StringComparer.OrdinalIgnoreCase)).Passes();
        }

        [Fact]
        public void Not_Contain_Should_Fail()
        {
            Verify.That(() => Actual.Should().Not.Contain("HELLO", StringComparer.OrdinalIgnoreCase))
                .Fails("Expected `Actual` not to contain \"HELLO\", but [\"hello\", \"world\"] does.");
        }
    }

    public class WhenCollectionDoesNotContainExpectedElementWithSpecifiedComparer
    {
        private static readonly string[] Actual = ["hello", "world"];

        [Fact]
        public void Contain_Should_Fail()
        {
            Verify.That(() => Actual.Should().Contain("HELLO"))
                .Fails("Expected `Actual` to contain \"HELLO\", but [\"hello\", \"world\"] doesn't.");
        }

        [Fact]
        public void Not_Contain_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.Contain("HELLO")).Passes();
        }
    }

    public class WhenCollectionContainsExpectedNullItem
    {
        private static readonly string?[] Actual = ["hello", null, "world"];

        [Fact]
        public void Contain_Should_Pass()
        {
            Verify.That(() => Actual.Should().Contain(null)).Passes();
        }

        [Fact]
        public void Not_Contain_Should_Fail()
        {
            Verify.That(() => Actual.Should().Not.Contain(null))
                .Fails("Expected `Actual` not to contain (null), but [\"hello\", (null), \"world\"] does.");
        }
    }

    public class WhenValueIsNull
    {
        private static readonly int[]? Actual = null;

        [Fact]
        public void Contain_Should_Fail()
        {
            Verify.That(() => Actual.Should().Contain(1))
                .Fails("Expected `Actual` to contain 1, but it is null.");
        }

        [Fact]
        public void Not_Contain_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.Contain(1)).Passes();
        }
    }
}
