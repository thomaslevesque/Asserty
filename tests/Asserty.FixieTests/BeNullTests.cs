using Asserty.FixieTests.AssertionTesting;

namespace Asserty.FixieTests;

public class BeNullTests : IAssertionTests
{
    public void Test(IAssertionTestCase testCase) => testCase.Execute();

    public void Describe(IAssertionTestBuilder builder)
    {
        builder.For<string?>(s => s.BeNull())
            .ForSubject(
                null,
                Pass(),
                Fail("Expected `value` not to be null, but it is actually null."))
            .ForSubject(
                "hello",
                Fail("""Expected `value` to be null, but it is actually "hello"."""),
                Pass());
    }
}
