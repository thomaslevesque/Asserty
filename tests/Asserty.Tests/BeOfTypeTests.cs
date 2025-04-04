using static Asserty.Tests.AssertionTestHelpers;

namespace Asserty.Tests;

public static class BeOfTypeTests
{
    public class WhenActualValueIsOfTheExpectedType
    {
        [Fact]
        public void BeOfType_Should_Pass_And_ReturnSubjectOfTheExpectedType()
        {
            object value = "hello";
            Expect(() => value.Should().BeOfType<string>().And.Contain("ell")).ToPass();
        }

        [Fact]
        public void Not_BeOfType_Should_Fail()
        {
            object value = "hello";
            Expect(() => value.Should().Not.BeOfType<string>())
                .ToFail("Expected `value` not to be of type `System.String`, but it is actually of that type.");
        }
    }

    public class WhenActualValueIsNotOfTheExpectedType
    {
        [Fact]
        public void BeOfType_Should_Fail()
        {
            object value = 42;
            Expect(() => value.Should().BeOfType<string>())
                .ToFail("Expected `value` to be of type `System.String`, but it is actually of type `System.Int32`.");
        }

        [Fact]
        public void Not_BeOfType_Should_Pass()
        {
            object value = "hello";
            Expect(() => value.Should().Not.BeOfType<int>()).ToPass();
        }
    }

    public class WhenActualValueIsOfATypeDerivedFromTheExpectedType
    {
        [Fact]
        public void BeOfType_Should_Fail()
        {
            object value = "hello";
            Expect(() => value.Should().BeOfType<object>())
                .ToFail("Expected `value` to be of type `System.Object`, but it is actually of type `System.String`.");
        }

        [Fact]
        public void Not_BeOfType_Should_Pass()
        {
            object value = "hello";
            Expect(() => value.Should().Not.BeOfType<object>()).ToPass();
        }
    }
}
