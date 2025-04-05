using System.Runtime.CompilerServices;
using Asserty.Assertions;
using Asserty.Internal;

namespace Asserty;

/// <summary>
/// Expose the <see cref="Should{T}"/> method, which is the entry point of the assertion API.
/// </summary>
public static class ShouldExtensions
{
    /// <summary>
    /// Returns an assertion subject for the value, that can be used to verify various assertions on the value.
    /// </summary>
    /// <param name="value">The value for which an assertion subject is created.</param>
    /// <param name="expression">The expression used in code to represent the value. Note: don't specify an explicit
    /// value for this parameter, it will be provided automatically by the compiler (requires C# 10 or later).</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion subject.</returns>
    /// <exception cref="ArgumentException">A null or empty value was passed for the <paramref name="expression"/>
    /// parameter. This should only happen if you explicitly passed a null or empty value, or if you didn't provide a
    /// value and use a language version earlier than C# 10.</exception>
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
