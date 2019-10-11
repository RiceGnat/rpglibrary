using System;

namespace Davfalcon.Equipment
{
	public interface IEquipment<TUnit, TEquipSlots> : IStatsModifier<TUnit>
		where TUnit : IUnitTemplate<TUnit>
		where TEquipSlots : Enum
	{
		string Name { get; }

		string Description { get; }

		TEquipSlots EquipmentSlot { get; }
	}
}
