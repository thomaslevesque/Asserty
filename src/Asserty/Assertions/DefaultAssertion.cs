namespace Asserty.Assertions;

public abstract class DefaultAssertion<T> : IAssertion<T>
{
    public abstract bool IsVerified(T actualValue);
    public abstract string GetExpectationDescription();
    public abstract string GetActualDescription(T actualValue);
    public virtual IAssertion<T> GetNegativeAssertion() => new DefaultNegativeAssertion<T>(this);
}