using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    public static IAssertionResult<TExpected> BeAssignableTo<TExpected>(this IAssertionSubject<object?> subject)
    {
        return subject.Verify(new BeAssignableToAssertion<TExpected>()).Cast<TExpected>();
    }

    private class BeAssignableToAssertion<TExpected> : IAssertion<object?>
    {
        public bool IsVerified(object? actualValue) => actualValue is TExpected;

        public string GetExpectationDescription() => $"to be assignable to `{typeof(TExpected)}`";

        public string GetActualDescription(object? actualValue)
        {
            if (actualValue is null)
                return "it is actually null";
            return $"it is actually of type `{actualValue.GetType()}`, which is not assignable to `{typeof(TExpected)}`";
        }

        public IAssertion<object?> GetNegativeAssertion() => new Negative(this);

        private class Negative(IAssertion<object?> positiveAssertion) : DefaultNegativeAssertion<object?>(positiveAssertion)
        {
            public override string GetActualDescription(object? actualValue)
            {
                var actualType = actualValue?.GetType();
                if (actualType == typeof(TExpected))
                    return "it is actually of that type";
                return $"it is actually of type `{actualType}`, which is assignable to `{typeof(TExpected)}`";
            }
        }
    }
}
