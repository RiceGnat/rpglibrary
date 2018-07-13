namespace Saffron
{
	/// <summary>
	/// Represents an item that can be attached to a unit to modify its stats.
	/// </summary>
	public interface IModifierItem : IItem, IStatsModifier
	{
		// Inherited properties are hidden to resolve ambiguity between interfaces

		/// <summary>
		/// Gets the item's name.
		/// </summary>
		new string Name { get; }

		/// <summary>
		/// Gets a description of the item.
		/// </summary>
		new string Description { get; }
	}
}
