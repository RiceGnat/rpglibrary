namespace RPGLibrary
{
	/// <summary>
	/// Exposes properties of unit modifiers.
	/// </summary>
	public interface IUnitModifier : IUnit
	{
		/// <summary>
		/// Gets the base unit the object is modifying.
		/// </summary>
		IUnit Base { get; }

		/// <summary>
		/// Binds the modifier to a new target.
		/// </summary>
		/// <param name="target">The new object to bind to.</param>
		void Bind(IUnit target);
	}
}
