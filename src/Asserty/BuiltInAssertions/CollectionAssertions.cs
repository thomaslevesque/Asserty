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
            .Verify((actualValue, context) => context.Set("count", actualValue?.Count()) == count)
            .ExpectValue($"to contain {count} {Elements(count)}")
            .DescribeActual((actualValue, context) =>
            {
                if (actualValue is null)
                    return "it is actually null";
                var actualCount = context.Get<int>("count");
                return $"{Format(actualValue)} contains {actualCount} {Elements(actualCount)}";
            })
            .DescribeActualWhenNegated(_ => "it does");
        return subject.Verify(assertion);
    }

    /// <summary>
    /// Asserts that the subject's value contains the same elements as the specified collection, in any order.
    /// Duplicate elements in the expected collection are expected to appear the same number of times in the actual
    /// collection.
    /// </summary>
    /// <param name="subject">The subject of the assertion</param>
    /// <param name="expectedCollection">The expected collection of elements.</param>
    /// <param name="equalityComparer">The equality comparer to use to compare elements. If null, the default comparer
    /// for this type will be used.</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<IEnumerable<T>?> HaveSameElementsAs<T>(
        this IAssertionSubject<IEnumerable<T>?> subject,
        IEnumerable<T> expectedCollection,
        IEqualityComparer<T>? equalityComparer = null)
    {
        ArgumentNullException.ThrowIfNull(expectedCollection);

        var actualComparer = equalityComparer ?? EqualityComparer<T>.Default;
        var assertion = AssertionBuilder.For<IEnumerable<T>?>()
            .Verify((actualValue, context) =>
            {
                if (actualValue is null)
                    return false;

                var boxComparer = new BoxEqualityComparer<T>(actualComparer);
                var elementCounts = expectedCollection
                    .GroupBy(element => new Box<T>(element), boxComparer)
                    .ToDictionary(group => group.Key, group => group.Count(), boxComparer);

                foreach (var element in actualValue)
                {
                    var key = new Box<T>(element);
                    if (!elementCounts.TryGetValue(key, out var count) || count == 0)
                    {
                        // Actual collection contains an element that is not in the expected collection, or contains
                        // more occurrences of an element than expected.
                        context.Set("unexpectedElement", element);
                        return false;
                    }

                    if (--count == 0)
                    {
                        elementCounts.Remove(key);
                    }
                    else
                    {
                        elementCounts[key] = count;
                    }
                }

                if (elementCounts.Count > 0)
                {
                    // Actual collection is missing some expected elements.
                    var missingElement = elementCounts.Keys.First().Value;
                    context.Set("missingExpectedElement", missingElement);
                    return false;
                }

                return true;
            })
            .ExpectValue($"to have the same elements as {Format(expectedCollection)}")
            .DescribeActual((actualValue, context) =>
            {
                if (actualValue is null)
                    return "it is null";

                if (context.TryGet("missingExpectedElement", out T missingExpectedElement))
                {
                    return $"{Format(actualValue)} does not (missing expected element {Format(missingExpectedElement)})";
                }

                if (context.TryGet("unexpectedElement", out T unexpectedElement))
                {
                    return $"{Format(actualValue)} does not (contains unexpected element {Format(unexpectedElement)})";
                }

                // Should never reach here, since either missingExpectedElement or unexpectedElement should always be
                // set when the assertion fails, but just in case, return a generic message.
                return $"{Format(actualValue)} does not";
            })
            .DescribeActualWhenNegated(_ => "it does");
        return subject.Verify(assertion);
    }

    /// <summary>
    /// Asserts that the subject's value contains the same elements as the specified sequence, in the same order.
    /// </summary>
    /// <param name="subject">The subject of the assertion</param>
    /// <param name="expectedSequence">The expected sequence of elements.</param>
    /// <param name="equalityComparer">The equality comparer to use to compare elements. If null, the default comparer
    /// for this type will be used.</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<IEnumerable<T>?> BeSameSequenceAs<T>(
        this IAssertionSubject<IEnumerable<T>?> subject,
        IEnumerable<T> expectedSequence,
        IEqualityComparer<T>? equalityComparer = null)
    {
        ArgumentNullException.ThrowIfNull(expectedSequence);

        var actualComparer = equalityComparer ?? EqualityComparer<T>.Default;
        var assertion = AssertionBuilder.For<IEnumerable<T>?>()
            .Verify((actualValue, context) =>
            {
                if (actualValue is null)
                    return false;

                using var actualEnumerator = actualValue.GetEnumerator();
                using var expectedEnumerator = expectedSequence.GetEnumerator();

                var position = 0;
                while (true)
                {
                    var hasActual = actualEnumerator.MoveNext();
                    var hasExpected = expectedEnumerator.MoveNext();
                    if (!hasActual && !hasExpected)
                        return true;

                    if (!hasActual
                        || !hasExpected
                        || !actualComparer.Equals(actualEnumerator.Current, expectedEnumerator.Current))
                    {
                        context.Set("differencePosition", position);
                        return false;
                    }

                    position++;
                }
            })
            .ExpectValue($"to be the same sequence as {Format(expectedSequence)}")
            .DescribeActual((actualValue, context) => actualValue is null
                ? "it is null"
                : $"{Format(actualValue)} differs at position {context.Get<int>("differencePosition")}")
            .DescribeActualWhenNegated(_ => "it is");
        return subject.Verify(assertion);
    }

    /// <summary>
    /// Asserts that the subject's value contains the specified element.
    /// </summary>
    /// <param name="subject">The subject of the assertion</param>
    /// <param name="expectedElement">The element that the collection must contain.</param>
    /// <param name="equalityComparer">The equality comparer to use to compare elements. If null, the default comparer
    /// for this type will be used.</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<IEnumerable<T>?> Contain<T>(
        this IAssertionSubject<IEnumerable<T>?> subject,
        T expectedElement,
        IEqualityComparer<T>? equalityComparer = null)
    {
        var actualComparer = equalityComparer ?? EqualityComparer<T>.Default;
        var assertion = AssertionBuilder.For<IEnumerable<T>?>()
            .Verify(actualValue => actualValue?.Contains(expectedElement, actualComparer) ?? false)
            .ExpectValue($"to contain {Format(expectedElement)}")
            .DescribeActual(actualValue => actualValue is null
                ? "it is null"
                : $"{Format(actualValue)} doesn't")
            .DescribeActualWhenNegated(actualValue => $"{Format(actualValue)} does");
        return subject.Verify(assertion);
    }

    private static string Elements(int count) => count > 1 ? "elements" : "element";

    private readonly record struct Box<TValue>(TValue Value);

    private sealed class BoxEqualityComparer<TValue>(IEqualityComparer<TValue> valueComparer) : IEqualityComparer<Box<TValue>>
    {
        public bool Equals(Box<TValue> x, Box<TValue> y) => valueComparer.Equals(x.Value, y.Value);

        public int GetHashCode(Box<TValue> obj) => obj.Value is null ? 0 : valueComparer.GetHashCode(obj.Value);
    }
}
