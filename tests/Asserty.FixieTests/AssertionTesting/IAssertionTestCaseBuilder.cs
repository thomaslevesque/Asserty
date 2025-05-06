using System.Runtime.CompilerServices;

namespace Asserty.FixieTests.AssertionTesting;

public interface IAssertionTestCaseBuilder<TSubject>
{
    IAssertionTestCaseBuilder<TSubject> ForSubject(
        TSubject value,
        ExpectedAssertionResult expectedPositiveAssertionResult,
        ExpectedAssertionResult expectedNegativeAssertionResult,
        [CallerArgumentExpression(nameof(value))] string expression = null!);
}
