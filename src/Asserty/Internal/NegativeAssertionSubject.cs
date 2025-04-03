namespace Asserty.Internal;

internal record NegativeAssertionSubject<T>(T Value, string Expression) : INegativeAssertionSubject<T>
{
    public IAssertionResult<T> Verify(IAssertion<T> assertion) =>
        AssertionHelper.Verify(assertion.GetNegativeAssertion(), this);

    public IAssertionSubject<TResult> Cast<TResult>() =>
        new NegativeAssertionSubject<TResult>((TResult)(object)Value!, Expression);
}
