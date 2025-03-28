namespace Asserty;

public interface IAssertionSubject<out T>
{
    T Value { get; }
    string? Expression { get; }
}
