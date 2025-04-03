namespace Asserty.Tests;

public class BeOfTypeTests
{
    public class WhenActualValueIsOfTheExpectedType
    {
        [Fact]
        public void BeOfType_ShouldSucceedAndReturnSubjectOfTheExpectedType()
        {
            object value = "hello";
            value.Should().BeOfType<string>().And.Contain("ell");
        }

        [Fact]
        public void Not_BeOfType_ShouldThrow()
        {
            object value = "hello";
            var exception = Record.Exception(() => value.Should().Not.BeOfType<string>());
            Assert.IsType<AssertionException>(exception);
            Assert.Equal(
                "Expected `value` not to be of type `System.String`, but it is actually of that type.",
                exception.Message);
        }
    }

    public class WhenActualValueIsNotOfTheExpectedType
    {
        [Fact]
        public void BeOfType_ShouldThrow()
        {
            object value = 42;
            var exception = Record.Exception(() => value.Should().BeOfType<string>());
            Assert.IsType<AssertionException>(exception);
            Assert.Equal("Expected `value` to be of type `System.String`, but it is actually of type `System.Int32`.",
                exception.Message);
        }

        [Fact]
        public void Not_BeOfType_Should_Succeed()
        {
            object value = "hello";
            value.Should().Not.BeOfType<int>();
        }
    }

    public class WhenActualValueIsOfATypeDerivedFromTheExpectedType
    {
        [Fact]
        public void BeOfType_ShouldThrow()
        {
            object value = "hello";
            var exception = Record.Exception(() => value.Should().BeOfType<object>());
            Assert.IsType<AssertionException>(exception);
            Assert.Equal(
                "Expected `value` to be of type `System.Object`, but it is actually of type `System.String`.",
                exception.Message);
        }

        [Fact]
        public void Not_BeOfType_ShouldSucceed()
        {
            object value = "hello";
            value.Should().Not.BeOfType<object>();
        }
    }
}
