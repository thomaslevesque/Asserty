namespace Asserty.Tests;

public static class BeAssignableToTests
{
    public class WhenActualValueIsOfTheExpectedType
    {
        [Fact]
        public void BeAssignableTo_Should_Pass_And_ReturnSubjectOfTheExpectedType()
        {
            object value = "hello";
            Verify.That(() => value.Should().BeAssignableTo<string>().And.Contain("ell")).Passes();
        }

        [Fact]
        public void Not_BeAssignableTo_Should_Fail()
        {
            object value = "hello";
            Verify.That(() => value.Should().Not.BeAssignableTo<string>())
                .Fails("Expected `value` not to be assignable to `System.String`, but it is actually of that type.");
        }
    }

    public class WhenActualValueIsNotOfTheExpectedType
    {
        [Fact]
        public void BeAssignableTo_Should_Fail()
        {
            object value = 42;
            Verify.That(() => value.Should().BeAssignableTo<string>())
                .Fails("Expected `value` to be assignable to `System.String`, but it is actually of type `System.Int32`, which is not assignable to `System.String`.");
        }

        [Fact]
        public void Not_BeAssignableTo_Should_Pass()
        {
            object value = 42;
            Verify.That(() => value.Should().Not.BeAssignableTo<string>()).Passes();
        }
    }

    public class WhenActualValueIsOfATypeDerivedFromTheExpectedType
    {
        [Fact]
        public void BeAssignableTo_Should_Pass()
        {
            object value = "hello";
            Verify.That(() => value.Should().BeAssignableTo<object>()).Passes();
        }

        [Fact]
        public void Not_BeAssignableTo_Should_Fail()
        {
            object value = "hello";
            Verify.That(() => value.Should().Not.BeAssignableTo<object>())
                .Fails("Expected `value` not to be assignable to `System.Object`, but it is actually of type `System.String`, which is assignable to `System.Object`.");
        }
    }
}
