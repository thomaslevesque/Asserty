using Asserty.Assertions;

namespace Asserty.Internal;

internal record PositiveAssertionSubject<TSubject>(TSubject Value, string Expression) : IPositiveAssertionSubject<TSubject>
{
    public IAssertionResult<TSubject> Verify(IAssertion<TSubject> assertion) =>
        AssertionHelper.Verify(assertion, this);

    public INegativeAssertionSubject<TSubject> Not => new NegativeAssertionSubject<TSubject>(Value, Expression);

    public IAssertionSubject<TResult> Cast<TResult>() =>
        new PositiveAssertionSubject<TResult>((TResult)(object)Value!, Expression);
}
