namespace RPGLibrary
{
	/// <summary>
	/// Exposes a unit's detailed properties.
	/// </summary>
	public interface IUnitProperties
	{
		/// <summary>
		/// Gets or sets the unit's name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the unit's class.
		/// </summary>
		string Class { get; set; }

		/// <summary>
		/// Gets or sets the unit's level.
		/// </summary>
		int Level { get; set; }

		/// <summary>
		/// Get the properties using a specified interface.
		/// </summary>
		/// <typeparam name="T">An interface inheriting from <see cref="IUnitProperties"/>.</typeparam>
		T GetAs<T>() where T : class, IUnitProperties;
	}
}
