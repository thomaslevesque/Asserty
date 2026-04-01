namespace Asserty.Tests;

public static class StringHaveLengthTests
{
    public class WhenValueHasSpecifiedLength
    {
        [Fact]
        public void HaveLength_Should_Pass()
        {
            const string actual = "hello";
            Verify.That(() => actual.Should().HaveLength(5)).Passes();
        }

        [Fact]
        public void Not_HaveLength_Should_Fail()
        {
            const string actual = "hello";
            Verify.That(() => actual.Should().Not.HaveLength(5)).Fails("Expected `actual` not to have a length of 5 characters, but it does.");
        }
    }

    public class WhenValueHasDifferentLength
    {
        [Fact]
        public void HaveLength_Should_Fail()
        {
            const string actual = "foo";
            Verify.That(() => actual.Should().HaveLength(5)).Fails("Expected `actual` to have a length of 5 characters, but its actual length is 3.");
        }

        [Fact]
        public void Not_HaveLength_Should_Pass()
        {
            const string actual = "foo";
            Verify.That(() => actual.Should().Not.HaveLength(5)).Passes();
        }
    }

    public class WhenValueIsNull
    {
        [Fact]
        public void HaveLength_Should_Fail()
        {
            const string? actual = null;
            Verify.That(() => actual.Should().HaveLength(5)).Fails("Expected `actual` to have a length of 5 characters, but it is null.");
        }

        [Fact]
        public void Not_HaveLength_Should_Pass()
        {
            const string? actual = null;
            Verify.That(() => actual.Should().Not.HaveLength(5)).Passes();
        }
    }
}
