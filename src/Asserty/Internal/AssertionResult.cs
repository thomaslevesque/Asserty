namespace Asserty.Internal;

internal class AssertionResult<T>(IAssertionSubject<T> subject) : IAssertionResult<T>
{
    public IPositiveAssertionSubject<T> And => EnsurePositive(subject);

    private static IPositiveAssertionSubject<T> EnsurePositive(IAssertionSubject<T> subject)
    {
        return subject as IPositiveAssertionSubject<T>
               ?? new PositiveAssertionSubject<T>(subject.Value, subject.Expression);
    }
}
