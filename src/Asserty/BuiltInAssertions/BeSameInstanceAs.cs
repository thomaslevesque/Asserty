using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that the subject's value is the same instance as <paramref name="expected"/>.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="expected">The expected instance.</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<T> BeSameInstanceAs<T>(this IAssertionSubject<T> subject, T expected)
        where T : class?
    {
        var assertion = AssertionBuilder.For<T>()
            .Verify(value => ReferenceEquals(value, expected))
            .ExpectValue($"to be the same instance as {Format(expected)}")
            .DescribeActual(_ => "it's not")
            .DescribeActualWhenNegated(_ => "it is");
        return subject.Verify(assertion);
    }
}
