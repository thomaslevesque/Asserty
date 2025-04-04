using Asserty.Assertions;

namespace Asserty.Internal;

internal static class AssertionHelper
{
    public static IAssertionResult<T> Verify<T>(IAssertion<T> assertion, IAssertionSubject<T> subject)
    {
        if (!assertion.IsVerified(subject.Value))
        {
            var message = CreateAssertionFailureMessage(assertion, subject);
            throw new AssertionException(message);
        }

        return new AssertionResult<T>(subject);
    }

    private static string CreateAssertionFailureMessage<T>(IAssertion<T> assertion, IAssertionSubject<T> subject)
    {
        var subjectExpression = GetSubjectExpression(subject);
        var expectationDescription = assertion.GetExpectationDescription();
        var actualDescription = assertion.GetActualDescription(subject.Value);
        return $"Expected {subjectExpression} {expectationDescription}, but {actualDescription}.";
    }

    private static string GetSubjectExpression<T>(IAssertionSubject<T> subject)
    {
        return !string.IsNullOrWhiteSpace(subject.Expression)
            ? $"`{subject.Expression}`"
            : $"(expression of type `{typeof(T)}`)";
    }
}
