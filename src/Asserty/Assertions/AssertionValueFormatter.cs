using System.Buffers;
using System.Text;

namespace Asserty.Assertions;

public static class AssertionValueFormatter
{
    public static string Format(object? value) => value switch
    {
        null => "(null)",
        string s => FormatStringValue(s),
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
}
