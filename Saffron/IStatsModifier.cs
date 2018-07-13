namespace Saffron
{
	public interface IStatsModifier : IUnitModifier
	{
		/// <summary>
		/// Gets the values that this item will add to the unit's stats.
		/// </summary>
		IStats Additions { get; }

		/// <summary>
		/// Gets the multipliers that this item will apply to the unit's stats.
		/// </summary>
		IStats Multiplications { get; }
	}
}
