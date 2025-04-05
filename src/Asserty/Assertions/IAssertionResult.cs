namespace Asserty.Assertions;

/// <summary>
/// Represents a successful assertion result, exposing a way to chain another assertion.
/// </summary>
/// <typeparam name="T">The type of the assertion subject's value.</typeparam>
/// <remarks>There is no failed assertion result, since an exception is thrown when an assertion fails.</remarks>
public interface IAssertionResult<out T>
{
    /// <summary>
    /// Returns the assertion subject so that another assertion can be chained.
    /// </summary>
    /// <remarks>Note that this always returns a positive assertion subject, i.e. if the assertion that returned this
    /// result was negated, the following assertion will not be negated unless you explicitly negate it with
    /// <see cref="IPositiveAssertionSubject{T}.Not"/>.</remarks>
    IPositiveAssertionSubject<T> And { get; }

    /// <summary>
    /// Casts this assertion result to a different type.
    /// </summary>
    /// <typeparam name="TResult">The type of the new result.</typeparam>
    /// <returns>A new result with the specified type.</returns>
    /// <remarks>This method is not typically called when writing tests, but can be used to extend Asserty with new assertion types.</remarks>
    IAssertionResult<TResult> Cast<TResult>();
}
