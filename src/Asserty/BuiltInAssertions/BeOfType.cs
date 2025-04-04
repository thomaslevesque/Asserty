using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    public static IAssertionResult<TExpected> BeOfType<TExpected>(this IAssertionSubject<object?> subject)
    {
        var assertion = AssertionBuilder.For<object?>()
            .Verify(actualValue => actualValue?.GetType() == typeof(TExpected))
            .ExpectValue($"to be of type `{typeof(TExpected)}`")
            .DescribeActual(actualValue => actualValue is null
                ? "it is actually null"
                : $"it is actually of type `{actualValue.GetType()}`")
            .DescribeActualWhenNegated(_ => "it is actually of that type");
        return subject.Verify(assertion).Cast<TExpected>();
    }
}
