﻿using Asserty.Assertions;
using static Asserty.Assertions.AssertionValueFormatter;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>
    /// Asserts that the subject's value is an empty collection.
    /// </summary>
    /// <param name="subject">The subject of the assertion</param>
    /// <typeparam name="T">The type of the assertion subject's value.</typeparam>
    /// <returns>An assertion result that can be used to chain other assertions, if successful.</returns>
    /// <exception cref="AssertionException">The assertion failed.</exception>
    public static IAssertionResult<IEnumerable<T>> BeEmpty<T>(this IAssertionSubject<IEnumerable<T>> subject)
    {
        var assertion = AssertionBuilder.For<IEnumerable<T>>()
            .Verify(value => !value.Any())
            .ExpectValue("to be empty")
            .DescribeActual(value =>
            {
                int count = value.Count();
                var elements = count > 1 ? "elements" : "element";
                return $"{Format(value)} contains {count} {elements}";
            })
            .DescribeActualWhenNegated(_ => "it is");
        return subject.Verify(assertion);
    }
}
