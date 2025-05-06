namespace Asserty.FixieTests.AssertionTesting;

internal interface IAssertionTestCaseDescriptor
{
    IList<IAssertionTestSubjectDescriptor> Subjects { get; }

    IEnumerable<IAssertionTestCase> CreateTestCases();
}
