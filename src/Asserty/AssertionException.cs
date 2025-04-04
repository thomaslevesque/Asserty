namespace Asserty;

public class AssertionException : Exception
{
    public AssertionException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}
