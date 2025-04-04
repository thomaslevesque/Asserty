using Asserty.Assertions;

namespace Asserty.Internal;

internal class AssertionResult<T>(IAssertionSubject<T> subject) : IAssertionResult<T>
{
    public IPositiveAssertionSubject<T> And => subject.EnsurePositive();

    public IAssertionResult<TResult> Cast<TResult>() => new CastAssertionResult<T, TResult>(subject);
}
