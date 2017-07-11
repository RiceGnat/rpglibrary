using System;
using System.Collections.Generic;
using RPGLibrary;
using RPGLibrary.Items;

namespace Davfalcon
{
	public interface IUnitCombatProps : IUnitProperties
	{
		int CurrentHP { get; set; }
		int CurrentMP { get; set; }
		IWeapon EquippedWeapon { get; }
	}

	public interface IUnitEquipProps : IUnitProperties
	{
		IWeapon EquippedWeapon { get; }
		IWeapon EquipWeapon(IWeapon weapon);
		IEquipment GetEquipment(EquipmentSlot slot);
		IEquipment Equip(EquipmentSlot slot, IEquipment equipment);
	}

	public class UnitProperties : RPGLibrary.UnitProperties, IUnitCombatProps, IUnitEquipProps
	{
		protected IUnit unit;

		public int CurrentHP { get; set; }
		public int CurrentMP { get; set; }

		// other combat properties

		// weapon assignment
		public IWeapon EquippedWeapon { get; protected set; }

		public IWeapon EquipWeapon(IWeapon weapon)
		{
			EquippedWeapon = weapon;
			return Equip(EquipmentSlot.Weapon, weapon) as IWeapon;
		}

		public IEquipment GetEquipment(EquipmentSlot slot)
		{
			foreach (IEquipment e in unit.Modifiers)
			{
				if (e != null && e.Slot == slot) return e;
			}
			return null;
		}

		public IEquipment Equip(EquipmentSlot slot, IEquipment equipment)
		{
			if (equipment != null && equipment.Slot != slot) throw new ArgumentException("Equipment does not match specified slot.");

			IEquipment current = GetEquipment(slot);
			
			// Remove current equipment
			unit.Modifiers.Remove(current);

			// If not null, add new equipment
			if (equipment != null)
				unit.Modifiers.Add(equipment);

			// Return previously equipped weapon
			return current;
		}

		// inventory
		public IList<IItem> Inventory { get; protected set; }

		// spells known

		public UnitProperties(IUnit unit)
		{
			this.unit = unit;
			Inventory = new List<IItem>();
		}
	}
}
