using Asserty.Internal;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    internal static IPositiveAssertionSubject<T> EnsurePositive<T>(this IAssertionSubject<T> subject)
    {
        return subject as IPositiveAssertionSubject<T>
               ?? new PositiveAssertionSubject<T>(subject.Value, subject.Expression);
    }
}
