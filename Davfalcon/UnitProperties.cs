using System;
using System.Collections.Generic;
using System.Linq;
using RPGLibrary;
using RPGLibrary.Items;

namespace Davfalcon
{
	public interface IUnitCombatProps : IUnitProperties
	{
		int CurrentHP { get; set; }
		int CurrentMP { get; set; }
		IWeapon EquippedWeapon { get; }

		IEnumerable<IBuff> GetBuffs();
	}

	public interface IUnitEquipProps : IUnitProperties
	{
		IUnitModifierStack Equipment { get; }
		IWeapon EquippedWeapon { get; }
		IEquipment GetEquipment(EquipmentSlot slot);
		IEquipment Equip(EquipmentSlot slot, IEquipment equipment);
	}

	[Serializable]
	public class UnitProperties : RPGLibrary.UnitProperties, IUnitCombatProps, IUnitEquipProps
	{
		[NonSerialized]
		private IUnit unit;

		public int CurrentHP { get; set; }
		public int CurrentMP { get; set; }

		// other combat properties
		public IEnumerable<IBuff> GetBuffs() => unit.Modifiers.Where(m => m is IBuff).Select(x => (IBuff)x);

		// equipment
		public IUnitModifierStack Equipment { get; private set; }

		[NonSerialized]
		private Dictionary<EquipmentSlot, IEquipment> equipLookup;

		public IWeapon EquippedWeapon
		{
			get
			{
				return GetEquipment(EquipmentSlot.Weapon) as IWeapon ?? Weapon.Unarmed;
			}
		}

		public IEquipment GetEquipment(EquipmentSlot slot)
		{
			if (equipLookup.ContainsKey(slot))
			{
				return equipLookup[slot];
			}
			else return null;
		}

		public IEquipment Equip(EquipmentSlot slot, IEquipment equipment)
		{
			if (equipment != null && equipment.Slot != slot) throw new ArgumentException("Equipment does not match specified slot.");

			IEquipment current = GetEquipment(slot);

			// Remove current equipment
			Equipment.Remove(current);
			equipLookup.Remove(slot);

			// If not null, add new equipment
			if (equipment != null)
			{
				Equipment.Add(equipment);
				equipLookup[slot] = equipment;
			}

			// Return previously equipped weapon
			return current;
		}

		// inventory
		public IList<IItem> Inventory { get; protected set; }

		// spells known

		public void Bind(IUnit unit)
		{
			Equipment.Bind(unit);
			unit.Modifiers.Bind(Equipment);
			equipLookup = new Dictionary<EquipmentSlot, IEquipment>();
			foreach (IEquipment equip in Equipment)
			{
				equipLookup[equip.Slot] = equip;
			}

			this.unit = unit;
		}

		public UnitProperties()
		{
			Equipment = new UnitModifierStack();
			Inventory = new List<IItem>();
		}
	}
}
