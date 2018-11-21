using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class UnitModifierCollection<T> : Davfalcon.UnitModifierCollection<T>, IModifierCollection<IUnit>, IUnit where T : IUnit
	{
		IDictionary<Enum, int> IUnit.VolatileStats => Interface.VolatileStats;
		IModifierCollection<IUnit> IUnit.Modifiers => Interface.Modifiers;
		IUnitEquipmentManager<IUnit> IUnit.Equipment => Interface.Equipment;
		IModifierCollection<IUnit> IUnit.Buffs => Interface.Buffs;

		IUnit IModifier<IUnit>.Target => Target;
		public void Add(IModifier<IUnit> item) => Add((IModifier<T>)item);
		void IModifier<IUnit>.Bind(IUnit target) => base.Bind((T)target);
		void IModifier<IUnit>.Bind(IModifier<IUnit> target) => Bind((IModifier<T>)target);
		public bool Remove(IModifier<IUnit> item) => Remove((IModifier<T>)item);
		// Not sure how to implement this yet
		IEnumerator<IModifier<IUnit>> IEnumerable<IModifier<IUnit>>.GetEnumerator() => throw new NotImplementedException();
	}
}
