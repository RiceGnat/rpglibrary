using System;
using System.Collections.Generic;

namespace Davfalcon.Equipment
{
	public interface IUnitEquipmentManager<TUnit, TEquipSlots> : IModifier<TUnit>
		where TUnit : IUnitTemplate<TUnit>
		where TEquipSlots : Enum
	{
		IEnumerable<TEquipSlots> EquipmentSlots { get; }
		IEnumerable<IEquipment<TUnit, TEquipSlots>> All { get; }

		void AddEquipmentSlot(TEquipSlots slotType);
		void AddEquipmentSlot(TEquipSlots slotType, int capacity);
		IEquipment<TUnit, TEquipSlots> GetEquipment(TEquipSlots slot);
		IEquipment<TUnit, TEquipSlots> GetEquipment(TEquipSlots slotType, int offset);
		IEnumerable<IEquipment<TUnit, TEquipSlots>> GetAllEquipmentForSlot(TEquipSlots slot);
		void Equip(IEquipment<TUnit, TEquipSlots> equipment);
		void Equip(IEquipment<TUnit, TEquipSlots> equipment, int offset);
		void EquipSlotIndex(IEquipment<TUnit, TEquipSlots> equipment, int index);
		void UnequipSlot(TEquipSlots slot);
		void UnequipSlot(TEquipSlots slotType, int offset);
		void UnequipSlotIndex(int index);
	}
}
