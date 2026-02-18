namespace Asserty.Assertions;

/// <summary>
/// Allows sharing information between multiple phases of the assertion evaluation (verification, failure message
/// generation, etc).
/// </summary>
public class AssertionEvaluationContext
{
    private readonly Dictionary<string, object?> _data = new();

    /// <summary>
    /// Sets a value on the context.
    /// </summary>
    /// <param name="key">The key associated with the value.</param>
    /// <param name="value">The value.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    public T Set<T>(string key, T value)
    {
        _data[key] = value;
        return value;
    }

    /// <summary>
    /// Tries to get the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key associated with the value.</param>
    /// <param name="value">The value.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>true if the value is found, otherwise false.</returns>
    public bool TryGet<T>(string key, out T value)
    {
        if (_data.TryGetValue(key, out var rawValue))
        {
            value = (T)rawValue!;
            return true;
        }

        value = default!;
        return false;
    }

    /// <summary>
    /// Get the value associated with the specified key, or throws if no such value is found.
    /// </summary>
    /// <param name="key">The key associated with the value.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>The value associated with the specified key, if found.</returns>
    /// <exception cref="KeyNotFoundException">No value associated with the specified key was found.</exception>
    public T Get<T>(string key) => TryGet(key, out T value)
        ? value
        : throw new KeyNotFoundException("The specified context value was not found.");
}
