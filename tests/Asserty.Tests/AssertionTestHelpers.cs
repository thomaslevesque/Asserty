using System.Runtime.CompilerServices;
using Asserty.Internal;

namespace Asserty.Tests;

public static class AssertionTestHelpers
{
    public static IAssertionTest Expect(Action action, [CallerArgumentExpression(nameof(action))] string expression = null!)
    {
        return new AssertionTest(action, expression);
    }

    public interface IAssertionTest : IHideObjectMembers
    {
        void ToPass();
        void ToFail(string expectedMessage);
    }

    private class AssertionTest(Action action, string expression) : IAssertionTest
    {
        public void ToPass()
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

        public void ToFail(string expectedMessage)
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
