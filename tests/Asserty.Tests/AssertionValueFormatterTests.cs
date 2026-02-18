using Asserty.Assertions;

namespace Asserty.Tests;

public class AssertionValueFormatterTests
{
    [Fact]
    public void Format_Null_ReturnsNullPlaceholder()
    {
        var result = AssertionValueFormatter.Format(null);

        Assert.Equal("(null)", result);
    }

    [Theory]
    [InlineData("hello", "\"hello\"")]
    [InlineData("", "\"\"")]
    public void Format_String_WrapsInQuotes(string input, string expected)
    {
        var result = AssertionValueFormatter.Format(input);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(@"C:\path\to\file", @"""C:\\path\\to\\file""")]
    [InlineData(@"He said ""hello""", @"""He said \""hello\""""")]
    [InlineData("line1\nline2", "\"line1\\nline2\"")]
    [InlineData("line1\rline2", "\"line1\\rline2\"")]
    [InlineData("col1\tcol2", "\"col1\\tcol2\"")]
    [InlineData("beep\a", "\"beep\\a\"")]
    [InlineData("text\b", "\"text\\b\"")]
    [InlineData("esc\e", "\"esc\\e\"")]
    [InlineData("page1\fpage2", "\"page1\\fpage2\"")]
    [InlineData("text\0end", "\"text\\0end\"")]
    [InlineData("line1\n\tline2\r\n", "\"line1\\n\\tline2\\r\\n\"")]
    public void Format_String_EscapesSpecialCharacters(string input, string expected)
    {
        var result = AssertionValueFormatter.Format(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Format_EmptyCollection_ReturnsEmptyBrackets()
    {
        var result = AssertionValueFormatter.Format(new List<int>());

        Assert.Equal("[]", result);
    }

    [Fact]
    public void Format_CollectionWithOneElement_FormatsElement()
    {
        var result = AssertionValueFormatter.Format(new List<int> { 42 });

        Assert.Equal("[42]", result);
    }

    [Fact]
    public void Format_CollectionWithTwoElements_FormatsBothElements()
    {
        var result = AssertionValueFormatter.Format(new List<int> { 1, 2 });

        Assert.Equal("[1, 2]", result);
    }

    [Fact]
    public void Format_CollectionWithThreeElements_FormatsAllElements()
    {
        var result = AssertionValueFormatter.Format(new List<int> { 1, 2, 3 });

        Assert.Equal("[1, 2, 3]", result);
    }

    [Fact]
    public void Format_CollectionWithFourElements_FormatsFirstThreeWithEllipsis()
    {
        var result = AssertionValueFormatter.Format(new List<int> { 1, 2, 3, 4 });

        Assert.Equal("[1, 2, 3, …]", result);
    }

    [Fact]
    public void Format_CollectionWithManyElements_FormatsFirstThreeWithEllipsis()
    {
        var result = AssertionValueFormatter.Format(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

        Assert.Equal("[1, 2, 3, …]", result);
    }

    [Fact]
    public void Format_CollectionOfStrings_FormatsStringsWithQuotes()
    {
        var result = AssertionValueFormatter.Format(new List<string> { "one", "two", "three" });

        Assert.Equal("[\"one\", \"two\", \"three\"]", result);
    }

    [Fact]
    public void Format_CollectionWithNullElements_FormatsNullsAsPlaceholder()
    {
        var result = AssertionValueFormatter.Format(new List<string?> { "one", null, "three" });

        Assert.Equal("[\"one\", (null), \"three\"]", result);
    }

    [Fact]
    public void Format_Array_FormatsAsCollection()
    {
        var result = AssertionValueFormatter.Format(new[] { 1, 2, 3 });

        Assert.Equal("[1, 2, 3]", result);
    }

    [Fact]
    public void Format_CollectionOfMixedTypes_FormatsEachElementCorrectly()
    {
        var result = AssertionValueFormatter.Format(new List<object?> { 42, "hello", null });

        Assert.Equal("[42, \"hello\", (null)]", result);
    }

    [Fact]
    public void Format_Integer_UsesToString()
    {
        var result = AssertionValueFormatter.Format(42);

        Assert.Equal("42", result);
    }

    [Fact]
    public void Format_Boolean_UsesToString()
    {
        var result = AssertionValueFormatter.Format(true);

        Assert.Equal("True", result);
    }

    [Fact]
    public void Format_Double_UsesToString()
    {
        var result = AssertionValueFormatter.Format(3.14);

        Assert.Equal("3.14", result);
    }

    [Fact]
    public void Format_CustomObject_UsesToString()
    {
        var obj = new CustomObject("test");

        var result = AssertionValueFormatter.Format(obj);

        Assert.Equal("CustomObject: test", result);
    }

    [Fact]
    public void Format_ObjectWithNullToString_ReturnsEmptyString()
    {
        var obj = new ObjectWithNullToString();

        var result = AssertionValueFormatter.Format(obj);

        Assert.Equal("", result);
    }

    private class CustomObject(string name)
    {
        public override string ToString() => $"CustomObject: {name}";
    }

    private class ObjectWithNullToString
    {
        public override string? ToString() => null;
    }
}
