namespace Davfalcon
{
	/// <summary>
	/// Exposes basic properties of a unit.
	/// </summary>
	public interface IUnit : INameable, IStatsContainer
	{
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
		/// Gets the modifiers attached to the unit.
		/// </summary>
		IModifierCollection<IUnit> Modifiers { get; }
	}
}
