using System.Numerics;
using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that the subject's value is equal to the specified value within the specified tolerance.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="otherValue">The value to compare the subject against.</param>
    /// <param name="tolerance">The maximum allowed difference between the subject's value and the other value.</param>
    /// <typeparam name="T">The numeric type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<T> BeEqualToWithin<T>(this IAssertionSubject<T> subject, T otherValue, T tolerance)
        where T : INumber<T>
    {
        var assertion = AssertionBuilder.For<T>()
            .Verify(actualValue => T.Abs(actualValue - otherValue) <= tolerance)
            .ExpectValue($"to be equal to {Format(otherValue)} within a tolerance of {Format(tolerance)}")
            .DescribeActual(_ => "it's not")
            .DescribeActualWhenNegated(_ => "it is");
        return subject.Verify(assertion);
    }
}
