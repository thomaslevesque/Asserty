# Built-in assertions

Asserty's built-in assertions are extension methods available with both the `Should()` and `Expect(...).To` API flavors.
Assertions can also be negated by inserting `.Not` before the assertion, for example `value.Should().Not.BeNull()`.

## Values

| Assertion | Subject | Description |
|---|---|---|
| `BeTrue()` | `bool` | Asserts that the value is `true`. |
| `BeFalse()` | `bool` | Asserts that the value is `false`. |
| `BeNull()` | Reference or nullable value type | Asserts that the value is `null`. |
| `BeEqualTo(expectedValue, equalityComparer?)` | Any `T` | Asserts equality with the expected value. Uses `EqualityComparer<T>.Default` unless a comparer is provided. |
| `BeSameInstanceAs(expected)` | Reference type | Asserts that the value is the exact same object instance as `expected`. |

## Comparisons

The comparison assertions require `T : IComparable<T>`. They use `Comparer<T>.Default` unless an `IComparer<T>` is
provided.

| Assertion | Description |
|---|---|
| `BeGreaterThan(otherValue, comparer?)` | The value is greater than `otherValue`. |
| `BeGreaterThanOrEqualTo(otherValue, comparer?)` | The value is greater than or equal to `otherValue`. |
| `BeLessThan(otherValue, comparer?)` | The value is less than `otherValue`. |
| `BeLessThanOrEqualTo(otherValue, comparer?)` | The value is less than or equal to `otherValue`. |
| `BeEqualToWithin(otherValue, tolerance)` | For numeric `T`, the absolute difference from `otherValue` is at most `tolerance`. |

## Strings

String assertions accept a nullable string. A `null` value does not satisfy these assertions. The substring comparisons
use `StringComparison.Ordinal` by default.

| Assertion | Description |
|---|---|
| `Contain(substring, comparisonType?)` | The string contains `substring`. |
| `StartWith(prefix, comparisonType?)` | The string starts with `prefix`. |
| `EndWith(suffix, comparisonType?)` | The string ends with `suffix`. |
| `HaveLength(length)` | The string has the specified number of characters. |

## Collections

Collection assertions operate on `IEnumerable<T>?`. A `null` collection does not satisfy them. Equality-based
assertions use `EqualityComparer<T>.Default` unless an `IEqualityComparer<T>` is supplied.

| Assertion | Description |
|---|---|
| `BeEmpty()` | The collection has no elements. |
| `HaveCount(count)` | The collection contains exactly `count` elements. |
| `HaveAtLeast(count)` | The collection contains at least `count` elements. |
| `HaveAtMost(count)` | The collection contains at most `count` elements. |
| `HaveSameElementsAs(expectedCollection, equalityComparer?)` | The collection contains the same elements as the expected collection, regardless of order. Duplicate elements must occur with the same multiplicity. |
| `BeSameSequenceAs(expectedSequence, equalityComparer?)` | The collection contains the same elements as the expected sequence in the same order. |
| `Contain(expectedElement, equalityComparer?)` | The collection contains the specified element. |
| `Contain(predicate)` | At least one element matches the predicate. |
| `AllMatch(predicate)` | Every element matches the predicate. An empty collection satisfies this assertion. |

The expected collection/sequence and predicates must not be `null`; passing `null` throws `ArgumentNullException`.

## Type assertions

Type assertions operate on an `object?` subject.

| Assertion | Description |
|---|---|
| `BeOfType<TExpected>()` | The value's runtime type is exactly `TExpected`; derived types do not match. |
| `BeAssignableTo<TExpected>()` | The value is an instance of `TExpected` or a type derived from it. |

## Combining assertions

`Either` and `Neither` take one or more assertion callbacks for the same subject:

```csharp
value.Should().Either(
    subject => subject.BeEqualTo("first"),
    subject => subject.BeEqualTo("second"));

value.Should().Neither(
    subject => subject.BeEqualTo("first"),
    subject => subject.BeEqualTo("second"));
```

`Either` passes when at least one callback passes; `Neither` passes when all callbacks fail. Both throw
`ArgumentException` if called without callbacks. When the combined assertion fails, its exception includes the
individual assertion failure messages.
