using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that the subject's value is true.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<bool> BeTrue(this IAssertionSubject<bool> subject)
    {
        var assertion = AssertionBuilder.For<bool>()
            .Verify(actualValue => actualValue)
            .ExpectValue("to be true")
            .DescribeActual(_ => "it is false")
            .DescribeActualWhenNegated(_ => "it is");
        return subject.Verify(assertion);
    }

    /// <summary>
    /// Asserts that the subject's value is false.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<bool> BeFalse(this IAssertionSubject<bool> subject)
    {
        var assertion = AssertionBuilder.For<bool>()
            .Verify(actualValue => !actualValue)
            .ExpectValue("to be false")
            .DescribeActual(_ => "it is true")
            .DescribeActualWhenNegated(_ => "it is");
        return subject.Verify(assertion);
    }
}
