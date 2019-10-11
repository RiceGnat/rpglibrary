using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Collections.Adapters;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Equipment
{
	[Serializable]
	public abstract class UnitEquipmentManager<TUnit, TEquipSlots> : Modifier<TUnit>, IUnitEquipmentManager<TUnit, TEquipSlots>, IUnitComponent<TUnit>
		where TUnit : IUnitTemplate<TUnit>
		where TEquipSlots : Enum
	{
		private class EquipmentSlot
		{
			public int Capacity { get; }
			public IModifierStack<TUnit> Modifiers { get; }
			public bool IsFull => Modifiers.Count >= Capacity;

			public EquipmentSlot(int capacity, IModifierStack<TUnit> modifiers)
			{
				Capacity = capacity;
				Modifiers = modifiers;
			}
		}

		private readonly IModifierStack<TUnit> stack = new ModifierStack<TUnit>();
		private readonly Dictionary<TEquipSlots, EquipmentSlot> slots = new Dictionary<TEquipSlots, EquipmentSlot>();

		public IEnumerable<TEquipSlots> EquipmentSlots => throw new NotImplementedException();

		public IEnumerable<IEquipment<TUnit, TEquipSlots>> All => throw new NotImplementedException();

		public void AddEquipmentSlot(TEquipSlots slotType) => AddEquipmentSlot(slotType, 1);

		public void AddEquipmentSlot(TEquipSlots slotType, int capacity)
		{
			IModifierStack<TUnit> slotStack = new ModifierStack<TUnit>();
			slots.Add(slotType, new EquipmentSlot(capacity, slotStack));
			stack.Add(slotStack);
		}

		public void Equip(IEquipment<TUnit, TEquipSlots> equipment)
		{
			throw new NotImplementedException();
		}

		public void Equip(IEquipment<TUnit, TEquipSlots> equipment, int offset)
		{
			throw new NotImplementedException();
		}

		public void EquipSlotIndex(IEquipment<TUnit, TEquipSlots> equipment, int index)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<IEquipment<TUnit, TEquipSlots>> GetAllEquipmentForSlot(TEquipSlots slot)
		{
			throw new NotImplementedException();
		}

		public IEquipment<TUnit, TEquipSlots> GetEquipment(TEquipSlots slot)
		{
			throw new NotImplementedException();
		}

		public IEquipment<TUnit, TEquipSlots> GetEquipment(TEquipSlots slotType, int offset)
		{
			throw new NotImplementedException();
		}

		public void UnequipSlot(TEquipSlots slot)
		{
			throw new NotImplementedException();
		}

		public void UnequipSlot(TEquipSlots slotType, int offset)
		{
			throw new NotImplementedException();
		}

		public void UnequipSlotIndex(int index)
		{
			throw new NotImplementedException();
		}

		private bool CheckSlotExists(TEquipSlots slot)
		{
			if (!slots.ContainsKey(slot)) throw new InvalidOperationException("The unit does not have the specified slot.");
			return true;
		}

		private bool CheckSlotNotFull(TEquipSlots slot)
		{
			if (slots[slot].IsFull) throw new InvalidOperationException("The specified slot is full.");
			return true;
		}

		void IUnitComponent<TUnit>.Initialize(TUnit unit)
		{
			unit.Modifiers.Add(this);
		}
	}
}
