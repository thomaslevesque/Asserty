using static Asserty.Tests.AssertionTestHelpers;

namespace Asserty.Tests;

public static class BeAssignableToTests
{
    public class WhenActualValueIsOfTheExpectedType
    {
        [Fact]
        public void BeAssignableTo_Should_Pass_And_ReturnSubjectOfTheExpectedType()
        {
            object value = "hello";
            Expect(() => value.Should().BeAssignableTo<string>().And.Contain("ell")).ToPass();
        }

        [Fact]
        public void Not_BeAssignableTo_Should_Fail()
        {
            object value = "hello";
            Expect(() => value.Should().Not.BeAssignableTo<string>())
                .ToFail("Expected `value` not to be assignable to `System.String`, but it is actually of that type.");
        }
    }

    public class WhenActualValueIsNotOfTheExpectedType
    {
        [Fact]
        public void BeAssignableTo_Should_Fail()
        {
            object value = 42;
            Expect(() => value.Should().BeAssignableTo<string>())
                .ToFail("Expected `value` to be assignable to `System.String`, but it is actually of type `System.Int32`, which is not assignable to `System.String`.");
        }

        [Fact]
        public void Not_BeAssignableTo_Should_Pass()
        {
            object value = "hello";
            Expect(() => value.Should().Not.BeAssignableTo<int>()).ToPass();
        }
    }

    public class WhenActualValueIsOfATypeDerivedFromTheExpectedType
    {
        [Fact]
        public void BeAssignableTo_Should_Pass()
        {
            object value = "hello";
            value.Should().BeAssignableTo<object>();
        }

        [Fact]
        public void Not_BeAssignableTo_Should_Fail()
        {
            object value = "hello";
            var exception = Record.Exception(() => value.Should().Not.BeAssignableTo<object>());
            Assert.IsType<AssertionException>(exception);
            Assert.Equal(
                "Expected `value` not to be assignable to `System.Object`, but it is actually of type `System.String`, which is assignable to `System.Object`.",
                exception.Message);
        }
    }
}
