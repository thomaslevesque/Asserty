using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that the subject's value is an empty collection.
    /// </summary>
    /// <param name="subject">The subject of the assertion</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<IEnumerable<T>?> BeEmpty<T>(this IAssertionSubject<IEnumerable<T>?> subject)
    {
        var assertion = AssertionBuilder.For<IEnumerable<T>?>()
            .Verify(actualValue => actualValue is not null && !actualValue.Any())
            .ExpectValue("to be empty")
            .DescribeActual(actualValue =>
            {
                if (actualValue is null)
                    return "it is actually null";
                int count = actualValue.Count();
                return $"{Format(actualValue)} contains {count} {Elements(count)}";
            })
            .DescribeActualWhenNegated(_ => "it is");
        return subject.Verify(assertion);
    }

    /// <summary>
    /// Asserts that the subject's value contains the specified number of elements.
    /// </summary>
    /// <param name="subject">The subject of the assertion</param>
    /// <param name="count">The expected number of elements</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<IEnumerable<T>?> HaveCount<T>(this IAssertionSubject<IEnumerable<T>?> subject, int count)
    {
        var assertion = AssertionBuilder.For<IEnumerable<T>?>()
            .Verify(actualValue => actualValue is not null && actualValue.Count() == count)
            .ExpectValue($"to contain {count} {Elements(count)}")
            .DescribeActual(actualValue =>
            {
                if (actualValue is null)
                    return "it is actually null";
                int actualCount = actualValue.Count();
                return $"{Format(actualValue)} contains {actualCount} {Elements(actualCount)}";
            })
            .DescribeActualWhenNegated(_ => "it does");
        return subject.Verify(assertion);
    }

    private static string Elements(int count) => count > 1 ? "elements" : "element";
}
