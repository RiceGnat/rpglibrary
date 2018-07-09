using System;
using System.Collections;
using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	internal class EquipmentSlotMap : IDictionary<EquipmentType, IEquipment>
	{
		private Dictionary<EquipmentType, IEquipment> lookup = new Dictionary<EquipmentType, IEquipment>();
		private IUnitModifierStack modifiers;

		public IEquipment this[EquipmentType slot]
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

		public void Add(EquipmentType slot, IEquipment equipment)
		{
			if (equipment == null) throw new ArgumentNullException("Equipment cannot be null");
			lookup.Add(slot, equipment);
			modifiers.Add(equipment);
		}

		public bool Remove(EquipmentType slot)
		{
			if (!lookup.ContainsKey(slot)) return false;
			modifiers.Remove(lookup[slot]);
			return lookup.Remove(slot);
		}

		#region IDictionary implementation
		ICollection<EquipmentType> IDictionary<EquipmentType, IEquipment>.Keys
		{
			get { return lookup.Keys; }
		}

		ICollection<IEquipment> IDictionary<EquipmentType, IEquipment>.Values
		{
			get { return lookup.Values; }
		}

		int ICollection<KeyValuePair<EquipmentType, IEquipment>>.Count
		{
			get { return lookup.Count; }
		}

		bool ICollection<KeyValuePair<EquipmentType, IEquipment>>.IsReadOnly
		{
			get { return ((IDictionary<EquipmentType, IEquipment>)lookup).IsReadOnly; }
		}

		bool IDictionary<EquipmentType, IEquipment>.ContainsKey(EquipmentType key)
			=> lookup.ContainsKey(key);

		bool IDictionary<EquipmentType, IEquipment>.TryGetValue(EquipmentType key, out IEquipment value)
			=> lookup.TryGetValue(key, out value);

		bool ICollection<KeyValuePair<EquipmentType, IEquipment>>.Contains(KeyValuePair<EquipmentType, IEquipment> item)
			=> ((IDictionary<EquipmentType, IEquipment>)lookup).Contains(item);

		void ICollection<KeyValuePair<EquipmentType, IEquipment>>.CopyTo(KeyValuePair<EquipmentType, IEquipment>[] array, int arrayIndex)
			=> ((IDictionary<EquipmentType, IEquipment>)lookup).CopyTo(array, arrayIndex);

		IEnumerator<KeyValuePair<EquipmentType, IEquipment>> IEnumerable<KeyValuePair<EquipmentType, IEquipment>>.GetEnumerator()
			=> lookup.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> lookup.GetEnumerator();

		void ICollection<KeyValuePair<EquipmentType, IEquipment>>.Add(KeyValuePair<EquipmentType, IEquipment> item)
			=> Add(item.Key, item.Value);

		bool ICollection<KeyValuePair<EquipmentType, IEquipment>>.Remove(KeyValuePair<EquipmentType, IEquipment> item)
		{
			if (!lookup.ContainsKey(item.Key)) return false;
			bool removed = ((IDictionary<EquipmentType, IEquipment>)lookup).Remove(item);
			if (removed)
				modifiers.Remove(lookup[item.Key]);
			return removed;
		}

		void ICollection<KeyValuePair<EquipmentType, IEquipment>>.Clear()
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
