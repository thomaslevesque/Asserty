namespace Asserty.Internal;

internal record NegativeAssertionSubject<T>(T Value, string? Expression) : INegativeAssertionSubject<T>;
