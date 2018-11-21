using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public abstract class UnitModifierBase<T> : UnitStatsModifier<T>, IModifier<IUnit>, IUnit where T : IUnit
	{
		IDictionary<Enum, int> IUnit.VolatileStats => Target.VolatileStats;
		IModifierCollection<IUnit> IUnit.Modifiers => Target.Modifiers;
		IUnitEquipmentManager<IUnit> IUnit.Equipment => Target.Equipment;
		IModifierCollection<IUnit> IUnit.Buffs => Target.Buffs;

		IUnit IModifier<IUnit>.Target { get; }
		void IModifier<IUnit>.Bind(IUnit target) => base.Bind((T)target);
		void IModifier<IUnit>.Bind(IModifier<IUnit> target) => Bind((IModifier<T>)target);
	}
}
