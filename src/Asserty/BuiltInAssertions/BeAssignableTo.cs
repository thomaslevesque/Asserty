using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    public static IAssertionResult<TExpected> BeAssignableTo<TExpected>(this IAssertionSubject<object?> subject)
    {
        var assertion = AssertionBuilder.For<object?>()
            .Verify(actualValue => actualValue is TExpected)
            .ExpectValue($"to be assignable to `{typeof(TExpected)}`")
            .DescribeActual(actualValue => actualValue is null
                ? "it is actually null"
                : $"it is actually of type `{actualValue.GetType()}`, which is not assignable to `{typeof(TExpected)}`")
            .DescribeActualWhenNegated(actualValue =>
            {
                var actualType = actualValue?.GetType();
                return actualType == typeof(TExpected)
                    ? "it is actually of that type"
                    : $"it is actually of type `{actualType}`, which is assignable to `{typeof(TExpected)}`";
            });
        return subject.Verify(assertion).Cast<TExpected>();
    }
}
