namespace Asserty;

public class AssertionException(string message, Exception? innerException = null)
    : Exception(message, innerException);
