using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that the subject's value is assignable to the specified type, i.e. is of that type or a derived type.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <typeparam name="TExpected">The type the subject's must be assignable to.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
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
