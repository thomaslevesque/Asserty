using Asserty.Assertions;

namespace Asserty.Internal;

internal static class AssertionSubjectFactory
{
    public static IPositiveAssertionSubject<TSubject> CreatePositive<TSubject>(TSubject value, string expression)
    {
        AssertExpressionIsValid(expression);
        return new PositiveAssertionSubject<TSubject>(value, expression);
    }

    public static INegativeAssertionSubject<TSubject> CreateNegative<TSubject>(TSubject value, string expression)
    {
        AssertExpressionIsValid(expression);
        return new NegativeAssertionSubject<TSubject>(value, expression);
    }

    private static void AssertExpressionIsValid(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            throw new ArgumentException(
                "In order to have meaningful assertion failure messages, expression must not be null or empty. Just leave it unspecified, the compiler will provide the value (requires C# 10 or later).",
                nameof(expression));
        }
    }
}
