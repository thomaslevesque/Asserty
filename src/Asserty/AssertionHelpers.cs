namespace Asserty;

public static class AssertionHelpers
{
    public static void Assert<T>(
        IAssertionSubject<T> subject,
        IAssertion<T> assertion)
    {
        if (subject is INegativeAssertionSubject<T>)
            assertion = assertion.GetNegativeAssertion();

        var result = assertion.IsVerified(subject.Value);
        if (!result)
        {
            var message = CreateAssertionFailureMessage(assertion, subject);
            throw new AssertionException(message);
        }
    }

    private static string CreateAssertionFailureMessage<T>(IAssertion<T> assertion, IAssertionSubject<T> subject)
    {
        string subjectExpression = GetSubjectExpression(subject);
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
