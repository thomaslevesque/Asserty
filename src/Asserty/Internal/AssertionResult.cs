using Asserty.Assertions;

namespace Asserty.Internal;

internal class AssertionResult<TSubject>(IAssertionSubject<TSubject> subject) : IAssertionResult<TSubject>
{
    public IPositiveAssertionSubject<TSubject> And => subject.EnsurePositive();

    public IAssertionResult<TResult> Cast<TResult>() => new CastAssertionResult<TSubject, TResult>(subject);
}
