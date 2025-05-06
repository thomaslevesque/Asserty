using System.Runtime.CompilerServices;
using Asserty.Assertions;

namespace Asserty.FixieTests.AssertionTesting;

public interface IAssertionTestBuilder
{
    IAssertionTestCaseBuilder<TSubject> For<TSubject>(
        Func<IAssertionSubject<TSubject>, IAssertionResult<TSubject>> assertion,
        [CallerArgumentExpression(nameof(assertion))] string expression = null!);
}
