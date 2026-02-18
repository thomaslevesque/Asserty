using Asserty.Assertions;

namespace Asserty.Internal;

internal class AssertionBuilder<TSubject> :
    IAssertionBuilder<TSubject>,
    IAssertionBuilder<TSubject>.IExpectValueStep,
    IAssertionBuilder<TSubject>.IExpectValueWhenNegatedStep,
    IAssertionBuilder<TSubject>.IDescribeActualStep,
    IAssertionBuilder<TSubject>.IIDescribeActualWhenNegatedStep,
    IAssertionBuilder<TSubject>.IFinalStep
{
    private Func<TSubject, AssertionEvaluationContext, bool>? _predicate;
    private string? _expectationDescription;
    private string? _negativeExpectationDescription;
    private Func<TSubject, AssertionEvaluationContext, string>? _actualDescriptionFactory;
    private Func<TSubject, AssertionEvaluationContext, string>? _negativeActualDescriptionFactory;

    public IAssertionBuilder<TSubject>.IExpectValueStep Verify(Func<TSubject, AssertionEvaluationContext, bool> predicate)
    {
        _predicate = predicate;
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

    public IAssertionBuilder<TSubject>.IIDescribeActualWhenNegatedStep DescribeActual(Func<TSubject, AssertionEvaluationContext, string> actualDescriptionFactory)
    {
        _actualDescriptionFactory = actualDescriptionFactory;
        return this;
    }

    public IAssertionBuilder<TSubject>.IFinalStep DescribeActualWhenNegated(Func<TSubject, AssertionEvaluationContext, string> negativeActualDescriptionFactory)
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
            _negativeExpectationDescription,
            _negativeActualDescriptionFactory);
    }

    private class BuiltAssertion(
        Func<TSubject, AssertionEvaluationContext, bool> predicate,
        string expectationDescription,
        Func<TSubject, AssertionEvaluationContext, string> actualDescriptionFactory,
        string? negativeExpectationDescription,
        Func<TSubject, AssertionEvaluationContext, string>? negativeActualDescriptionFactory
    ) : IAssertion<TSubject>
    {
        public bool IsVerified(TSubject actualValue, AssertionEvaluationContext context) =>
            predicate(actualValue, context);

        public string GetExpectationDescription() => expectationDescription;

        public string GetActualDescription(TSubject actualValue, AssertionEvaluationContext context) =>
            actualDescriptionFactory(actualValue, context);

        public IAssertion<TSubject> GetNegativeAssertion() =>
            new Negative(this, negativeExpectationDescription, negativeActualDescriptionFactory);

        private class Negative(
            IAssertion<TSubject> positiveAssertion,
            string? negativeExpectationDescription,
            Func<TSubject, AssertionEvaluationContext, string>? negativeActualDescriptionFactory
        ) : DefaultNegativeAssertion<TSubject>(positiveAssertion)
        {
            public override string GetExpectationDescription() =>
                negativeExpectationDescription ?? base.GetExpectationDescription();

            public override string GetActualDescription(TSubject actualValue, AssertionEvaluationContext context) =>
                negativeActualDescriptionFactory?.Invoke(actualValue, context) ??
                base.GetActualDescription(actualValue, context);
        }
    }
}
