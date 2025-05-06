using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Asserty.Assertions;

namespace Asserty.FixieTests.AssertionTesting;

public partial class AssertionTestBuilder : IAssertionTestBuilder
{
    private readonly List<IAssertionTestCaseDescriptor> _descriptors = [];

    public IAssertionTestCaseBuilder<TSubject> For<TSubject>(
        Func<IAssertionSubject<TSubject>, IAssertionResult<TSubject>> assertion,
        [CallerArgumentExpression(nameof(assertion))] string expression = null!)
    {
        var description = AssertionLambdaRegex().Replace(expression, "");
        var builder = new AssertionTestCaseBuilder<TSubject>(assertion, description);
        _descriptors.Add(builder.Descriptor);
        return builder;
    }

    public IEnumerable<IAssertionTestCase> GetTestCases()
    {
        return _descriptors.SelectMany(d => d.CreateTestCases());
    }

    [GeneratedRegex(@"(?<paramName>[a-zA-Z0-9_]+) => \k<paramName>.")]
    private static partial Regex AssertionLambdaRegex();
}
