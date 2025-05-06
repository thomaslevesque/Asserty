namespace Asserty.FixieTests.AssertionTesting;

internal record AssertionTestSubjectDescriptor<TSubject>(
    TSubject Value,
    string Expression,
    ExpectedAssertionResult ExpectedPositiveAssertionResult,
    ExpectedAssertionResult ExpectedNegativeAssertionResult)
    : IAssertionTestSubjectDescriptor
{
    object? IAssertionTestSubjectDescriptor.Value => Value;
}
