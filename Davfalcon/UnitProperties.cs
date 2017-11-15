using System;
using System.Collections.Generic;
using RPGLibrary;
using RPGLibrary.Items;

namespace Davfalcon
{
	public interface IUnitCombatProperties : IUnitProperties
	{
		int CurrentHP { get; set; }
		int CurrentMP { get; set; }
		IWeapon EquippedWeapon { get; }
		IEnumerable<IEquipment> Equipment { get; }

		IUnitModifierStack Buffs { get; }
	}

	public interface IUnitEquipmentProperties : IUnitProperties
	{
		IEnumerable<IEquipment> Equipment { get; }
		IDictionary<EquipmentSlot, IEquipment> EquipmentLookup { get; }
		IWeapon EquippedWeapon { get; }
		IEquipment GetEquipment(EquipmentSlot slot);
	}

	public interface IUnitInventoryProperties : IUnitProperties
	{
		IList<IItem> Inventory { get; }
	}

	[Serializable]
	internal class UnitProperties : RPGLibrary.UnitProperties, IUnitCombatProperties, IUnitEquipmentProperties
	{
		public int CurrentHP { get; set; }
		public int CurrentMP { get; set; }

		// other combat properties
		[NonSerialized]
		private IUnitModifierStack buffs;
		public IUnitModifierStack Buffs { get { return buffs; } }

		#region Equipment
		[NonSerialized]
		private EquipmentSlotMap equipLookup;
		public IDictionary<EquipmentSlot, IEquipment> EquipmentLookup { get { return equipLookup; } }
		public IEnumerable<IEquipment> Equipment { get { return EquipmentLookup.Values; } }

		public IWeapon EquippedWeapon
		{
			get
			{
				return GetEquipment(EquipmentSlot.Weapon) as IWeapon ?? Weapon.Unarmed;
			}
		}

		public IEquipment GetEquipment(EquipmentSlot slot)
		{
			if (EquipmentLookup.ContainsKey(slot))
			{
				return equipLookup[slot];
			}
			else return null;
		}
		#endregion

		// inventory
		public IList<IItem> Inventory { get; protected set; }

		// spells known

		public void Bind(Unit unit)
		{
			equipLookup = new EquipmentSlotMap(unit.Equipment);

			buffs = unit.Buffs;
		}

		public UnitProperties()
		{
			Inventory = new List<IItem>();
		}
	}
}
