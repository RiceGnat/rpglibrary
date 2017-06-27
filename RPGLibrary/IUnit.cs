namespace RPGLibrary
{
	/// <summary>
	/// Exposes basic properties of a unit.
	/// </summary>
	public interface IUnit
	{
		/// <summary>
		/// Gets the unit's unique ID.
		/// </summary>
		uint ID { get; }

		/// <summary>
		/// Gets the unit's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the unit's class.
		/// </summary>
		string Class { get; }

		/// <summary>
		/// Gets the unit's level.
		/// </summary>
		int Level { get; }

		/// <summary>
		/// Gets the unit's stats.
        /// </summary>
        IStats Stats { get; }

        /// <summary>
        /// Gets a breakdown of the unit's stats.
        /// </summary>
		IStatsPackage StatsDetails { get; }

		IUnitModifierStack Modifiers { get; }
	}
}
