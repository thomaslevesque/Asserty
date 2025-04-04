using Asserty.Assertions;

namespace Asserty.Internal;

internal class AssertionBuilder<T> :
    IAssertionBuilder<T>,
    IAssertionBuilder<T>.IExpectValueStep,
    IAssertionBuilder<T>.IExpectValueWhenNegatedStep,
    IAssertionBuilder<T>.IDescribeActualStep,
    IAssertionBuilder<T>.IIDescribeActualWhenNegatedStep,
    IAssertionBuilder<T>.IFinalStep
{
    private Func<T, bool>? _predicate;
    private string? _expectationDescription;
    private string? _negativeExpectationDescription;
    private Func<T, string>? _actualDescriptionFactory;
    private Func<T, string>? _negativeActualDescriptionFactory;

    public IAssertionBuilder<T>.IExpectValueStep Verify(Func<T, bool> predicate)
    {
        _predicate = predicate;
        return this;
    }

    public IAssertionBuilder<T>.IExpectValueWhenNegatedStep ExpectValue(string expectationDescription)
    {
        _expectationDescription = expectationDescription;
        return this;
    }

    public IAssertionBuilder<T>.IDescribeActualStep ExpectValueWhenNegated(string negativeExpectationDescription)
    {
        _negativeExpectationDescription = negativeExpectationDescription;
        return this;
    }

    public IAssertionBuilder<T>.IIDescribeActualWhenNegatedStep DescribeActual(Func<T, string> actualDescriptionFactory)
    {
        _actualDescriptionFactory = actualDescriptionFactory;
        return this;
    }

    public IAssertionBuilder<T>.IFinalStep DescribeActualWhenNegated(Func<T, string> negativeActualDescriptionFactory)
    {
        _negativeActualDescriptionFactory = negativeActualDescriptionFactory;
        return this;
    }

    public IAssertion<T> Build()
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
        Func<T, bool> predicate,
        string expectationDescription,
        Func<T, string> actualDescriptionFactory,
        string? negativeExpectationDescription,
        Func<T, string>? negativeActualDescriptionFactory
    ) : IAssertion<T>
    {
        public bool IsVerified(T actualValue) => predicate(actualValue);

        public string GetExpectationDescription() => expectationDescription;

        public string GetActualDescription(T actualValue) => actualDescriptionFactory(actualValue);

        public IAssertion<T> GetNegativeAssertion() =>
            new Negative(this, negativeExpectationDescription, negativeActualDescriptionFactory);

        private class Negative(
            IAssertion<T> positiveAssertion,
            string? negativeExpectationDescription,
            Func<T, string>? negativeActualDescriptionFactory
        ) : DefaultNegativeAssertion<T>(positiveAssertion)
        {
            public override string GetExpectationDescription() =>
                negativeExpectationDescription ?? base.GetExpectationDescription();

            public override string GetActualDescription(T actualValue) =>
                negativeActualDescriptionFactory?.Invoke(actualValue) ?? base.GetActualDescription(actualValue);
        }
    }
}
