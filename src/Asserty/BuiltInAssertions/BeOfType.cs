using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that the subject's value is exactly of the specified type, i.e. not a derived type.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <typeparam name="TExpected">The expected type of the subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
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
