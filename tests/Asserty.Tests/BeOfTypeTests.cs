namespace Asserty.Tests;

public static class BeOfTypeTests
{
    public class WhenActualValueIsOfTheExpectedType
    {
        [Fact]
        public void BeOfType_Should_Pass_And_ReturnSubjectOfTheExpectedType()
        {
            object value = "hello";
            Verify.That(() => value.Should().BeOfType<string>().And.Contain("ell")).Passes();
        }

        [Fact]
        public void Not_BeOfType_Should_Fail()
        {
            object value = "hello";
            Verify.That(() => value.Should().Not.BeOfType<string>())
                .Fails("Expected `value` not to be of type `System.String`, but it is actually of that type.");
        }
    }

    public class WhenActualValueIsNotOfTheExpectedType
    {
        [Fact]
        public void BeOfType_Should_Fail()
        {
            object value = 42;
            Verify.That(() => value.Should().BeOfType<string>())
                .Fails("Expected `value` to be of type `System.String`, but it is actually of type `System.Int32`.");
        }

        [Fact]
        public void Not_BeOfType_Should_Pass()
        {
            object value = "hello";
            Verify.That(() => value.Should().Not.BeOfType<int>()).Passes();
        }
    }

    public class WhenActualValueIsOfATypeDerivedFromTheExpectedType
    {
        [Fact]
        public void BeOfType_Should_Fail()
        {
            object value = "hello";
            Verify.That(() => value.Should().BeOfType<object>())
                .Fails("Expected `value` to be of type `System.Object`, but it is actually of type `System.String`.");
        }

        [Fact]
        public void Not_BeOfType_Should_Pass()
        {
            object value = "hello";
            Verify.That(() => value.Should().Not.BeOfType<object>()).Passes();
        }
    }
}
