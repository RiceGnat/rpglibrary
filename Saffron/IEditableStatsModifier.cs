namespace Saffron
{
	public interface IEditableStatsModifier : IUnitModifier
	{
		/// <summary>
		/// Gets the values that this item will add to the unit's stats in an editable format.
		/// </summary>
		IEditableStats Additions { get; }

		/// <summary>
		/// Gets the multipliers that this item will apply to the unit's stats in an editable format.
		/// </summary>
		IEditableStats Multiplications { get; }
	}
}
