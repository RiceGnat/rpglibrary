using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon.Engine.UnitManagement
{
	public static class EquipmentOperations
	{
		public static IUnitEquipmentProperties GetEquipmentProperties(this IUnit unit) => unit.Properties.GetAs<IUnitEquipmentProperties>();

		public static IEnumerable<IEquipment> GetAllEquipment(this IUnit unit)
			=> unit.GetEquipmentProperties().Equipment;

		public static IEquipment GetEquipped(this IUnit unit, EquipmentSlot slot)
			=> unit.GetEquipmentProperties().GetEquipment(slot);

		public static IEquipment Equip(this IUnit unit, IEquipment equipment)
		{
			EquipmentSlot slot = equipment.Slot;

			// Get current equipment
			IEquipment current = unit.GetEquipped(slot);

			// Remove current equipment
			unit.GetEquipmentProperties().EquipmentLookup.Remove(slot);

			// If not null, add new equipment
			if (equipment != null)
				unit.GetEquipmentProperties().EquipmentLookup.Add(slot, equipment);

			// Return previously equipped weapon
			return current;
			// Consider directly transferring previous equipment to inventory once implemented
		}

		public static IEquipment EquipTo(this IEquipment equipment, IUnit unit)
			=> unit.Equip(equipment);
	}
}
