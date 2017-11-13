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

		IUnitModifierStack Buffs { get; }
	}

	public interface IUnitEquipProps : IUnitProperties
	{
		IUnitModifierStack Equipment { get; }
		IWeapon EquippedWeapon { get; }
		IEquipment GetEquipment(EquipmentSlot slot);
		IEquipment Equip(EquipmentSlot slot, IEquipment equipment);
	}

	[Serializable]
	internal class UnitProperties : RPGLibrary.UnitProperties, IUnitCombatProps, IUnitEquipProps
	{
		public int CurrentHP { get; set; }
		public int CurrentMP { get; set; }

		// other combat properties
		[NonSerialized]
		private IUnitModifierStack buffs;
		public IUnitModifierStack Buffs { get { return buffs; } }

		// equipment
		[NonSerialized]
		private IUnitModifierStack equipment;
		public IUnitModifierStack Equipment { get { return equipment; } }

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

		public void Bind(Unit unit)
		{
			//Equipment.Bind(unit);
			//unit.Modifiers.Bind(Equipment);
			equipment = unit.Equipment;
			equipLookup = new Dictionary<EquipmentSlot, IEquipment>();
			foreach (IEquipment equip in Equipment)
			{
				equipLookup[equip.Slot] = equip;
			}

			buffs = unit.Buffs;
		}

		public UnitProperties()
		{
			//Equipment = new UnitModifierStack();
			Inventory = new List<IItem>();
		}
	}
}
