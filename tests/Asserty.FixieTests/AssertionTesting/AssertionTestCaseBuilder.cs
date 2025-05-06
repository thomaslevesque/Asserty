using Asserty.Assertions;

namespace Asserty.FixieTests.AssertionTesting;

internal class AssertionTestCaseBuilder<TSubject>(
    Func<IAssertionSubject<TSubject>, IAssertionResult<TSubject>> assertion,
    string description)
    : IAssertionTestCaseBuilder<TSubject>
{
    public IAssertionTestCaseBuilder<TSubject> ForSubject(
        TSubject value,
        ExpectedAssertionResult expectedPositiveAssertionResult,
        ExpectedAssertionResult expectedNegativeAssertionResult,
        string expression)
    {
        var subjectDescriptor = new AssertionTestSubjectDescriptor<TSubject>(
            value,
            expression,
            expectedPositiveAssertionResult,
            expectedNegativeAssertionResult);
        Descriptor.Subjects.Add(subjectDescriptor);
        return this;
    }

    public IAssertionTestCaseDescriptor Descriptor { get; } =
        new AssertionTestCaseDescriptor<TSubject>(assertion, description);
}
