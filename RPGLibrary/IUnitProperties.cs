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

		T GetAs<T>() where T : class, IUnitProperties;
	}
}
