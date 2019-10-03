namespace Davfalcon
{
    /// <summary>
    /// Exposes basic properties of a unit.
    /// </summary>
    public interface IUnit : INameable
	{
		/// <summary>
		/// Gets the unit's name.
		/// </summary>
		new string Name { get; }
		
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

		/// <summary>
		/// Gets the modifiers attached to the unit.
		/// </summary>
		IModifierStack<IUnit> Modifiers { get; }
	}
}
