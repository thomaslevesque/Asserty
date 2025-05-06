using Asserty.FixieTests.AssertionTesting;

namespace Asserty.FixieTests;

public class BeEqualToTests : IAssertionTests
{
    public void Test(IAssertionTestCase testCase) => testCase.Execute();

    public void Describe(IAssertionTestBuilder builder)
    {
        builder.For<string?>(s => s.BeEqualTo("hello"))
            .ForSubject(
                "hello",
                Pass(),
                Fail("Expected `value` not to be equal to \"hello\", but it is actually equal."))
            .ForSubject(
                "hi",
                Fail("Expected `value` to be equal to \"hello\", but it is actually \"hi\"."),
                Pass())
            .ForSubject(
                null,
                Fail("Expected `value` to be equal to \"hello\", but it is actually (null)."),
                Pass());
        builder.For<string>(s => s.BeEqualTo("hello", StringComparer.OrdinalIgnoreCase))
            .ForSubject(
                "HeLlO",
                Pass(),
                Fail("Expected `value` not to be equal to \"hello\", but it is actually equal."));
    }
}
