namespace Asserty.Assertions;

/// <summary>
/// Represents an assertion.
/// </summary>
/// <typeparam name="T">The type of the asserted value.</typeparam>
public interface IAssertion<in T>
{
    /// <summary>
    /// Evaluates whether the assertion is verified.
    /// </summary>
    /// <param name="actualValue">The actual value on which to evaluate the assertion.</param>
    /// <returns><c>true</c> if the assertion is verified, otherwise <c>false</c>.</returns>
    bool IsVerified(T actualValue);

    /// <summary>
    /// Returns the part of the assertion failure message describing what is expected, following "Expected {expression}…",
    /// starting with "to" or "not to" (e.g. "to be equal to 42" or "to contain 3 elements").
    /// </summary>
    /// <returns>The part of the assertion failure message describing what is expected.</returns>
    string GetExpectationDescription();

    /// <summary>
    /// Returns the part of the assertion failure message describing what was actually observed, following "but…"
    /// (e.g. "it is actually 0" or "it actually contains 1 element").
    /// </summary>
    /// <param name="actualValue">The actual value of the subject</param>
    /// <returns>The part of the assertion failure message describing what was actually observed.</returns>
    string GetActualDescription(T actualValue);

    /// <summary>
    /// Returns the negation of this assertion (e.g. not equal instead of equal)
    /// </summary>
    /// <returns>An assertion that is the negation of this assertion.</returns>
    IAssertion<T> GetNegativeAssertion();
}
