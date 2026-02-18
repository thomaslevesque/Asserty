using Asserty.Assertions;

namespace Asserty.Internal;

internal static class AssertionHelper
{
    public static IAssertionResult<TSubject> Verify<TSubject>(
        IAssertion<TSubject> assertion,
        IAssertionSubject<TSubject> subject)
    {
        var context = new AssertionEvaluationContext();
        if (!assertion.IsVerified(subject.Value, context))
        {
            var message = CreateAssertionFailureMessage(assertion, subject, context);
            throw new AssertionException(message);
        }

        return new AssertionResult<TSubject>(subject);
    }

    private static string CreateAssertionFailureMessage<TSubject>(
        IAssertion<TSubject> assertion,
        IAssertionSubject<TSubject> subject,
        AssertionEvaluationContext context)
    {
        var actualDescription = assertion.GetActualDescription(subject.Value, context);
        return CreateAssertionFailureMessage(assertion, subject, actualDescription);
    }

    private static string CreateAssertionFailureMessage<TSubject>(
        IAssertion<TSubject> assertion,
        IAssertionSubject<TSubject> subject,
        string actualDescription)
    {
        var expectationDescription = assertion.GetExpectationDescription();
        return $"Expected `{subject.Expression}` {expectationDescription}, but {actualDescription}.";
    }
}
