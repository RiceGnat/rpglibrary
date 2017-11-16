namespace RPGLibrary.Items
{
	/// <summary>
	/// Exposes basic properties of an item.
	/// </summary>
	public interface IItem
	{
		/// <summary>
		/// Gets the item's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets a description of the item.
		/// </summary>
		string Description { get; }
	}
}
