namespace Asserty.Internal;

internal record PositiveAssertionSubject<T>(T Value, string? Expression) : IPositiveAssertionSubject<T>
{
    public INegativeAssertionSubject<T> Not => new NegativeAssertionSubject<T>(Value, Expression);
}
