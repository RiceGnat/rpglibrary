namespace RPGLibrary
{
	public interface IStatsModifierEditable : IUnitModifier
	{
		/// <summary>
		/// Gets the values that this item will add to the unit's stats.
		/// </summary>
		IStatsEditable Additions { get; }

		/// <summary>
		/// Gets the multipliers that this item will apply to the unit's stats.
		/// </summary>
		IStatsEditable Multiplications { get; }
	}
}
