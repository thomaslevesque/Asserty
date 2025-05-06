using Asserty.Assertions;

namespace Asserty.FixieTests.AssertionTesting;

public class AssertionTestCase<TSubject> : IAssertionTestCase
{
    private readonly IAssertionSubject<TSubject> _subject;
    private readonly Action<IAssertionSubject<TSubject>> _assertion;
    private readonly string _assertionDescription;
    private readonly ExpectedAssertionResult _expectedResult;

    internal AssertionTestCase(
        IAssertionSubject<TSubject> subject,
        Action<IAssertionSubject<TSubject>> assertion,
        string assertionDescription,
        ExpectedAssertionResult expectedResult)
    {
        _subject = subject;
        _assertion = assertion;
        _assertionDescription = assertionDescription;
        _expectedResult = expectedResult;
    }

    public void Execute()
    {
        _expectedResult.Verify(
            () => _assertion(_subject),
            _assertionDescription);
    }

    public override string ToString() => $"[{_assertionDescription}] {_expectedResult}";
}
