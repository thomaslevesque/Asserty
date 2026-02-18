using Asserty.Internal;

namespace Asserty.Assertions;

/// <summary>
/// Provides a way to fluently define a new type of assertion.
/// </summary>
/// <typeparam name="TSubject">The type of the assertion subject's value.</typeparam>
public interface IAssertionBuilder<TSubject> : IHideObjectMembers
{
    /// <summary>
    /// Specifies the predicate for the assertion.
    /// </summary>
    /// <param name="predicate">The predicate that verifies if the value verifies the assertion.</param>
    /// <returns>The next step of the assertion definition.</returns>
    IExpectValueStep Verify(Func<TSubject, bool> predicate) => Verify((value, _) => predicate(value));

    /// <summary>
    /// Specifies the predicate for the assertion.
    /// </summary>
    /// <param name="predicate">The predicate that verifies if the value verifies the assertion.</param>
    /// <returns>The next step of the assertion definition.</returns>
    IExpectValueStep Verify(Func<TSubject, AssertionEvaluationContext, bool> predicate);

    #region Fluent API interfaces

    /// <summary>
    /// Represents the final step of the assertion definition.
    /// </summary>
    public interface IFinalStep : IHideObjectMembers
    {
        /// <summary>
        /// Builds the assertion being defined.
        /// </summary>
        /// <returns>An assertion.</returns>
        IAssertion<TSubject> Build();
    }

    /// <summary>
    /// Represents the step of the assertion definition where the expectation description is specified.
    /// </summary>
    public interface IExpectValueStep : IHideObjectMembers
    {
        /// <summary>
        /// Specifies the part of the assertion failure message describing what is expected, following "Expected
        /// {expression}…", starting with "to" (e.g. "to be equal to 42" or "to contain 3 elements").
        /// </summary>
        /// <param name="expectationDescription">The part of the assertion failure message describing what is expected.</param>
        /// <returns>The next step of the assertion definition.</returns>
        IExpectValueWhenNegatedStep ExpectValue(string expectationDescription);
    }

    /// <summary>
    /// Represents the step of the assertion definition where the expectation description for a negated assertion is
    /// specified.
    /// </summary>
    public interface IExpectValueWhenNegatedStep : IDescribeActualStep
    {
        /// <summary>
        /// Specifies the part of the assertion failure message describing what is expected for a negated assertion,
        /// following "Expected {expression}…", starting with "not to" (e.g. "not to be equal to 42" or "not to contain
        /// 3 elements").
        /// </summary>
        /// <param name="negativeExpectationDescription">The part of the assertion failure message describing what is
        /// expected for a negated assertion.</param>
        /// <returns>The next step of the assertion definition.</returns>
        /// <remarks>This step is optional. By default, the positive expectation description will be used, prefixed
        /// with "not".</remarks>
        IDescribeActualStep ExpectValueWhenNegated(string negativeExpectationDescription);
    }

    /// <summary>
    /// Represents the step of the assertion definition where the actual value description is specified.
    /// </summary>
    public interface IDescribeActualStep : IHideObjectMembers
    {
        /// <summary>
        /// Specifies the part of the assertion failure message describing what was actually observed, following "but…"
        /// (e.g. "it is actually 0" or "it actually contains 1 element").
        /// </summary>
        /// <param name="actualDescriptionFactory">The part of the assertion failure message describing what was
        /// actually observed.</param>
        /// <returns>The next step of the assertion definition.</returns>
        IIDescribeActualWhenNegatedStep DescribeActual(Func<TSubject, string> actualDescriptionFactory) =>
            DescribeActual((actualValue, _) => actualDescriptionFactory(actualValue));

        /// <summary>
        /// Specifies the part of the assertion failure message describing what was actually observed, following "but…"
        /// (e.g. "it is actually 0" or "it actually contains 1 element").
        /// </summary>
        /// <param name="actualDescriptionFactory">The part of the assertion failure message describing what was
        /// actually observed.</param>
        /// <returns>The next step of the assertion definition.</returns>
        IIDescribeActualWhenNegatedStep DescribeActual(Func<TSubject, AssertionEvaluationContext, string> actualDescriptionFactory);
    }

    /// <summary>
    /// Represents the step of the assertion definition where the actual value description for a negated assertion is
    /// specified.
    /// </summary>
    public interface IIDescribeActualWhenNegatedStep : IFinalStep
    {
        /// <summary>
        /// Specifies the part of the assertion failure message describing what was actually observed for a negated
        /// assertion, following "but…" (e.g. "it is actually 0" or "it actually contains 1 element").
        /// </summary>
        /// <param name="negativeActualDescriptionFactory">The part of the assertion failure message describing what was
        /// actually observed.</param>
        /// <returns>The next step of the assertion definition.</returns>
        /// <remarks>This step is optional but recommended. By default, the positive description will be used, which
        /// might work in some cases, but probably not most.</remarks>
        IFinalStep DescribeActualWhenNegated(Func<TSubject, string> negativeActualDescriptionFactory) =>
            DescribeActualWhenNegated((actualValue, _) => negativeActualDescriptionFactory(actualValue));

        /// <summary>
        /// Specifies the part of the assertion failure message describing what was actually observed for a negated
        /// assertion, following "but…" (e.g. "it is actually 0" or "it actually contains 1 element").
        /// </summary>
        /// <param name="negativeActualDescriptionFactory">The part of the assertion failure message describing what was
        /// actually observed.</param>
        /// <returns>The next step of the assertion definition.</returns>
        /// <remarks>This step is optional but recommended. By default, the positive description will be used, which
        /// might work in some cases, but probably not most.</remarks>
        IFinalStep DescribeActualWhenNegated(Func<TSubject, AssertionEvaluationContext, string> negativeActualDescriptionFactory);
    }

    #endregion
}
