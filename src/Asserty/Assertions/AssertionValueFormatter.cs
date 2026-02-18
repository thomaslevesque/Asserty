using System.Buffers;
using System.Collections;
using System.Text;

namespace Asserty.Assertions;

/// <summary>
/// Exposes a method to format values in assertion failure messages.
/// </summary>
public static class AssertionValueFormatter
{
    /// <summary>
    /// Format values to be included in assertion failure messages.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>A string representation of the value, suitable for including it in an assertion failure message.</returns>
    public static string Format(object? value) => value switch
    {
        null => "(null)",
        string s => FormatStringValue(s),
        IEnumerable collection => FormatCollection(collection),
        _ => value.ToString() ?? ""
    };

    private static string FormatStringValue(string value) => $"\"{value.EscapeSpecialCharacters()}\"";

    private static readonly SearchValues<char> CharactersToEscape = SearchValues.Create("\\\"\n\r\t\a\b\e\f\v\0");

    private static string EscapeSpecialCharacters(this string input)
    {
        if (input.AsSpan().IndexOfAny(CharactersToEscape) < 0)
            return input;

        var builder = new StringBuilder(input.Length);
        foreach (var c in input)
        {
            var substitution = c switch
            {
                '\\' => @"\\",
                '\"' => @"\""",
                '\n' => @"\n",
                '\r' => @"\r",
                '\t' => @"\t",
                '\a' => @"\a",
                '\b' => @"\b",
                '\e' => @"\e",
                '\f' => @"\f",
                '\0' => @"\0",
                _ => null
            };
            if (substitution is null)
            {
                builder.Append(c);
            }
            else
            {
                builder.Append(substitution);
            }
        }
        return builder.ToString();
    }

    private static string FormatCollection(IEnumerable collection)
    {
        var builder = new StringBuilder(32);
        builder.Append('[');

        var items = collection.Cast<object?>().Take(4).ToList();
        for (int i = 0; i < items.Count && i < 3; i++)
        {
            if (i > 0)
                builder.Append(", ");
            builder.Append(Format(items[i]));
        }

        if (items.Count > 3)
            builder.Append(", …");

        builder.Append(']');
        return builder.ToString();
    }
}
