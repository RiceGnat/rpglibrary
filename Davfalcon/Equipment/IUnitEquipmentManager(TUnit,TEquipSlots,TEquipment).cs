using System;
using System.Collections.Generic;

namespace Davfalcon.Equipment
{
	public interface IUnitEquipmentManager<TUnit, TEquipmentType, TEquipment> : IModifier<TUnit>
		where TUnit : IUnitTemplate<TUnit>
		where TEquipmentType : Enum
		where TEquipment : IEquipment<TUnit, TEquipmentType>
	{
		IEnumerable<TEquipmentType> EquipmentSlots { get; }

		void AddEquipmentSlot(TEquipmentType type);
		TEquipment GetEquipmentOfType(TEquipmentType type);
		TEquipment GetEquipmentOfType(TEquipmentType type, int offset);
		IEnumerable<TEquipment> GetAllEquipment();
		IEnumerable<TEquipment> GetAllEquipmentOfType(TEquipmentType type);
		void Equip(TEquipment equipment);
		void Equip(TEquipment equipment, int offset);
		void EquipToSlotIndex(int index, TEquipment equipment);
		void UnequipSlot(TEquipmentType type);
		void UnequipSlot(TEquipmentType type, int offset);
		void UnequipSlotIndex(int index);
	}
}
