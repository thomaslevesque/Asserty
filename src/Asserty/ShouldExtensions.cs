using System.Runtime.CompilerServices;
using Asserty.Internal;

namespace Asserty;

public static class ShouldExtensions
{
    public static IPositiveAssertionSubject<T> Should<T>(this T value, [CallerArgumentExpression(nameof(value))] string? expression = null)
    {
        return new PositiveAssertionSubject<T>(value, expression);
    }
}
