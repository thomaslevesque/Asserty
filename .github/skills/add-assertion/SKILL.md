---
name: add-assertion
description: 'Provides the step-by-step workflow for adding a new built-in assertion to the Asserty library, including the implementation pattern and test structure; use when asked to add or implement a new assertion.'
---

## Overview

Asserty assertions are extension methods on `IAssertionSubject<T>` that live in the `static partial class AssertionSubjectExtensions` (namespace `Asserty`). Each assertion is built with a fluent `AssertionBuilder<T>` chain and requires no registration — the `partial class` pattern makes it automatically available after `.Should()`, `.To`, or `.Not.To`.

---

## Step 1 — Create the source file

**Location:** `src/Asserty/BuiltInAssertions/`

- If the assertion naturally groups with others for the same subject type, add it to the existing file (e.g., `StringAssertions.cs`, `CollectionAssertions.cs`, `BooleanAssertions.cs`). Note that the assertion methods still belong to the `AssertionSubjectExtensions` class, whatever the file name is.
- Otherwise create a new file named after the assertion: `YourAssertionName.cs`.
- Assertion methods typically start with a verb in the infinitive form (e.g. "Have", "Be", "Contain", etc.), because they need to be chained behind either `.Should()` or `Expect(...).To`. The point is to make the API read like natural language.

```csharp
// src/Asserty/BuiltInAssertions/YourAssertionName.cs
using Asserty.Assertions;

namespace Asserty;

public static partial class AssertionSubjectExtensions
{
    /// <summary>Asserts that the subject …</summary>
    public static IAssertionResult<YourType> YourAssertionName(
        this IAssertionSubject<YourType> subject /*, additional parameters if needed */)
    {
        var assertion = AssertionBuilder.For<YourType>()
            .Verify(value => /* bool: true = passes */)
            .ExpectValue("to …")                           // phrase starting with "to"
            .DescribeActual(value => $"…")                 // description of the actual value when the assertion fails
            .DescribeActualWhenNegated(value => $"…");     // description of the actual value when the negated assertion fails (i.e. .Should().Not...)
        return subject.Verify(assertion);
    }
}
```

**Notes:**
- `Format()` is available in every source file without an import (global static using in `GlobalUsings.cs`).
- If you need to share a computed value between `Verify` and `DescribeActual` (e.g., to avoid evaluating an enumerable twice), use the `(value, context)` overloads and `context.Set("key", computedValue)` / `context.Get<T>("key")`. See `CollectionAssertions.cs` → `HaveCount` for a concrete example.

---

## Step 2 — Understand the failure message format

The framework always produces:

```
Expected `{callerExpression}` [not] {ExpectValue}, but {DescribeActual[WhenNegated]}.
```

For example, `BeTrue` on a `false` value produces:
```
Expected `value` to be true, but it is false.
```

And its negation (`Not.BeTrue`) on a `true` value:
```
Expected `value` not to be true, but it is.
```

Keep this format in mind when writing test expectations.

---

## Step 3 — Create the test file

**Location:** `tests/Asserty.Tests/YourAssertionNameTests.cs`

Write one test file per assertion method group (i.e. overloads of the same assertion method can share the same test file), even if the implementations are grouped in a single file.

Use nested classes to describe the actual value.

```csharp
namespace Asserty.Tests;

public static class YourAssertionNameTests
{
    public class WhenValue{MatchesCondition}
    {
        [Fact]
        public void YourAssertionName_Should_Pass()
        {
            var value = /* a value that satisfies the assertion */;
            Verify.That(() => value.Should().YourAssertionName()).Passes();
        }

        [Fact]
        public void Not_YourAssertionName_Should_Fail()
        {
            var value = /* a value that satisfies the assertion */;
            Verify.That(() => value.Should().Not.YourAssertionName())
                .Fails("Expected `value` not to …, but ….");
        }
    }

    public class WhenValue{DoesNotMatchCondition}
    {
        [Fact]
        public void YourAssertionName_Should_Fail()
        {
            var value = /* a value that does NOT satisfy the assertion */;
            Verify.That(() => value.Should().YourAssertionName())
                .Fails("Expected `value` to …, but ….");
        }

        [Fact]
        public void Not_YourAssertionName_Should_Pass()
        {
            var value = /* a value that does NOT satisfy the assertion */;
            Verify.That(() => value.Should().Not.YourAssertionName()).Passes();
        }
    }

    // Add a WhenValueIsNull nested class if the subject type is nullable.
}
```

**Test helpers:**
- `Verify.That(lambda).Passes()` — asserts the assertion succeeds.
- `Verify.That(lambda).Fails("exact message")` — asserts an `AssertionException` is thrown with that **exact** message (including the trailing period).

---

## Step 4 — Run the tests

```
dotnet test
```

All existing tests must remain green. Add more `WhenValue…` nested classes for any additional edge cases (null inputs, boundary values, etc.).

---

## Quick reference: real-world examples

| Assertion | Subject type | File |
|---|---|---|
| `BeTrue` / `BeFalse` | `bool` | `BooleanAssertions.cs` |
| `BeNull` | `object?` | `BeNull.cs` |
| `BeEqualTo` | `T` | `BeEqualTo.cs` |
| `BeEmpty`, `HaveCount`, `HaveSameElementsAs` | `IEnumerable<T>?` | `CollectionAssertions.cs` |
| `Contain`, `StartWith`, `EndWith`, `HaveLength` | `string?` | `StringAssertions.cs` |
