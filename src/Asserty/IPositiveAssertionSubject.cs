namespace Asserty;

public interface IPositiveAssertionSubject<out T> : IAssertionSubject<T>
{
    INegativeAssertionSubject<T> Not { get; }
}
