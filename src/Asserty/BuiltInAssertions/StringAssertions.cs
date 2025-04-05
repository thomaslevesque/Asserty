using Asserty.Assertions;
using static Asserty.Assertions.AssertionValueFormatter;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that the subject's value contains the specified substring.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="substring">The substring that the subject's value must contain.</param>
    /// <param name="comparisonType">The type of string comparison to use.</param>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
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

    /// <summary>
    /// Asserts that the subject's value starts with the specified substring.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="prefix">The prefix that the subject's value must start with.</param>
    /// <param name="comparisonType">The type of string comparison to use.</param>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<string?> StartWith(
        this IAssertionSubject<string?> subject,
        string prefix,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        var assertion = AssertionBuilder.For<string?>()
            .Verify(actualValue => actualValue?.StartsWith(prefix, comparisonType) ?? false)
            .ExpectValue($"to start with {Format(prefix)}")
            .DescribeActual(actualValue => actualValue is null
                ? "it is null"
                : $"{Format(actualValue)} doesn't")
            .DescribeActualWhenNegated(actualValue => $"{Format(actualValue)} does");
        return subject.Verify(assertion);
    }

    /// <summary>
    /// Asserts that the subject's value ends with the specified substring.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="suffix">The suffix that the subject's value must end with.</param>
    /// <param name="comparisonType">The type of string comparison to use.</param>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<string?> EndWith(
        this IAssertionSubject<string?> subject,
        string suffix,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        var assertion = AssertionBuilder.For<string?>()
            .Verify(actualValue => actualValue?.EndsWith(suffix, comparisonType) ?? false)
            .ExpectValue($"to end with {Format(suffix)}")
            .DescribeActual(actualValue => actualValue is null
                ? "it is null"
                : $"{Format(actualValue)} doesn't")
            .DescribeActualWhenNegated(actualValue => $"{Format(actualValue)} does");
        return subject.Verify(assertion);
    }
}
