namespace Asserty;

public interface IAssertionResult<out T>
{
    IPositiveAssertionSubject<T> And { get; }
}
