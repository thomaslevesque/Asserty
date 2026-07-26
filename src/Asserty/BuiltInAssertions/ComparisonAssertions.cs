using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that the subject's value is greater than the specified value.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="otherValue">The value to compare the subject against.</param>
    /// <param name="comparer">The comparer to use. If null, the default comparer for this type will be used.</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<T> BeGreaterThan<T>(this IAssertionSubject<T> subject, T otherValue, IComparer<T>? comparer = null)
        where T : IComparable<T>
    {
        var actualComparer = comparer ?? Comparer<T>.Default;
        var assertion = AssertionBuilder.For<T>()
            .Verify(actualValue => actualComparer.Compare(actualValue, otherValue) > 0)
            .ExpectValue($"to be greater than {Format(otherValue)}")
            .DescribeActual(actualValue => $"{Format(actualValue)} is actually less than or equal to {Format(otherValue)}")
            .DescribeActualWhenNegated(_ => "it is");
        return subject.Verify(assertion);
    }

    /// <summary>
    /// Asserts that the subject's value is greater than or equal to the specified value.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="otherValue">The value to compare the subject against.</param>
    /// <param name="comparer">The comparer to use. If null, the default comparer for this type will be used.</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<T> BeGreaterThanOrEqualTo<T>(this IAssertionSubject<T> subject, T otherValue, IComparer<T>? comparer = null)
        where T : IComparable<T>
    {
        var actualComparer = comparer ?? Comparer<T>.Default;
        var assertion = AssertionBuilder.For<T>()
            .Verify(actualValue => actualComparer.Compare(actualValue, otherValue) >= 0)
            .ExpectValue($"to be greater than or equal to {Format(otherValue)}")
            .DescribeActual(actualValue => $"{Format(actualValue)} is actually less than {Format(otherValue)}")
            .DescribeActualWhenNegated(_ => "it is");
        return subject.Verify(assertion);
    }

    /// <summary>
    /// Asserts that the subject's value is less than the specified value.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="otherValue">The value to compare the subject against.</param>
    /// <param name="comparer">The comparer to use. If null, the default comparer for this type will be used.</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<T> BeLessThan<T>(this IAssertionSubject<T> subject, T otherValue, IComparer<T>? comparer = null)
        where T : IComparable<T>
    {
        var actualComparer = comparer ?? Comparer<T>.Default;
        var assertion = AssertionBuilder.For<T>()
            .Verify(actualValue => actualComparer.Compare(actualValue, otherValue) < 0)
            .ExpectValue($"to be less than {Format(otherValue)}")
            .DescribeActual(actualValue => $"{Format(actualValue)} is actually greater than or equal to {Format(otherValue)}")
            .DescribeActualWhenNegated(_ => "it is");
        return subject.Verify(assertion);
    }

    /// <summary>
    /// Asserts that the subject's value is less than or equal to the specified value.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="otherValue">The value to compare the subject against.</param>
    /// <param name="comparer">The comparer to use. If null, the default comparer for this type will be used.</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<T> BeLessThanOrEqualTo<T>(this IAssertionSubject<T> subject, T otherValue, IComparer<T>? comparer = null)
        where T : IComparable<T>
    {
        var actualComparer = comparer ?? Comparer<T>.Default;
        var assertion = AssertionBuilder.For<T>()
            .Verify(actualValue => actualComparer.Compare(actualValue, otherValue) <= 0)
            .ExpectValue($"to be less than or equal to {Format(otherValue)}")
            .DescribeActual(actualValue => $"{Format(actualValue)} is actually greater than {Format(otherValue)}")
            .DescribeActualWhenNegated(_ => "it is");
        return subject.Verify(assertion);
    }
}
