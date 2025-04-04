using Asserty.Assertions;
using static Asserty.Assertions.AssertionValueFormatter;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    public static IAssertionResult<string?> Contain(
        this IAssertionSubject<string?> subject,
        string substring,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        var assertion = AssertionBuilder.For<string?>()
            .Verify(actualValue => actualValue?.Contains(substring, comparisonType) ?? false)
            .ExpectValue($"to contain {Format(substring)}")
            .DescribeActual(actualValue => actualValue is null
                ? "it is null"
                : $"{Format(actualValue)} doesn't")
            .DescribeActualWhenNegated(actualValue => $"{Format(actualValue)} does");
        return subject.Verify(assertion);
    }
}
