namespace Asserty;

/// <summary>
/// An exception thrown when an assertion fails
/// </summary>
/// <param name="message">The assertion failure message.</param>
/// <param name="innerException">The optional inner exception.</param>
public class AssertionException(string message, Exception? innerException = null)
    : Exception(message, innerException);
