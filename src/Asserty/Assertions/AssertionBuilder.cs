using Asserty.Internal;

namespace Asserty.Assertions;

/// <summary>
/// Exposes the entry point for the assertion builder API.
/// </summary>
public static class AssertionBuilder
{
    /// <summary>
    /// Returns an assertion builder for the specified subject type.
    /// </summary>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion builder.</returns>
    public static IAssertionBuilder<T> For<T>() => new AssertionBuilder<T>();
}
