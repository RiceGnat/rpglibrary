using System;

namespace Davfalcon.Equipment
{
	public interface IEquipment<TUnit, TEquipmentType> : IStatsModifier<TUnit>
		where TUnit : class, IUnitTemplate<TUnit>
		where TEquipmentType : Enum
	{
		string Name { get; }

		string Description { get; }

		TEquipmentType EquipmentType { get; }
	}
}
