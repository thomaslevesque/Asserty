namespace Asserty.Tests;

public static class AllMatchTests
{
    public class WhenAllElementsMatchPredicate
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void AllMatch_Should_Pass()
        {
            Verify.That(() => Actual.Should().AllMatch(x => x > 0)).Passes();
        }

        [Fact]
        public void Not_AllMatch_Should_Fail()
        {
            Verify.That(() => Actual.Should().Not.AllMatch(x => x > 0))
                .Fails("Expected `Actual` not to contain only elements matching `x => x > 0`, but it does. Actual value: [1, 2, 3]");
        }
    }

    public class WhenAnElementDoesNotMatchPredicate
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void AllMatch_Should_Fail()
        {
            Verify.That(() => Actual.Should().AllMatch(x => x < 3))
                .Fails("Expected `Actual` to contain only elements matching `x => x < 3`, but it contains non-matching element 3 at position 2. Actual value: [1, 2, 3]");
        }

        [Fact]
        public void Not_AllMatch_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.AllMatch(x => x < 3)).Passes();
        }
    }

    public class WhenCollectionIsEmpty
    {
        private static readonly int[] Actual = [];

        [Fact]
        public void AllMatch_Should_Pass()
        {
            Verify.That(() => Actual.Should().AllMatch(x => x > 0)).Passes();
        }

        [Fact]
        public void Not_AllMatch_Should_Fail()
        {
            Verify.That(() => Actual.Should().Not.AllMatch(x => x > 0))
                .Fails("Expected `Actual` not to contain only elements matching `x => x > 0`, but it does. Actual value: []");
        }
    }

    public class WhenCollectionIsNull
    {
        private static readonly int[]? Actual = null;

        [Fact]
        public void AllMatch_Should_Fail()
        {
            Verify.That(() => Actual.Should().AllMatch(x => x > 0))
                .Fails("Expected `Actual` to contain only elements matching `x => x > 0`, but it is null. Actual value: (null)");
        }

        [Fact]
        public void Not_AllMatch_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.AllMatch(x => x > 0)).Passes();
        }
    }

    public class WhenPredicateIsNull
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void AllMatch_WithNullPredicate_Should_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => Actual.Should().AllMatch((Func<int, bool>)null!));
        }
    }
}
