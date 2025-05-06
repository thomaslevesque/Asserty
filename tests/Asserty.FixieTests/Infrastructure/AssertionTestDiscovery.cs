using System.Reflection;
using Asserty.FixieTests.AssertionTesting;
using Fixie;

namespace Asserty.FixieTests.Infrastructure;

public class AssertionTestDiscovery : IDiscovery
{
    public IEnumerable<Type> TestClasses(IEnumerable<Type> concreteClasses) =>
        concreteClasses.Where(c => c.Name.EndsWith("Tests") && c.IsAssignableTo(typeof(IAssertionTests)));

    public IEnumerable<MethodInfo> TestMethods(IEnumerable<MethodInfo> publicMethods) =>
        publicMethods.Where(m => !m.IsStatic && m.IsAssertionTest());
}

public class OtherTestDiscovery : IDiscovery
{
    public IEnumerable<Type> TestClasses(IEnumerable<Type> concreteClasses) =>
        concreteClasses.Where(c => c.Name.EndsWith("Tests"));

    public IEnumerable<MethodInfo> TestMethods(IEnumerable<MethodInfo> publicMethods) =>
        publicMethods.Where(m => !m.IsStatic && !m.IsAssertionTest() && !m.IsAssertionTestDescribe());
}

internal static class AssertionTestExtensions
{
    public static bool IsAssertionTest(this MethodInfo method)
    {
        var parameters = method.GetParameters();
        return parameters.Length == 1 && parameters[0].ParameterType == typeof(IAssertionTestCase);
    }

    public static bool IsAssertionTestDescribe(this MethodInfo method)
    {
        if (method.Name != nameof(IAssertionTests.Describe))
            return false;
        var parameters = method.GetParameters();
        return parameters.Length == 1 && parameters[0].ParameterType == typeof(IAssertionTestBuilder);
    }
}
