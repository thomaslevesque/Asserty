using static Asserty.ValueFormatter;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    public static void BeEqualTo<T>(this IAssertionSubject<T> subject, T expectedValue, IEqualityComparer<T>? equalityComparer = null)
    {
        new BeEqualAssertion<T>(expectedValue, equalityComparer).Execute(subject);
    }

    internal class BeEqualAssertion<T>(T expectedValue, IEqualityComparer<T>? equalityComparer) : IAssertion<T>
    {
        public bool IsVerified(T actualValue)
        {
            var actualComparer = equalityComparer ?? EqualityComparer<T>.Default;
            return actualComparer.Equals(expectedValue, actualValue);
        }

        public string GetExpectationDescription() => $"to be equal to {Format(expectedValue)}";

        public string GetActualDescription(T actualValue) => $"it is actually {Format(actualValue)}";

        public IAssertion<T> GetNegativeAssertion() => new Negative(this);

        private class Negative(BeEqualAssertion<T> positiveAssertion) : DefaultNegativeAssertion<T>(positiveAssertion)
        {
            public override string GetActualDescription(T actualValue) => "it is actually equal";
        }
    }
}
