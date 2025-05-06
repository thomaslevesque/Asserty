using Asserty.Assertions;

namespace Asserty.FixieTests.AssertionTesting;

internal class AssertionTestCaseDescriptor<TSubject>(
    Func<IAssertionSubject<TSubject>, IAssertionResult<TSubject>> assertion,
    string description)
    : IAssertionTestCaseDescriptor
{
    public IList<IAssertionTestSubjectDescriptor> Subjects { get; } = new List<IAssertionTestSubjectDescriptor>();

    public IEnumerable<IAssertionTestCase> CreateTestCases()
    {
        foreach (var subjectDescriptor in Subjects)
        {
            var value = (TSubject)subjectDescriptor.Value!;
            var positiveSubject = value.Should();
            var positiveAssertionDescription = $"{subjectDescriptor.Expression}.Should().{description}";
            yield return new AssertionTestCase<TSubject>(
                positiveSubject,
                s => assertion(s),
                positiveAssertionDescription,
                subjectDescriptor.ExpectedPositiveAssertionResult);

            var negativeSubject = positiveSubject.Not;
            var negativeAssertionDescription = $"{subjectDescriptor.Expression}.Should().Not.{description}";
            yield return new AssertionTestCase<TSubject>(
                negativeSubject,
                s => assertion(s),
                negativeAssertionDescription,
                subjectDescriptor.ExpectedNegativeAssertionResult);
        }
    }
}
