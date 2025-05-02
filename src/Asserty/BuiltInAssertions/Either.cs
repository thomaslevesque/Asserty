using Asserty.Assertions;
using Asserty.Internal;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that at least one of the specified assertions are verified by the subject.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="assertions">A list of assertions</param>
    /// <typeparam name="TSubject">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="ArgumentException">No assertion was specified.</exception>
    /// <exception cref="AssertionException">None of the assertions passed.</exception>
    public static IAssertionResult<TSubject> Either<TSubject>(
        this IPositiveAssertionSubject<TSubject> subject,
        params Action<IPositiveAssertionSubject<TSubject>>[] assertions)
    {
        if (assertions is [])
        {
            throw new ArgumentException("At least one assertion must be provided");
        }

        var failureMessages = new List<string>();
        foreach (var assertion in assertions)
        {
            try
            {
                assertion(subject);
                return new AssertionResult<TSubject>(subject);
            }
            catch (AssertionException ex)
            {
                failureMessages.Add(ex.Message);
            }
        }

        var message = $"""
            Expected either of multiple assertions to pass, but none does. Assertion failure messages:
            {string.Join(Environment.NewLine, failureMessages.Select(m => "- " + m))}
            """;
        throw new AssertionException(message);
    }

    /// <summary>
    /// Asserts that none of the specified assertions are verified by the subject.
    /// </summary>
    /// <param name="subject">The subject of the assertion.</param>
    /// <param name="assertions">A list of assertions</param>
    /// <typeparam name="TSubject">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="ArgumentException">No assertion was specified.</exception>
    /// <exception cref="AssertionException">None of the assertions passed.</exception>
    public static IAssertionResult<TSubject> Neither<TSubject>(
        this IPositiveAssertionSubject<TSubject> subject,
        params Action<IPositiveAssertionSubject<TSubject>>[] assertions)
    {
        if (assertions is [])
        {
            throw new ArgumentException("At least one assertion must be provided");
        }

        var failureMessages = new List<string>();
        foreach (var assertion in assertions)
        {
            try
            {
                assertion(subject);
            }
            catch (AssertionException)
            {
                // Expected
                continue;
            }

            var failureMessage = GetExpectedFailureMessage();
            failureMessages.Add(failureMessage ?? "(cannot get failure message)");

            string? GetExpectedFailureMessage()
            {
                try
                {
                    // If the assertion passed, the negated assertion should not, so this should throw the expected
                    // exception. If not, not much we can do to identify the actual failure.
                    assertion(new NegativeAssertionAsPositive<TSubject>(subject));
                    return null;
                }
                catch (AssertionException ex)
                {
                    return ex.Message;
                }
            }
        }

        if (failureMessages.Any())
        {
            string message = $"""
                Expected neither of multiple assertions to pass, but at least one does:
                {string.Join(Environment.NewLine, failureMessages.Select(m => "- " + m))}
                """;
            throw new AssertionException(message);
        }

        return new AssertionResult<TSubject>(subject);
    }

    // Note: the goal of this class is to make an intentionally failing assertion: the negative subject pretends to be
    // positive, and vice versa.
    private class NegativeAssertionAsPositive<TSubject>(IPositiveAssertionSubject<TSubject> positiveSubject)
        : IPositiveAssertionSubject<TSubject>
    {
        public TSubject Value => positiveSubject.Value;
        public string Expression => positiveSubject.Expression;

        public IAssertionResult<TSubject> Verify(IAssertion<TSubject> assertion) =>
            positiveSubject.Verify(assertion.GetNegativeAssertion());

        public IAssertionSubject<TResult> Cast<TResult>() => positiveSubject.Cast<TResult>();
        public INegativeAssertionSubject<TSubject> Not => new PositiveAssertionAsNegative(positiveSubject);

        private class PositiveAssertionAsNegative(IPositiveAssertionSubject<TSubject> positiveSubject)
            : INegativeAssertionSubject<TSubject>
        {
            public TSubject Value => positiveSubject.Value;
            public string Expression => positiveSubject.Expression;

            public IAssertionResult<TSubject> Verify(IAssertion<TSubject> assertion) =>
                positiveSubject.Verify(assertion);

            public IAssertionSubject<TResult> Cast<TResult>() => positiveSubject.Cast<TResult>();
        }
    }
}
