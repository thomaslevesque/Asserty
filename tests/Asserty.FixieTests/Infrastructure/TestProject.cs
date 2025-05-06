using Fixie;

namespace Asserty.FixieTests.Infrastructure;

public class TestProject : ITestProject
{
    public void Configure(TestConfiguration configuration, TestEnvironment environment)
    {
        configuration.Conventions.Add<AssertionTestDiscovery, AssertionTestExecution>();
        configuration.Conventions.Add<OtherTestDiscovery, ParameterizedExecution>();
    }
}
