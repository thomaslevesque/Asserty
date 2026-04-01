using System.Runtime.CompilerServices;
using Asserty.Assertions;
using Asserty.Internal;

namespace Asserty;

/// <summary>
/// The entry point for the expectation-style API. Typically imported statically like this:
/// <code>
/// using static Asserty.Expectations;
/// </code>
/// </summary>
public static class Expectations
{
    /// <summary>
    /// Returns an expectation for the specified value. The expectation can be used to assert that the value meets, or
    /// does not meet, certain conditions.
    /// </summary>
    /// <param name="value">The value for which an expectation is created.</param>
    /// <param name="expression">The expression used in code to represent the value. Note: don't specify an explicit
    /// value for this parameter, it will be provided automatically by the compiler (requires C# 10 or later).</param>
    /// <typeparam name="TSubject">The type of the expectation subject's value.</typeparam>
    /// <returns>An expectation for the specified value.</returns>
    public static IPositiveExpectation<TSubject> Expect<TSubject>(TSubject value, [CallerArgumentExpression(nameof(value))] string expression = null!)
    {
        return new PositiveExpectation<TSubject>(value, expression);
    }

    /// <summary>
    /// Represents a positive expectation for a value. A positive expectation can be used to assert that the value meets
    /// certain conditions.
    /// </summary>
    /// <typeparam name="TSubject">The type of the expectation subject's value.</typeparam>
    public interface IPositiveExpectation<TSubject>
    {
        /// <summary>
        /// Returns an assertion subject for the expectation's value. The assertion subject can be used to assert that
        /// the value meets certain conditions.
        /// </summary>
        IPositiveAssertionSubject<TSubject> To { get; }

        /// <summary>
        /// Returns a negative expectation for the expectation's value. The negative expectation can be used to assert
        /// that the value does not meet certain conditions.
        /// </summary>
        INegativeExpectation<TSubject> Not { get; }
    }

    /// <summary>
    /// Represents a negative expectation for a value. A negative expectation can be used to assert that the value does
    /// not meet certain conditions.
    /// </summary>
    /// <typeparam name="TSubject">The type of the expectation subject's value.</typeparam>
    public interface INegativeExpectation<TSubject>
    {
        /// <summary>
        /// Returns an assertion subject for the expectation's value. The assertion subject can be used to assert that
        /// the value does not meet certain conditions.
        /// </summary>
        INegativeAssertionSubject<TSubject> To { get; }
    }

    private class PositiveExpectation<TSubject>(TSubject value, string expression) : IPositiveExpectation<TSubject>
    {
        public IPositiveAssertionSubject<TSubject> To => AssertionSubjectFactory.CreatePositive(value, expression);
        public INegativeExpectation<TSubject> Not => new NegativeExpectation<TSubject>(value, expression);
    }

    private class NegativeExpectation<TSubject>(TSubject value, string expression) : INegativeExpectation<TSubject>
    {
        public INegativeAssertionSubject<TSubject> To => AssertionSubjectFactory.CreateNegative(value, expression);
    }
}
