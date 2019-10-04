using System;

namespace Davfalcon
{
    /// <summary>
    /// Generic abstract base class for modifiers.
    /// </summary>
    public abstract class Modifier<T> : IModifier<T>
    {
        [NonSerialized]
        private T target;

        /// <summary>
        /// Gets the object the modifier is bound to.
        /// </summary>
        public T Target => target;

        /// <summary>
        /// Binds the modifier to an object.
        /// </summary>
        /// <param name="target">The object to bind the modifier to.</param>
        public virtual void Bind(T target)
        {
            this.target = target;
        }

        /// <summary>
        /// Gets a representation of the modified object.
        /// </summary>
        /// <returns>A representation of the modified object.</returns>
        public abstract T AsModified();
    }
}
