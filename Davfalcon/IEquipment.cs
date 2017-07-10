namespace Davfalcon
{
	public interface IEquipment : RPGLibrary.Items.IEquipment
	{
		EquipmentSlot Slot { get; }
	}
}
