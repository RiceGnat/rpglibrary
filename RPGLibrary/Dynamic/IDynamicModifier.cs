namespace RPGLibrary.Dynamic
{
	/// <summary>
	/// Exposes properties, events, and functions for dynamic modifiers such as buffs and debuffs.
	/// </summary>
	public interface IDynamicModifier : IUnitModifier
	{
		/// <summary>
		/// Gets the name of the modifier.
		/// </summary>
		new string Name { get; }

		/// <summary>
		/// Gets a description of the modifier.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Gets the total duration of the modifier.
		/// </summary>
		int Duration { get; set; }

		/// <summary>
		/// Gets the remaining duration of the modifier.
		/// </summary>
		int Remaining { get; set; }

		/// <summary>
		/// Called each time the modifier is ticked.
		/// </summary>
		event UnitEventHandler Upkeep;

		/// <summary>
		/// Ticks the modifier. Call this on each time unit.
		/// </summary>
		void Tick();
	}
}
