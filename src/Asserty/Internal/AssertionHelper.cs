using Asserty.Assertions;

namespace Asserty.Internal;

internal static class AssertionHelper
{
    public static IAssertionResult<TSubject> Verify<TSubject>(IAssertion<TSubject> assertion, IAssertionSubject<TSubject> subject)
    {
        if (!assertion.IsVerified(subject.Value))
        {
            var message = CreateAssertionFailureMessage(assertion, subject);
            throw new AssertionException(message);
        }

        return new AssertionResult<TSubject>(subject);
    }

    private static string CreateAssertionFailureMessage<TSubject>(IAssertion<TSubject> assertion, IAssertionSubject<TSubject> subject)
    {
        var subjectExpression = GetSubjectExpression(subject);
        var expectationDescription = assertion.GetExpectationDescription();
        var actualDescription = assertion.GetActualDescription(subject.Value);
        return $"Expected {subjectExpression} {expectationDescription}, but {actualDescription}.";
    }

    private static string GetSubjectExpression<TSubject>(IAssertionSubject<TSubject> subject)
    {
        return !string.IsNullOrWhiteSpace(subject.Expression)
            ? $"`{subject.Expression}`"
            : $"(expression of type `{typeof(TSubject)}`)";
    }
}
