using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IUnitEquipmentManager<T> : IModifier<T> where T : IUnit
	{
		IEnumerable<Enum> EquipmentSlots { get; }
		IEnumerable<IEquipment<T>> All { get; }

		void AddEquipmentSlot(Enum slotType);
		void RemoveEquipmentSlotIndex(int index);
		IEquipment<T> GetEquipment(Enum slot);
		IEquipment<T> GetEquipment(Enum slotType, int offset);
		IEnumerable<IEquipment<T>> GetAllEquipmentForSlot(Enum slot);
		void Equip(IEquipment<T> equipment);
		void Equip(IEquipment<T> equipment, int offset);
		void EquipSlotIndex(IEquipment<T> equipment, int index);
		void UnequipSlot(Enum slot);
		void UnequipSlot(Enum slotType, int offset);
		void UnequipSlotIndex(int index);
	}
}
