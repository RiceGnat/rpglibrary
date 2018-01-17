using System;
using System.Collections;
using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	internal class EquipmentSlotMap : IDictionary<EquipmentSlot, IEquipment>
	{
		private Dictionary<EquipmentSlot, IEquipment> lookup = new Dictionary<EquipmentSlot, IEquipment>();
		private IUnitModifierStack modifiers;

		public IEquipment this[EquipmentSlot slot]
		{
			get
			{
				return lookup[slot];
			}

			set
			{
				Remove(slot);
				Add(slot, value);
			}
		}

		public void Add(EquipmentSlot slot, IEquipment equipment)
		{
			if (equipment == null) throw new ArgumentNullException("Equipment cannot be null");
			lookup.Add(slot, equipment);
			modifiers.Add(equipment);
		}

		public bool Remove(EquipmentSlot slot)
		{
			if (!lookup.ContainsKey(slot)) return false;
			modifiers.Remove(lookup[slot]);
			return lookup.Remove(slot);
		}

		#region IDictionary implementation
		ICollection<EquipmentSlot> IDictionary<EquipmentSlot, IEquipment>.Keys
		{
			get { return lookup.Keys; }
		}

		ICollection<IEquipment> IDictionary<EquipmentSlot, IEquipment>.Values
		{
			get { return lookup.Values; }
		}

		int ICollection<KeyValuePair<EquipmentSlot, IEquipment>>.Count
		{
			get { return lookup.Count; }
		}

		bool ICollection<KeyValuePair<EquipmentSlot, IEquipment>>.IsReadOnly
		{
			get { return ((IDictionary<EquipmentSlot, IEquipment>)lookup).IsReadOnly; }
		}

		bool IDictionary<EquipmentSlot, IEquipment>.ContainsKey(EquipmentSlot key)
			=> lookup.ContainsKey(key);

		bool IDictionary<EquipmentSlot, IEquipment>.TryGetValue(EquipmentSlot key, out IEquipment value)
			=> lookup.TryGetValue(key, out value);

		bool ICollection<KeyValuePair<EquipmentSlot, IEquipment>>.Contains(KeyValuePair<EquipmentSlot, IEquipment> item)
			=> ((IDictionary<EquipmentSlot, IEquipment>)lookup).Contains(item);

		void ICollection<KeyValuePair<EquipmentSlot, IEquipment>>.CopyTo(KeyValuePair<EquipmentSlot, IEquipment>[] array, int arrayIndex)
			=> ((IDictionary<EquipmentSlot, IEquipment>)lookup).CopyTo(array, arrayIndex);

		IEnumerator<KeyValuePair<EquipmentSlot, IEquipment>> IEnumerable<KeyValuePair<EquipmentSlot, IEquipment>>.GetEnumerator()
			=> lookup.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> lookup.GetEnumerator();

		void ICollection<KeyValuePair<EquipmentSlot, IEquipment>>.Add(KeyValuePair<EquipmentSlot, IEquipment> item)
			=> Add(item.Key, item.Value);

		bool ICollection<KeyValuePair<EquipmentSlot, IEquipment>>.Remove(KeyValuePair<EquipmentSlot, IEquipment> item)
		{
			if (!lookup.ContainsKey(item.Key)) return false;
			bool removed = ((IDictionary<EquipmentSlot, IEquipment>)lookup).Remove(item);
			if (removed)
				modifiers.Remove(lookup[item.Key]);
			return removed;
		}

		void ICollection<KeyValuePair<EquipmentSlot, IEquipment>>.Clear()
		{
			lookup.Clear();
			modifiers.Clear();
		}
		#endregion

		public EquipmentSlotMap(IUnitModifierStack modifierStack)
		{
			modifiers = modifierStack;
			foreach (IEquipment equip in modifierStack)
			{
				lookup[equip.Slot] = equip;
			}
		}
	}
}
