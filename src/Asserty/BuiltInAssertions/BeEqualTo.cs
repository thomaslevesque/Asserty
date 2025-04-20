using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that the subject's value is equal to the specified value.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="expectedValue">The expected value of the subject's value.</param>
    /// <param name="equalityComparer">The equality comparer to use to check for equality. If null, the default comparer
    /// for this type will be used.</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<T> BeEqualTo<T>(this IAssertionSubject<T> subject, T expectedValue, IEqualityComparer<T>? equalityComparer = null)
    {
        var actualComparer = equalityComparer ?? EqualityComparer<T>.Default;
        var assertion = AssertionBuilder.For<T>()
            .Verify(actualValue => actualComparer.Equals(expectedValue, actualValue))
            .ExpectValue($"to be equal to {Format(expectedValue)}")
            .DescribeActual(actualValue => $"it is actually {Format(actualValue)}")
            .DescribeActualWhenNegated(_ => "it is actually equal");
        return subject.Verify(assertion);
    }
}
