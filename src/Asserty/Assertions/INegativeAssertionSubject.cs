namespace Asserty.Assertions;

/// <summary>
/// Represents the subject of a negated assertion (e.g. `value.Should().Not.BeEqualTo(42)`)
/// </summary>
/// <typeparam name="TSubject">The type of the subject's value.</typeparam>
public interface INegativeAssertionSubject<out TSubject> : IAssertionSubject<TSubject>;
