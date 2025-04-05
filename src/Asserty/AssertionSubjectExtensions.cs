using Asserty.Assertions;
using Asserty.Internal;

namespace Asserty;

/// <summary>
/// Exposes assertions methods.
/// </summary>
public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Verifies an assertion against the subject. This is a convenience method to treat an assertion builder in the
    /// final stage as an assertion, skipping the <see cref="IAssertionBuilder{T}.IFinalStep.Build"/> step.
    /// </summary>
    /// <param name="subject">The subject against which the assertion will be verified.</param>
    /// <param name="builder">The assertion builder's final step.</param>
    /// <typeparam name="T">The type of the subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    /// <remarks>This method is not typically called when writing tests, but can be used to extend Asserty with new assertion types.</remarks>
    public static IAssertionResult<T> Verify<T>(this IAssertionSubject<T> subject, IAssertionBuilder<T>.IFinalStep builder)
    {
        return subject.Verify(builder.Build());
    }

    internal static IPositiveAssertionSubject<T> EnsurePositive<T>(this IAssertionSubject<T> subject)
    {
        return subject as IPositiveAssertionSubject<T>
               ?? new PositiveAssertionSubject<T>(subject.Value, subject.Expression);
    }
}
