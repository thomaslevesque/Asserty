namespace Asserty.Tests;

public class BeAssignableToTests
{
    public class WhenActualValueIsOfTheExpectedType
    {
        [Fact]
        public void BeAssignableTo_ShouldSucceedAndReturnSubjectOfTheExpectedType()
        {
            object value = "hello";
            value.Should().BeAssignableTo<string>().And.Contain("ell");
        }

        [Fact]
        public void Not_BeAssignableTo_ShouldThrow()
        {
            object value = "hello";
            var exception = Record.Exception(() => value.Should().Not.BeAssignableTo<string>());
            Assert.IsType<AssertionException>(exception);
            Assert.Equal(
                "Expected `value` not to be assignable to `System.String`, but it is actually of that type.",
                exception.Message);
        }
    }

    public class WhenActualValueIsNotOfTheExpectedType
    {
        [Fact]
        public void BeAssignableTo_Should_Throw()
        {
            object value = 42;
            var exception = Record.Exception(() => value.Should().BeAssignableTo<string>());
            Assert.IsType<AssertionException>(exception);
            Assert.Equal("Expected `value` to be assignable to `System.String`, but it is actually of type `System.Int32`, which is not assignable to `System.String`.", exception.Message);
        }

        [Fact]
        public void NotBeAssignableTo_Should_Succeed()
        {
            object value = "hello";
            value.Should().Not.BeAssignableTo<int>();
        }
    }

    public class WhenActualValueIsOfATypeDerivedFromTheExpectedType
    {
        [Fact]
        public void BeAssignableTo_Should_Succeed()
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
