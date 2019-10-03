namespace Davfalcon
{
    /// <summary>
    /// Represents an object that modifies another object.
    /// </summary>
    /// <typeparam name="T">The type of object that the modifier affects.</typeparam>
    public interface IModifier<T> : IDescribable
    {
        /// <summary>
        /// Gets the object being modified.
        /// </summary>
        T Target { get; }

        /// <summary>
        /// Binds the modifier to a new target.
        /// </summary>
        /// <param name="target">The new object to bind to.</param>
        void Bind(T target);

        /// <summary>
        /// Gets a representation of the modified object.
        /// </summary>
        /// <returns>A representation of the modified object.</returns>
        T AsModified();
    }
}
