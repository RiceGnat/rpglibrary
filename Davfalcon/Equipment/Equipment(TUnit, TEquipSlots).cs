using System;

namespace Davfalcon.Equipment
{
	public abstract class Equipment<TUnit, TEquipSlots> : UnitStatsModifier<TUnit>, IEquipment<TUnit, TEquipSlots>
		where TUnit : IUnitTemplate<TUnit>
		where TEquipSlots : Enum
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public TEquipSlots EquipmentSlot { get; set; }
	}
}
