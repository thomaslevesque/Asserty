namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    public static IAssertionResult<TExpected> BeOfType<TExpected>(this IAssertionSubject<object?> subject)
    {
        return subject.Verify(new BeOfTypeAssertion<TExpected>()).Cast<TExpected>();
    }

    private class BeOfTypeAssertion<TExpected> : IAssertion<object?>
    {
        public bool IsVerified(object? actualValue) => actualValue?.GetType() == typeof(TExpected);

        public string GetExpectationDescription() => $"to be of type `{typeof(TExpected)}`";

        public string GetActualDescription(object? actualValue)
        {
            if (actualValue is null)
                return "it is actually null";
            return $"it is actually of type `{actualValue.GetType()}`";
        }

        public IAssertion<object?> GetNegativeAssertion() => new Negative(this);

        private class Negative(BeOfTypeAssertion<TExpected> positiveAssertion) : DefaultNegativeAssertion<object?>(positiveAssertion)
        {
            public override string GetActualDescription(object? actualValue) => "it is actually of that type";
        }
    }
}
