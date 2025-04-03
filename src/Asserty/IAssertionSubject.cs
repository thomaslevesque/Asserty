namespace Asserty;

public interface IAssertionSubject<out T> : IHideObjectMembers
{
    T Value { get; }
    string? Expression { get; }
}
