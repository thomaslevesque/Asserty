namespace Asserty.Assertions;

public class DefaultNegativeAssertion<T>(IAssertion<T> positiveAssertion) : IAssertion<T>
{
    public virtual bool IsVerified(T actualValue) => !positiveAssertion.IsVerified(actualValue);

    public virtual string GetExpectationDescription() => "not " + positiveAssertion.GetExpectationDescription();

    public virtual string GetActualDescription(T actualValue) => positiveAssertion.GetActualDescription(actualValue);

    public IAssertion<T> GetNegativeAssertion() => positiveAssertion;
}
