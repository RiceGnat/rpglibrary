using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public abstract class UnitModifierBase<T> : UnitStatsModifier<T>, IUnit where T : IUnit
	{
		IDictionary<Enum, int> IUnit.VolatileStats => Target.VolatileStats;
		IModifierCollection<IUnit> IUnit.Modifiers => Target.Modifiers;
		IUnitEquipmentManager IUnit.Equipment => Target.Equipment;
		IModifierCollection<IUnit> IUnit.Buffs => Target.Buffs;
	}
}
