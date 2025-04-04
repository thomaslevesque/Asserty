using Asserty.Assertions;
using static Asserty.Assertions.AssertionValueFormatter;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    public static IAssertionResult<T> BeNull<T>(this IAssertionSubject<T> subject)
        where T : class?
    {
        var assertion = AssertionBuilder.For<T>()
            .Verify(value => value is null)
            .ExpectValue("to be null")
            .DescribeActual(actualValue => $"it is actually {Format(actualValue)}")
            .DescribeActualWhenNegated(_ => "it is actually null");
        return subject.Verify(assertion);
    }
}
