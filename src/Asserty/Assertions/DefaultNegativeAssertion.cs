namespace Asserty.Assertions;

/// <summary>
/// A default implementation of a negative assertion based on a positive assertion, to be returned by
/// <see cref="IAssertion{T}.GetNegativeAssertion"/>.
/// </summary>
/// <param name="positiveAssertion">The positive assertion this negative assertion is based on.</param>
/// <typeparam name="T">The type of the assertion subject's value.</typeparam>
/// <remarks>End users will not typically need to use this class. It can be used as a base for the negative assertion
/// when implementing a custom assertion without using AssertionBuilder.</remarks>
public class DefaultNegativeAssertion<T>(IAssertion<T> positiveAssertion) : IAssertion<T>
{
    /// <inheritdoc />
    public virtual bool IsVerified(T actualValue) => !positiveAssertion.IsVerified(actualValue);

    /// <inheritdoc />
    public virtual string GetExpectationDescription() => "not " + positiveAssertion.GetExpectationDescription();

    /// <inheritdoc />
    public virtual string GetActualDescription(T actualValue) => positiveAssertion.GetActualDescription(actualValue);

    /// <inheritdoc />
    public IAssertion<T> GetNegativeAssertion() => positiveAssertion;
}
