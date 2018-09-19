using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IUnitItemProperties
	{
		IList<IItem> Inventory { get; }

		IEnumerable<Enum> EquipmentSlots { get; }
		IEnumerable<IEquipment> Equipment { get; }

		void AddEquipmentSlot(Enum slotType);
		void RemoveEquipmentSlotIndex(int index);
		IEquipment GetEquipment(Enum slot);
		IEquipment GetEquipment(Enum slotType, int offset);
		bool Equip(IEquipment equipment);
		bool Equip(IEquipment equipment, int offset);
		bool EquipSlotIndex(IEquipment equipment, int index);
		bool UnequipSlot(Enum slot);
		bool UnequipSlot(Enum slotType, int offset);
		bool UnequipSlotIndex(int index);
	}
}
