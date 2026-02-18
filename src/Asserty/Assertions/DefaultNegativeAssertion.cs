namespace Asserty.Assertions;

/// <summary>
/// A default implementation of a negative assertion based on a positive assertion, to be returned by
/// <see cref="IAssertion{TSubject}.GetNegativeAssertion"/>.
/// </summary>
/// <param name="positiveAssertion">The positive assertion this negative assertion is based on.</param>
/// <typeparam name="TSubject">The type of the assertion subject's value.</typeparam>
/// <remarks>End users will not typically need to use this class. It can be used as a base for the negative assertion
/// when implementing a custom assertion without using AssertionBuilder.</remarks>
public class DefaultNegativeAssertion<TSubject>(IAssertion<TSubject> positiveAssertion) : IAssertion<TSubject>
{
    /// <inheritdoc />
    public virtual bool IsVerified(TSubject actualValue, AssertionEvaluationContext context) =>
        !positiveAssertion.IsVerified(actualValue, context);

    /// <inheritdoc />
    public virtual string GetExpectationDescription() => "not " + positiveAssertion.GetExpectationDescription();

    /// <inheritdoc />
    public virtual string GetActualDescription(TSubject actualValue, AssertionEvaluationContext context) =>
        positiveAssertion.GetActualDescription(actualValue, context);

    /// <inheritdoc />
    public IAssertion<TSubject> GetNegativeAssertion() => positiveAssertion;
}
