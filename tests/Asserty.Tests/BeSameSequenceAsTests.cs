namespace Asserty.Tests;

public static class BeSameSequenceAsTests
{
    public class WhenValueHasSameSequenceAsExpectedSequence
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void BeSameSequenceAs_Should_Pass()
        {
            Verify.That(() => Actual.Should().BeSameSequenceAs([1, 2, 3])).Passes();
        }

        [Fact]
        public void Not_BeSameSequenceAs_Should_Fail()
        {
            Verify.That(() => Actual.Should().Not.BeSameSequenceAs([1, 2, 3]))
                .Fails("Expected `Actual` not to be the same sequence as [1, 2, 3], but it is.");
        }
    }

    public class WhenValueHasSameSequenceAsExpectedSequenceWithSpecifiedComparer
    {
        private static readonly string[] Actual = ["HeLlO", "WoRlD"];

        [Fact]
        public void BeSameSequenceAs_Should_Pass()
        {
            Verify.That(() => Actual.Should().BeSameSequenceAs(["hello", "world"], StringComparer.OrdinalIgnoreCase)).Passes();
        }

        [Fact]
        public void Not_BeSameSequenceAs_Should_Fail()
        {
            Verify.That(() => Actual.Should().Not.BeSameSequenceAs(["hello", "world"], StringComparer.OrdinalIgnoreCase))
                .Fails("Expected `Actual` not to be the same sequence as [\"hello\", \"world\"], but it is.");
        }
    }

    public class WhenValueHasDifferentSequenceThanExpectedSequence
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void BeSameSequenceAs_Should_Fail()
        {
            Verify.That(() => Actual.Should().BeSameSequenceAs([1, 3, 2]))
                .Fails("Expected `Actual` to be the same sequence as [1, 3, 2], but it is actually [1, 2, 3] (first difference at position 1).");
        }

        [Fact]
        public void Not_BeSameSequenceAs_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.BeSameSequenceAs([1, 3, 2])).Passes();
        }

        public class WhenValueAndExpectedSequenceHaveDifferentLengths
        {
            private static readonly int[] Actual = [1, 2];

            [Fact]
            public void BeSameSequenceAs_Should_Fail()
            {
                Verify.That(() => Actual.Should().BeSameSequenceAs([1, 2, 3]))
                    .Fails("Expected `Actual` to be the same sequence as [1, 2, 3], but it is actually [1, 2] (first difference at position 2).");
            }
        }
    }

    public class WhenValueIsNull
    {
        private static readonly int[]? Actual = null;

        [Fact]
        public void BeSameSequenceAs_Should_Fail()
        {
            Verify.That(() => Actual.Should().BeSameSequenceAs([1, 2, 3]))
                .Fails("Expected `Actual` to be the same sequence as [1, 2, 3], but it is actually null.");
        }

        [Fact]
        public void Not_BeSameSequenceAs_Should_Pass()
        {
            Verify.That(() => Actual.Should().Not.BeSameSequenceAs([1, 2, 3])).Passes();
        }
    }

    public class WhenExpectedSequenceIsNull
    {
        private static readonly int[] Actual = [1, 2, 3];

        [Fact]
        public void BeSameSequenceAs_Should_Throw()
        {
            Assert.Throws<ArgumentNullException>(() => Actual.Should().BeSameSequenceAs(null!));
        }
    }
}
