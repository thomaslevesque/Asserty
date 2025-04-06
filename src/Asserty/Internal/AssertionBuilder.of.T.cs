using Asserty.Assertions;

namespace Asserty.Internal;

internal class AssertionBuilder<TSubject> :
    IAssertionBuilder<TSubject>,
    IAssertionBuilder<TSubject>.IWithPreconditionStep,
    IAssertionBuilder<TSubject>.IExpectValueStep,
    IAssertionBuilder<TSubject>.IExpectValueWhenNegatedStep,
    IAssertionBuilder<TSubject>.IDescribeActualStep,
    IAssertionBuilder<TSubject>.IIDescribeActualWhenNegatedStep,
    IAssertionBuilder<TSubject>.IFinalStep
{
    private Func<TSubject, bool>? _precondition;
    private Func<TSubject, string?>? _preconditionFailureDescriptionFactory;
    private Func<TSubject, bool>? _predicate;
    private string? _expectationDescription;
    private string? _negativeExpectationDescription;
    private Func<TSubject, string>? _actualDescriptionFactory;
    private Func<TSubject, string>? _negativeActualDescriptionFactory;

    public IAssertionBuilder<TSubject>.IWithPreconditionStep Verify(Func<TSubject, bool> predicate)
    {
        _predicate = predicate;
        return this;
    }

    public IAssertionBuilder<TSubject>.IExpectValueStep WithPrecondition(
        Func<TSubject, bool> precondition,
        Func<TSubject, string?>? failureDescriptionFactory = null)
    {
        _precondition = precondition;
        _preconditionFailureDescriptionFactory = failureDescriptionFactory;
        return this;
    }

    public IAssertionBuilder<TSubject>.IExpectValueWhenNegatedStep ExpectValue(string expectationDescription)
    {
        _expectationDescription = expectationDescription;
        return this;
    }

    public IAssertionBuilder<TSubject>.IDescribeActualStep ExpectValueWhenNegated(string negativeExpectationDescription)
    {
        _negativeExpectationDescription = negativeExpectationDescription;
        return this;
    }

    public IAssertionBuilder<TSubject>.IIDescribeActualWhenNegatedStep DescribeActual(Func<TSubject, string> actualDescriptionFactory)
    {
        _actualDescriptionFactory = actualDescriptionFactory;
        return this;
    }

    public IAssertionBuilder<TSubject>.IFinalStep DescribeActualWhenNegated(Func<TSubject, string> negativeActualDescriptionFactory)
    {
        _negativeActualDescriptionFactory = negativeActualDescriptionFactory;
        return this;
    }

    public IAssertion<TSubject> Build()
    {
        if (_predicate is null)
            throw new InvalidOperationException("Predicate is not set");
        if (_expectationDescription is null)
            throw new InvalidOperationException("Expectation description is not set");
        if (_actualDescriptionFactory is null)
            throw new InvalidOperationException("Actual description factory is not set");

        return new BuiltAssertion(
            _predicate,
            _expectationDescription,
            _actualDescriptionFactory,
            _precondition,
            _preconditionFailureDescriptionFactory,
            _negativeExpectationDescription,
            _negativeActualDescriptionFactory);
    }

    private class BuiltAssertion(
        Func<TSubject, bool> predicate,
        string expectationDescription,
        Func<TSubject, string> actualDescriptionFactory,
        Func<TSubject, bool>? precondition,
        Func<TSubject, string?>? preconditionFailureDescriptionFactory,
        string? negativeExpectationDescription,
        Func<TSubject, string>? negativeActualDescriptionFactory
    ) : IAssertion<TSubject>
    {
        public bool IsPreconditionVerified(TSubject actualValue) => precondition?.Invoke(actualValue) ?? true;

        public string? GetPreconditionFailureDescription(TSubject actualValue) =>
            preconditionFailureDescriptionFactory?.Invoke(actualValue);

        public bool IsVerified(TSubject actualValue) => predicate(actualValue);

        public string GetExpectationDescription() => expectationDescription;

        public string GetActualDescription(TSubject actualValue) => actualDescriptionFactory(actualValue);

        public IAssertion<TSubject> GetNegativeAssertion() =>
            new Negative(this, negativeExpectationDescription, negativeActualDescriptionFactory);

        private class Negative(
            IAssertion<TSubject> positiveAssertion,
            string? negativeExpectationDescription,
            Func<TSubject, string>? negativeActualDescriptionFactory
        ) : DefaultNegativeAssertion<TSubject>(positiveAssertion)
        {
            public override string GetExpectationDescription() =>
                negativeExpectationDescription ?? base.GetExpectationDescription();

            public override string GetActualDescription(TSubject actualValue) =>
                negativeActualDescriptionFactory?.Invoke(actualValue) ?? base.GetActualDescription(actualValue);
        }
    }
}
