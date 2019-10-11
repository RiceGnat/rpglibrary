using System;

namespace Davfalcon.Equipment
{
	public abstract class Equipment<TUnit, TEquipmentType> : UnitStatsModifier<TUnit>, IEquipment<TUnit, TEquipmentType>
		where TUnit : IUnitTemplate<TUnit>
		where TEquipmentType : Enum
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public TEquipmentType EquipmentType { get; set; }
	}
}
