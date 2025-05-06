namespace Asserty.FixieTests.AssertionTesting;

internal interface IAssertionTestSubjectDescriptor
{
    object? Value { get; }
    string Expression { get; }
    ExpectedAssertionResult ExpectedPositiveAssertionResult { get; }
    ExpectedAssertionResult ExpectedNegativeAssertionResult { get; }
}
