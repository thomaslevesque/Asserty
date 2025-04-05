namespace Asserty.Assertions;

/// <summary>
/// Represents the subject of an assertion that has not be negated (e.g. `value.Should().BeEqualTo(42)`)
/// </summary>
/// <typeparam name="T">The type of the subject's value.</typeparam>
public interface IPositiveAssertionSubject<out T> : IAssertionSubject<T>
{
    /// <summary>
    /// Returns an assertion subject that negates the following assertion.
    /// </summary>
    /// <remarks>Note that the negation only applies to the next assertion, not subsequent assertions in an assertion
    /// chain.</remarks>
    INegativeAssertionSubject<T> Not { get; }
}
