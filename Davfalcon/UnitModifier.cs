using System;

namespace Davfalcon
{
	/// <summary>
	/// Abstract base class for unit modifiers.
	/// </summary>
	[Serializable]
	public abstract class UnitModifier<T> : Modifier<T>, IModifier<IUnit>, IUnit, IEditableDescription where T : IUnit
	{
		string IUnit.Name => Target.Name;
		string IUnit.Class => Target.Class;
		int IUnit.Level => Target.Level;
		IStats IStatsContainer.Stats => Target.Stats;
		IStatsDetails IStatsContainer.StatsDetails => Target.StatsDetails;
		IModifierCollection<IUnit> IUnit.Modifiers => Target.Modifiers;

		IUnit IModifier<IUnit>.Target => Target;
		void IModifier<IUnit>.Bind(IUnit target) => base.Bind((T)target);
		void IModifier<IUnit>.Bind(IModifier<IUnit> target) => Bind((IModifier<T>)target);
	}

	[Serializable]
	internal class UnitModifier : UnitModifier<IUnit> { }
}
