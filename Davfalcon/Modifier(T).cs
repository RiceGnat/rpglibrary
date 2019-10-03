using System;

namespace Davfalcon
{
    /// <summary>
    /// Generic abstract base class for modifiers.
    /// </summary>
    public abstract class Modifier<T> : IModifier<T>, IEditableDescription
    {
        [NonSerialized]
        private T target;

        /// <summary>
        /// Gets or sets the modifier's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the modifier's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the object the modifier is bound to.
        /// </summary>
        public T Target => target;

        /// <summary>
        /// Binds the modifier to an object.
        /// </summary>
        /// <param name="target">The object to bind the modifier to.</param>
        public virtual void Bind(T target) => this.target = target;

        /// <summary>
        /// Gets a representation of the modified object.
        /// </summary>
        /// <returns>A representation of the modified object.</returns>
        public abstract T AsModified();
    }
}
