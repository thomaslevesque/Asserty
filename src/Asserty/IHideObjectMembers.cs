using System.ComponentModel;

namespace Asserty;

[EditorBrowsable(EditorBrowsableState.Never)]
public interface IHideObjectMembers
{
    /// <summary>
    /// Hides the ToString-method.
    /// </summary>
    /// <returns>A string representation of the implementing object.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    string? ToString();

    /// <summary>
    /// Determines whether the specified <see cref="object"/> is equal to this instance.
    /// </summary>
    /// <param name="o">The <see cref="object"/> to compare with this instance.</param>
    /// <returns>
    /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    bool Equals(object? o);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    int GetHashCode();

    /// <summary>
    /// Gets the type.
    /// </summary>
    /// <returns>The exact runtime type of the current instance.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    Type GetType();
}
