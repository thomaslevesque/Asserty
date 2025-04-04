using Asserty.Assertions;
using static Asserty.Assertions.AssertionValueFormatter;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
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
