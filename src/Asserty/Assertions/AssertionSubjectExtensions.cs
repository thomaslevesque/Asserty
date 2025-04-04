using Asserty.Internal;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
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
