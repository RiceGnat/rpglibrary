using System;

namespace RPGLibrary
{
	/// <summary>
	/// Implements basic item properties.
	/// </summary>
	[Serializable]
	public class Item : IItem, IEditableDescription
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
