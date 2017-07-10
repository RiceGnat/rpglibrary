namespace Davfalcon
{
	public enum EquipmentSlot
	{
		Weapon, Armor, Accessory
	}

	public class Equipment : RPGLibrary.Items.Equipment
	{
		public EquipmentSlot Slot { get; protected set; }

		protected Equipment() : base() { }

		public Equipment(EquipmentSlot slot) : base()
		{
			Slot = slot;
		}
	}
}
