using Asserty.Assertions;

namespace Asserty.Internal;

internal class CastAssertionResult<TOriginal, TResult>(IAssertionSubject<TOriginal> originalSubject) : IAssertionResult<TResult>
{
    public IPositiveAssertionSubject<TResult> And => originalSubject.Cast<TResult>().EnsurePositive();

    public IAssertionResult<TOther> Cast<TOther>() => new CastAssertionResult<TOriginal, TOther>(originalSubject);
}
