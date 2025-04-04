using Asserty.Internal;

namespace Asserty.Assertions;

public static class AssertionBuilder
{
    public static IAssertionBuilder<T> For<T>() => new AssertionBuilder<T>();
}
