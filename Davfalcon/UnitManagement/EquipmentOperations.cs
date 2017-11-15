using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGLibrary;

namespace Davfalcon.UnitManagement
{
	public static class EquipmentOperations
	{
		public static IUnitEquipmentProperties GetEquipmentProperties(this IUnit unit) => unit.Properties.GetAs<IUnitEquipmentProperties>();

		public static IEnumerable<IEquipment> GetAllEquipment(this IUnit unit)
			=> unit.GetEquipmentProperties().EquipmentLookup.Values;

		public static IEquipment GetEquipped(this IUnit unit, EquipmentSlot slot)
			=> unit.GetEquipmentProperties().GetEquipment(slot);

		public static IEquipment Equip(this IUnit unit, EquipmentSlot slot, IEquipment equipment)
		{
			if (equipment != null && equipment.Slot != slot) throw new ArgumentException("Equipment does not match specified slot.");

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
	}
}
