using Fixie;

namespace Asserty.FixieTests.Infrastructure;

internal class ParameterizedExecution : IExecution
{
    public async Task Run(TestSuite testSuite)
    {
        foreach (var test in testSuite.Tests)
        {
            if (test.HasParameters)
            {
                foreach (var parameters in test.GetAll<InputAttribute>().Select(x => x.Parameters))
                    await test.Run(parameters);
            }
            else
            {
                await test.Run();
            }
        }
    }
}
