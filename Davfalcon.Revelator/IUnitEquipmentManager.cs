using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IUnitEquipmentManager : IModifier<IUnit>
	{
		IEnumerable<Enum> EquipmentSlots { get; }
		IEnumerable<IEquipment<IUnit>> All { get; }

		void AddEquipmentSlot(Enum slotType);
		void RemoveEquipmentSlotIndex(int index);
		IEquipment<IUnit> GetEquipment(Enum slot);
		IEquipment<IUnit> GetEquipment(Enum slotType, int offset);
		IEnumerable<IEquipment<IUnit>> GetAllEquipmentForSlot(Enum slot);
		void Equip(IEquipment<IUnit> equipment);
		void Equip(IEquipment<IUnit> equipment, int offset);
		void EquipSlotIndex(IEquipment<IUnit> equipment, int index);
		void UnequipSlot(Enum slot);
		void UnequipSlot(Enum slotType, int offset);
		void UnequipSlotIndex(int index);
	}
}
