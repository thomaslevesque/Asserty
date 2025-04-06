using Asserty.Internal;

namespace Asserty.Assertions;

/// <summary>
/// Represents the subject of an assertion, encapsulating its value and the expression used in code to assert it.
/// </summary>
/// <typeparam name="TSubject">The type of the subject's value.</typeparam>
public interface IAssertionSubject<out TSubject> : IHideObjectMembers
{
    /// <summary>
    /// The subject's value.
    /// </summary>
    TSubject Value { get; }

    /// <summary>
    /// The expression used in code to assert the subject.
    /// </summary>
    string Expression { get; }

    /// <summary>
    /// Verifies the specified assertion against the subject.
    /// </summary>
    /// <param name="assertion">The assertion to verify.</param>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    /// <remarks>This method is not typically called when writing tests, but can be used to extend Asserty with new assertion types.</remarks>
    IAssertionResult<TSubject> Verify(IAssertion<TSubject> assertion);

    /// <summary>
    /// Casts this assertion subject to a different type.
    /// </summary>
    /// <typeparam name="TResult">The type of the new subject.</typeparam>
    /// <returns>A new subject with the specified type.</returns>
    /// <exception cref="InvalidCastException">The subject's value can't be cast to the specified type.</exception>
    /// <remarks>This method is not typically called when writing tests, but can be used to extend Asserty with new assertion types.</remarks>
    IAssertionSubject<TResult> Cast<TResult>();
}
