using Asserty.Internal;

namespace Asserty.Assertions;

public interface IAssertionBuilder<T> : IHideObjectMembers
{
    IExpectValueStep Verify(Func<T, bool> predicate);

    #region Fluent API interfaces

    public interface IFinalStep : IHideObjectMembers
    {
        IAssertion<T> Build();
    }

    public interface IExpectValueStep : IHideObjectMembers
    {
        IExpectValueWhenNegatedStep ExpectValue(string expectationDescription);
    }

    public interface IExpectValueWhenNegatedStep : IDescribeActualStep
    {
        IDescribeActualStep ExpectValueWhenNegated(string negativeExpectationDescription);
    }

    public interface IDescribeActualStep : IHideObjectMembers
    {
        IIDescribeActualWhenNegatedStep DescribeActual(Func<T, string> actualDescriptionFactory);
    }

    public interface IIDescribeActualWhenNegatedStep : IFinalStep
    {
        IFinalStep DescribeActualWhenNegated(Func<T, string> negativeActualDescriptionFactory);
    }

    #endregion
}
