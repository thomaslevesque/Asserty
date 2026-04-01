using System.Runtime.CompilerServices;
using Asserty.Internal;

namespace Asserty.Tests;

public static class Verify
{
    public static IAssertionTest That(Action action, [CallerArgumentExpression(nameof(action))] string expression = null!)
    {
        return new AssertionTest(action, expression);
    }

    public interface IAssertionTest : IHideObjectMembers
    {
        void Passes();
        void Fails(string expectedMessage);
    }

    private class AssertionTest(Action action, string expression) : IAssertionTest
    {
        public void Passes()
        {
            try
            {
                action();
            }
            catch (AssertionException ex)
            {
                throw new AssertionException(
                    $"""
                     Expected assertion `{expression}` to pass, but it failed with the following message:
                     "{ex.Message}"
                     """,
                    ex);
            }
        }

        public void Fails(string expectedMessage)
        {
            try
            {
                action();
            }
            catch (AssertionException ex)
            {
                if (ex.Message == expectedMessage)
                    return;

                throw new AssertionException(
                    $"""
                     Assertion `{expression}` failed as expected, but the failure message is wrong.
                     Expected: "{expectedMessage}"
                     Actual: "{ex.Message}"
                     """,
                    ex);

            }

            throw new AssertionException($"Expected assertion `{expression}` to fail, but it passed.");
        }
    }
}
