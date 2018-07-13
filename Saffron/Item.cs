using System;

namespace Saffron
{
	// Not sure this is needed
	/// <summary>
	/// Implements basic item properties.
	/// </summary>
	[Serializable]
	public class Item : IItem, IEditableDescription
	{
		/// <summary>
		/// Gets or sets the item's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the item's description.
		/// </summary>
		public string Description { get; set; }
	}
}
