namespace RPGLibrary.Items
{
	/// <summary>
	/// Represents an item that can be attached to a unit to modify its stats.
	/// </summary>
	public interface IModifierItem : IItem, IUnitModifier
	{
		/// <summary>
		/// Gets the item's name.
		/// </summary>
		new string Name { get; }

		/// <summary>
		/// Gets a description of the item.
		/// </summary>
		new string Description { get; }

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
