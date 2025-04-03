using static Asserty.ValueFormatter;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    public static void Contain(
        this IAssertionSubject<string?> subject,
        string substring,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        new StringContainAssertion(substring, comparisonType).Execute(subject);
    }
    private class StringContainAssertion(string substring, StringComparison comparisonType) : IAssertion<string?>
    {
        public bool IsVerified(string? actualValue) =>
            actualValue?.Contains(substring, comparisonType) ?? false;

        public string GetExpectationDescription() => $"to contain {Format(substring)}";

        public string GetActualDescription(string? actualValue) => actualValue is null
            ? "it was null"
            : $"{Format(actualValue)} didn't";

        public IAssertion<string?> GetNegativeAssertion() => new Negative(this);

        private class Negative(StringContainAssertion positiveAssertion) : DefaultNegativeAssertion<string?>(positiveAssertion)
        {
            public override string GetActualDescription(string? actualValue) => $"{Format(actualValue)} did";
        }
    }
}
