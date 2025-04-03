namespace Asserty.Internal;

internal record PositiveAssertionSubject<T>(T Value, string Expression) : IPositiveAssertionSubject<T>
{
    public IAssertionResult<T> Verify(IAssertion<T> assertion) =>
        AssertionHelper.Verify(assertion, this);

    public INegativeAssertionSubject<T> Not => new NegativeAssertionSubject<T>(Value, Expression);
}
