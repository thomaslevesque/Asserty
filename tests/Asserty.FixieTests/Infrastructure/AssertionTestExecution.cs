using Asserty.FixieTests.AssertionTesting;
using Fixie;

namespace Asserty.FixieTests.Infrastructure;

public class AssertionTestExecution : IExecution
{
    public async Task Run(TestSuite testSuite)
    {
        foreach (var testClass in testSuite.TestClasses)
        {
            var instance = (IAssertionTests)testClass.Construct();
            var builder = new AssertionTestBuilder();
            instance.Describe(builder);
            var testCases = builder.GetTestCases().ToList();
            foreach (var test in testClass.Tests)
            {
                foreach (var testCase in testCases)
                {
                    await test.Run(instance, [testCase]);
                }
            }
        }
    }
}
