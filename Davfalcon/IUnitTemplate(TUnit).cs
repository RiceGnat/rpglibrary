namespace Davfalcon
{
	/// <summary>
	/// Exposes basic properties of a unit.
	/// </summary>
	/// <typeparam name="TUnit">The interface used by the unit's implementation.</typeparam>
	public interface IUnitTemplate<TUnit> where TUnit : IUnitTemplate<TUnit>
	{
		/// <summary>
		/// Gets the unit's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the unit's stats.
		/// </summary>
		IStatsProperties Stats { get; }

		/// <summary>
		/// Gets the modifiers attached to the unit.
		/// </summary>
		IModifierStack<TUnit> Modifiers { get; }
	}
}
