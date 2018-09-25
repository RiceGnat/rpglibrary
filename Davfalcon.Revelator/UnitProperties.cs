using System;
using System.Collections.Generic;
using Davfalcon.Collections.Adapters;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	internal class UnitProperties : IUnitCombatProperties, IUnitItemProperties, IUnitLevelingProperties
	{
		#region Combat
		public IDictionary<Enum, int> VolatileStats { get; } = new Dictionary<Enum, int>();

		[NonSerialized]
		private IUnitModifierStack buffs;
		public IUnitModifierStack Buffs { get { return buffs; } }

		public IUnitBattleState BattleState { get; set; }
		#endregion

		#region Equipment
		[NonSerialized]
		private IUnitModifierStack equipment;

		private ManagedEnumStringList equipmentSlots = new ManagedEnumStringList();
		private ManagedList<IEquipment> equippedSlots = new ManagedList<IEquipment>();

		public IEnumerable<Enum> EquipmentSlots { get => equipmentSlots.AsReadOnly(); }
		public IEnumerable<IEquipment> Equipment { get => equippedSlots.AsReadOnly(); }

		private int GetSlotIndex(Enum slotType, int offset)
		{
			int index = -1;
			for (int n = 0; n <= offset; n++)
			{
				index = equipmentSlots.IndexOf(slotType, index + 1);
			}
			return index;
		}

		private bool IndexHasEquipment(int index)
			=> index > -1 && index < equippedSlots.Count && equippedSlots[index] != null;

		public void AddEquipmentSlot(Enum slotType)
		{
			equipmentSlots.Add(slotType);
			equippedSlots.Add(null);
		}

		public void RemoveEquipmentSlotIndex(int index)
		{
			if (IndexHasEquipment(index)) equipment.Remove(equippedSlots[index]);
			equippedSlots.RemoveAt(index);
			equipmentSlots.RemoveAt(index); 
		}

		public IEquipment GetEquipment(Enum slot) => GetEquipment(slot, 0);
		public IEquipment GetEquipment(Enum slotType, int offset)
		{
			int index = GetSlotIndex(slotType, offset);

			return IndexHasEquipment(index) ? equippedSlots[index] : null;
		}

		public bool Equip(IEquipment equipment) => Equip(equipment, 0);
		public bool Equip(IEquipment equipment, int offset) => EquipSlotIndex(equipment, GetSlotIndex(equipment.SlotType, offset));
		public bool EquipSlotIndex(IEquipment equipment, int index)
		{
			if (!equipmentSlots[index].Equals(equipment.SlotType))
				throw new ArgumentException(String.Format("Equipment type ({2}) does not match slot specified by index {0} ({1}).", index, equipmentSlots[index], equipment.SlotType));

			if (index < 0 || IndexHasEquipment(index)) return false;

			equippedSlots[index] = equipment;
			this.equipment.Add(equipment);
			return true;
		}

		public bool UnequipSlot(Enum slot) => UnequipSlot(slot, 0);
		public bool UnequipSlot(Enum slotType, int offset) => UnequipSlotIndex(GetSlotIndex(slotType, offset));
		public bool UnequipSlotIndex(int index)
		{
			if (!IndexHasEquipment(index)) return false;

			equipment.Remove(equippedSlots[index]);
			equippedSlots[index] = null;
			return true;
		}
		#endregion

		#region Inventory
		public IList<IItem> Inventory { get; protected set; } = new List<IItem>();
		#endregion

		public void Bind(Unit unit)
		{
			buffs = unit.Buffs;
			//equipment = unit.Equipment;

			// Restore unit's equipment to modifier stack
			for (int i = 0; i < equipmentSlots.Count; i++)
			{
				// This seems roundabout but we want to fall back on slots in case of list size mismatch
				IEquipment e = equippedSlots[i];
				if (e != null) equipment.Add(e);
			}
		}
	}
}
