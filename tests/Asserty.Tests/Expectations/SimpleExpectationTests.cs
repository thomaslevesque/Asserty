using static Asserty.Expectations;

namespace Asserty.Tests.Expectations;

public static class SimpleExpectationTests
{
    public class WhenValueIsEqualToExpectedValue
    {
        [Fact]
        public void BeEqualTo_Should_Pass()
        {
            const string actual = "hello";
            Verify.That(() => Expect(actual).To.BeEqualTo("hello")).Passes();
        }

        [Fact]
        public void Not_BeEqualTo_Should_Fail()
        {
            const string actual = "hello";
            Verify.That(() => Expect(actual).Not.To.BeEqualTo("hello"))
                .Fails("Expected `actual` not to be equal to \"hello\", but it is actually equal.");
        }
    }

    public class WhenValueIsNotEqualToExpectedValue
    {
        [Fact]
        public void BeEqualTo_Should_Fail()
        {
            const string actual = "hi";
            Verify.That(() => Expect(actual).To.BeEqualTo("hello"))
                .Fails("Expected `actual` to be equal to \"hello\", but it is actually \"hi\".");
        }

        [Fact]
        public void Not_BeEqualTo_Should_Pass()
        {
            const string actual = "hi";
            Verify.That(() => Expect(actual).Not.To.BeEqualTo("hello")).Passes();
        }
    }
}
