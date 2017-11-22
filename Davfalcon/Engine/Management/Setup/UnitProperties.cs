using System;
using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon.Engine.Management.Setup
{
	[Serializable]
	internal class UnitProperties : RPGLibrary.UnitProperties, IUnitCombatProperties, IUnitItemManagementProperties, IUnitLevelingProperties
	{
		#region Combat
		public int CurrentHP { get; set; }
		public int CurrentMP { get; set; }

		[NonSerialized]
		private IUnitModifierStack buffs;
		public IUnitModifierStack Buffs { get { return buffs; } }

		public IUnitBattleState BattleState { get; set; }
		#endregion

		#region Equipment
		[NonSerialized]
		private EquipmentSlotMap equipLookup;
		public IDictionary<EquipmentSlot, IEquipment> EquipmentLookup { get { return equipLookup; } }
		public IEnumerable<IEquipment> Equipment { get { return EquipmentLookup.Values; } }

		public IWeapon EquippedWeapon
		{
			get { return GetEquipment(EquipmentSlot.Weapon) as IWeapon ?? Weapon.Unarmed; }
		}

		public IEquipment GetEquipment(EquipmentSlot slot)
			=> EquipmentLookup.ContainsKey(slot) ? equipLookup[slot] : null;
		#endregion

		#region Inventory
		public IList<IItem> Inventory { get; protected set; }
		#endregion

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
