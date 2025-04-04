using System.Runtime.CompilerServices;
using Asserty.Assertions;
using Asserty.Internal;

namespace Asserty;

public static class ShouldExtensions
{
    public static IPositiveAssertionSubject<T> Should<T>(this T value, [CallerArgumentExpression(nameof(value))] string expression = null!)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            throw new ArgumentException(
                "In order to have meaningful assertion failure messages, expression must not be null or empty. Just leave it unspecified, the compiler will provide the value (requires C# 10 or later).",
                nameof(expression));
        }

        return new PositiveAssertionSubject<T>(value, expression);
    }
}
