using System.Collections.Generic;
using Saffron;

namespace Davfalcon
{
	public interface IUnitItemProperties
	{
		IList<IItem> Inventory { get; }

		IEnumerable<EquipmentType> EquipmentSlots { get; }
		IEnumerable<IEquipment> Equipment { get; }

		void AddEquipmentSlot(EquipmentType slotType);
		void RemoveEquipmentSlotIndex(int index);
		IEquipment GetEquipment(EquipmentType slot);
		IEquipment GetEquipment(EquipmentType slotType, int offset);
		bool Equip(IEquipment equipment);
		bool Equip(IEquipment equipment, int offset);
		bool UnequipSlot(EquipmentType slot);
		bool UnequipSlot(EquipmentType slotType, int offset);
		bool UnequipSlotIndex(int index);
	}
}
