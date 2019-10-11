namespace Davfalcon
{
	/// <summary>
	/// Represents an object that modifies an entity.
	/// </summary>
	/// <typeparam name="T">The type of entity that the modifier affects.</typeparam>
	public interface IModifier<T>
	{
		/// <summary>
		/// Gets the entity being modified.
		/// </summary>
		T Target { get; }

		/// <summary>
		/// Binds the modifier to a new entity.
		/// </summary>
		/// <param name="target">The new entity to bind to.</param>
		void Bind(T target);

		/// <summary>
		/// Gets a representation of the modified entity.
		/// </summary>
		/// <returns>A representation of the modified entity.</returns>
		T AsModified();
	}
}
