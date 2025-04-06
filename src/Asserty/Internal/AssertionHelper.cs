using Asserty.Assertions;

namespace Asserty.Internal;

internal static class AssertionHelper
{
    public static IAssertionResult<TSubject> Verify<TSubject>(
        IAssertion<TSubject> assertion,
        IAssertionSubject<TSubject> subject)
    {
        if (!assertion.IsPreconditionVerified(subject.Value))
        {
            var message = CreatePreconditionFailureMessage(assertion, subject);
            throw new AssertionException(message);
        }

        if (!assertion.IsVerified(subject.Value))
        {
            var message = CreateAssertionFailureMessage(assertion, subject);
            throw new AssertionException(message);
        }

        return new AssertionResult<TSubject>(subject);
    }

    private static string CreatePreconditionFailureMessage<TSubject>(
        IAssertion<TSubject> assertion,
        IAssertionSubject<TSubject> subject)
    {
        var actualDescription = assertion.GetPreconditionFailureDescription(subject.Value)
                                ?? assertion.GetActualDescription(subject.Value);
        return CreateAssertionFailureMessage(assertion, subject, actualDescription);
    }

    private static string CreateAssertionFailureMessage<TSubject>(
        IAssertion<TSubject> assertion,
        IAssertionSubject<TSubject> subject)
    {
        var actualDescription = assertion.GetActualDescription(subject.Value);
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
