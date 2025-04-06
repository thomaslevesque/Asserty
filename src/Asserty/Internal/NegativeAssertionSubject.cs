using Asserty.Assertions;

namespace Asserty.Internal;

internal record NegativeAssertionSubject<TSubject>(TSubject Value, string Expression) : INegativeAssertionSubject<TSubject>
{
    public IAssertionResult<TSubject> Verify(IAssertion<TSubject> assertion) =>
        AssertionHelper.Verify(assertion.GetNegativeAssertion(), this);

    public IAssertionSubject<TResult> Cast<TResult>() =>
        new NegativeAssertionSubject<TResult>((TResult)(object)Value!, Expression);
}
