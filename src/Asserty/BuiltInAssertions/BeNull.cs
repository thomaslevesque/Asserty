using Asserty.Assertions;
using static Asserty.Assertions.AssertionValueFormatter;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that the subject's value is null.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<T> BeNull<T>(this IAssertionSubject<T> subject)
        where T : class?
    {
        var assertion = AssertionBuilder.For<T>()
            .Verify(value => value is null)
            .ExpectValue("to be null")
            .DescribeActual(actualValue => $"it is actually {Format(actualValue)}")
            .DescribeActualWhenNegated(_ => "it is actually null");
        return subject.Verify(assertion);
    }
}
