namespace Asserty.Tests;

public static class StringHaveLengthTests
{
    public class WhenValueHasSpecifiedLength
    {
        [Fact]
        public void HaveLength_Should_Pass()
        {
            const string actual = "hello";
            Expect(() => actual.Should().HaveLength(5)).ToPass();
        }

        [Fact]
        public void Not_HaveLength_Should_Fail()
        {
            const string actual = "hello";
            Expect(() => actual.Should().Not.HaveLength(5)).ToFail("Expected `actual` not to have a length of 5 characters, but it does.");
        }
    }

    public class WhenValueHasDifferentLength
    {
        [Fact]
        public void HaveLength_Should_Fail()
        {
            const string actual = "foo";
            Expect(() => actual.Should().HaveLength(5)).ToFail("Expected `actual` to have a length of 5 characters, but its actual length is 3.");
        }

        [Fact]
        public void Not_HaveLength_Should_Pass()
        {
            const string actual = "foo";
            Expect(() => actual.Should().Not.HaveLength(5)).ToPass();
        }
    }

    public class WhenValueIsNull
    {
        [Fact]
        public void HaveLength_Should_Fail()
        {
            const string? actual = null;
            Expect(() => actual.Should().HaveLength(5)).ToFail("Expected `actual` to have a length of 5 characters, but it is null.");
        }

        [Fact]
        public void Not_HaveLength_Should_Pass()
        {
            const string? actual = null;
            Expect(() => actual.Should().Not.HaveLength(5)).ToPass();
        }
    }
}
