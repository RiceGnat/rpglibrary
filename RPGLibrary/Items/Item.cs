using System;

namespace RPGLibrary.Items
{
	[Serializable]
	public class Item : IItem
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsConsumable { get; set; }
	}
}
