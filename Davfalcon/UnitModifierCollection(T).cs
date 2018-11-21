using System;
using System.Collections.Generic;

namespace Davfalcon
{
	[Serializable]
	public class UnitModifierCollection<T> : ModifierCollection<T>, IModifierCollection<IUnit>, IUnit where T : IUnit
	{
		string IUnit.Name => Interface.Name;
		string IUnit.Class => Interface.Class;
		int IUnit.Level => Interface.Level;
		IModifierCollection<IUnit> IUnit.Modifiers => Interface.Modifiers;
		IStats IStatsContainer.Stats => Interface.Stats;
		IStatsDetails IStatsContainer.StatsDetails => Interface.StatsDetails;

		IUnit IModifier<IUnit>.Target => Target;
		public void Add(IModifier<IUnit> item) => Add((IModifier<T>)item);
		void IModifier<IUnit>.Bind(IUnit target) => base.Bind((T)target);
		void IModifier<IUnit>.Bind(IModifier<IUnit> target) => Bind((IModifier<T>)target);
		public bool Remove(IModifier<IUnit> item) => Remove((IModifier<T>)item);
		// Not sure how to implement this yet
		IEnumerator<IModifier<IUnit>> IEnumerable<IModifier<IUnit>>.GetEnumerator() => throw new NotImplementedException();
	}
}
