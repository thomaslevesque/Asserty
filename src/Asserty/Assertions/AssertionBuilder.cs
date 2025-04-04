using Asserty.Internal;

namespace Asserty;

public static class AssertionBuilder
{
    public static IAssertionBuilder<T> For<T>() => new AssertionBuilder<T>();
}
