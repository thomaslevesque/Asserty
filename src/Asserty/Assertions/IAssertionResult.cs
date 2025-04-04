namespace Asserty.Assertions;

public interface IAssertionResult<out T>
{
    IPositiveAssertionSubject<T> And { get; }
    IAssertionResult<TResult> Cast<TResult>();
}
