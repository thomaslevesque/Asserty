namespace Asserty.Assertions;

public interface IPositiveAssertionSubject<out T> : IAssertionSubject<T>
{
    INegativeAssertionSubject<T> Not { get; }
}
