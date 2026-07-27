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
                .Fails("Expected `Actual` not to contain 2, but it does. Actual value: [1, 2, 3]");
        }
    }

    public class WhenCollectionDoesNotContainExpectedElement
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void Contain_Should_Fail()
        {
            Verify.That(() => Actual.Should().Contain(42))
                .Fails("Expected `Actual` to contain 42, but it doesn't. Actual value: [1, 2, 3]");
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
                .Fails("Expected `Actual` not to contain \"HELLO\", but it does. Actual value: [\"hello\", \"world\"]");
        }
    }

    public class WhenCollectionDoesNotContainExpectedElementWithSpecifiedComparer
    {
        private static readonly string[] Actual = ["hello", "world"];

        [Fact]
        public void Contain_Should_Fail()
        {
            Verify.That(() => Actual.Should().Contain("HELLO"))
                .Fails("Expected `Actual` to contain \"HELLO\", but it doesn't. Actual value: [\"hello\", \"world\"]");
        }

        [Fact]
        public void Not_Contain_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.Contain("HELLO")).Passes();
        }
    }

    public class WhenCollectionContainsExpectedNullElement
    {
        private static readonly string?[] Actual = ["hello", null, "world"];

        [Fact]
        public void Contain_Should_Pass()
        {
            Verify.That(() => Actual.Should().Contain((string?)null)).Passes();
        }

        [Fact]
        public void Not_Contain_Should_Fail()
        {
            Verify.That(() => Actual.Should().Not.Contain((string?)null))
                .Fails("Expected `Actual` not to contain (null), but it does. Actual value: [\"hello\", (null), \"world\"]");
        }
    }

    public class WhenValueIsNull
    {
        private static readonly int[]? Actual = null;

        [Fact]
        public void Contain_Should_Fail()
        {
            Verify.That(() => Actual.Should().Contain(1))
                .Fails("Expected `Actual` to contain 1, but it is null. Actual value: (null)");
        }

        [Fact]
        public void Not_Contain_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.Contain(1)).Passes();
        }
    }

    public class WhenCollectionContainsElementMatchingPredicate
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void Contain_WithPredicate_Should_Pass()
        {
            Verify.That(() => Actual.Should().Contain(x => x > 2)).Passes();
        }

        [Fact]
        public void Not_Contain_WithPredicate_Should_Fail()
        {
            Verify.That(() => Actual.Should().Not.Contain(x => x > 2))
                .Fails("Expected `Actual` not to contain an element matching `x => x > 2`, but it does. Actual value: [1, 2, 3]");
        }
    }

    public class WhenCollectionDoesNotContainElementMatchingPredicate
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void Contain_WithPredicate_Should_Fail()
        {
            Verify.That(() => Actual.Should().Contain(x => x > 10))
                .Fails("Expected `Actual` to contain an element matching `x => x > 10`, but it doesn't. Actual value: [1, 2, 3]");
        }

        [Fact]
        public void Not_Contain_WithPredicate_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.Contain(x => x > 10)).Passes();
        }
    }

    public class WhenCollectionIsNullAndPredicateIsUsed
    {
        private static readonly int[]? Actual = null;

        [Fact]
        public void Contain_WithPredicate_Should_Fail()
        {
            Verify.That(() => Actual.Should().Contain(x => x > 0))
                .Fails("Expected `Actual` to contain an element matching `x => x > 0`, but it is null. Actual value: (null)");
        }

        [Fact]
        public void Not_Contain_WithPredicate_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.Contain(x => x > 0)).Passes();
        }
    }

    public class WhenPredicateIsNull
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void Contain_WithNullPredicate_Should_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => Actual.Should().Contain((Func<int, bool>)null!));
        }
    }
}
