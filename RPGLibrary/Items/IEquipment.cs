namespace RPGLibrary.Items
{
	/// <summary>
	/// Exposes properties of a piece of equipment.
	/// </summary>
	public interface IEquipment : IItem, IUnitModifier
	{
		/// <summary>
		/// Gets the name of the equipment.
		/// </summary>
		new string Name { get; }

		/// <summary>
		/// Gets the values that this equipment will add to the unit's stats.
		/// </summary>
		IStats Additions { get; }

		/// <summary>
		/// Gets the multipliers that this equipment will apply to the unit's stats.
		/// </summary>
		IStats Multiplications { get; }
	}
}
