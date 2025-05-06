namespace Asserty.FixieTests.AssertionTesting;

public abstract record ExpectedAssertionResult
{
    public static ExpectedAssertionResult Pass() => new PassAssertionResult();
    public static ExpectedAssertionResult Fail(string failureMessage) => new FailAssertionResult(failureMessage);

    public abstract void Verify(Action action, string assertionDescription);

    private record PassAssertionResult : ExpectedAssertionResult
    {
        public override void Verify(Action action, string assertionDescription)
        {
            try
            {
                action();
            }
            catch (AssertionException ex)
            {
                throw new AssertionException(
                    $"""
                     Expected assertion `{assertionDescription}` to pass, but it failed with the following message:
                     "{ex.Message}"
                     """,
                    ex);
            }
        }

        public override string ToString() => "should pass";
    }

    private record FailAssertionResult(string FailureMessage) : ExpectedAssertionResult
    {
        public override void Verify(Action action, string assertionDescription)
        {
            try
            {
                action();
            }
            catch (AssertionException ex)
            {
                if (ex.Message == FailureMessage)
                    return;

                throw new AssertionException(
                    $"""
                     Assertion `{assertionDescription}` failed as expected, but the failure message is wrong.
                     Expected: "{FailureMessage}"
                     Actual: "{ex.Message}"
                     """,
                    ex);
            }

            throw new AssertionException($"Expected assertion `{assertionDescription}` to fail, but it passed.");
        }

        public override string ToString() => "should fail";
    }
}
