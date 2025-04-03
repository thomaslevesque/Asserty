namespace Asserty;

public interface IAssertionSubject<out T> : IHideObjectMembers
{
    T Value { get; }
    string Expression { get; }

    IAssertionResult<T> Verify(IAssertion<T> assertion);

    IAssertionSubject<TResult> Cast<TResult>();
}
