namespace Asserty.FixieTests.AssertionTesting;

public abstract class AssertionTestsBase
{
    public void Test(IAssertionTestCase testCase)
    {
        testCase.Execute();
    }

    public abstract void Describe(IAssertionTestBuilder builder);
}
